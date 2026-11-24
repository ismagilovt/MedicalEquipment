using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;

// =====================================================================
// Portable-режим: запуск с флешки на Windows без установки.
// Все данные лежат в папке UserData рядом с exe и переезжают вместе с ней.
// =====================================================================

Console.OutputEncoding = Encoding.UTF8;

var paths = AppPaths.FromBaseDirectory(AppContext.BaseDirectory);
paths.EnsureCreated();

// ---- Один экземпляр на одну папку (иначе две копии пишут в одну базу) ----
using var mutex = new Mutex(initiallyOwned: true, paths.MutexName, out var isFirstInstance);
if (!isFirstInstance)
{
    Console.WriteLine("Программа уже запущена. Открываю её в браузере...");
    if (File.Exists(paths.AddressFile))
        OpenBrowser(File.ReadAllText(paths.AddressFile).Trim());
    return;
}

var isDev = string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "Development",
    StringComparison.OrdinalIgnoreCase);

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    // В portable-режиме wwwroot и конфиги ищем рядом с exe, а не в текущей папке
    ContentRootPath = isDev ? null : AppContext.BaseDirectory,
});

if (!builder.Environment.IsDevelopment())
{
    // Только localhost и свободный порт: нет запроса брандмауэра, прав админа и конфликтов портов
    builder.WebHost.UseUrls("http://127.0.0.1:0");
}

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options
    .UseSqlite(paths.ConnectionString)
    .AddInterceptors(new UnicodeCaseInterceptor()));

// Ключи (antiforgery) храним на флешке, без DPAPI: DPAPI привязан к пользователю Windows
// и на другом ПК ключи не расшифруются. Заодно не оставляем следов в профиле пользователя.
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(paths.KeysDirectory))
    .SetApplicationName("MedicalEquipment");

var app = builder.Build();

// ---- Бэкап перед миграцией, затем миграция ----
DatabaseMaintenance.Backup(paths, keep: 10);
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // HTTPS/HSTS намеренно отключены: на чужом ПК нет сертификата, а трафик не покидает 127.0.0.1
}

var ru = new CultureInfo("ru-RU");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ru),
    SupportedCultures = [ru],
    SupportedUICultures = [ru],
});

app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

// Документы (РЭ, сертификаты) лежат в UserData/docs и открываются по ссылке /docs/<файл>
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(paths.DocsDirectory),
    RequestPath = "/docs",
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MedicalEquipment}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Lifetime.ApplicationStarted.Register(() =>
{
    var address = app.Services.GetRequiredService<IServer>()
        .Features.Get<IServerAddressesFeature>()?.Addresses.FirstOrDefault();
    if (address is null) return;

    File.WriteAllText(paths.AddressFile, address);
    Console.WriteLine();
    Console.WriteLine($"Программа запущена: {address}");
    Console.WriteLine($"Данные: {paths.DataDirectory}");
    Console.WriteLine("Не закрывайте это окно, пока работаете. Чтобы выйти — закройте окно.");
    if (!app.Environment.IsDevelopment()) OpenBrowser(address);
});

app.Lifetime.ApplicationStopping.Register(() =>
{
    try { File.Delete(paths.AddressFile); } catch { /* не критично */ }
});

app.Run();

static void OpenBrowser(string url)
{
    try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); }
    catch { Console.WriteLine($"Откройте в браузере: {url}"); }
}

// =====================================================================

sealed record AppPaths(string DataDirectory)
{
    public string DatabaseFile   => Path.Combine(DataDirectory, "medical-equipment.db");
    public string BackupDirectory => Path.Combine(DataDirectory, "backup");
    public string DocsDirectory  => Path.Combine(DataDirectory, "docs");
    public string KeysDirectory  => Path.Combine(DataDirectory, "keys");
    public string AddressFile    => Path.Combine(DataDirectory, "address.txt");

    public string ConnectionString => new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder
    {
        DataSource = DatabaseFile,
        DefaultTimeout = 30, // ждать блокировку, а не падать с "database is locked"
        Pooling = false,     // файл отпускается сразу -> флешку можно извлечь после выхода
    }.ToString();

    public string MutexName => "MedicalEquipment_" + Convert.ToHexString(
        SHA256.HashData(Encoding.UTF8.GetBytes(DataDirectory.ToLowerInvariant())))[..16];

    public static AppPaths FromBaseDirectory(string baseDirectory) =>
        new(Path.Combine(baseDirectory, "UserData"));

    public void EnsureCreated()
    {
        Directory.CreateDirectory(DataDirectory);
        Directory.CreateDirectory(BackupDirectory);
        Directory.CreateDirectory(DocsDirectory);
        Directory.CreateDirectory(KeysDirectory);
    }
}
