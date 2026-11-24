using System.Data.Common;
using System.Globalization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MyMvcApp.Data;

static class DatabaseMaintenance
{
    /// <summary>
    /// Консистентная копия базы через VACUUM INTO. Хранит последние <paramref name="keep"/> копий.
    /// Вызывается на старте — до миграции, чтобы неудачное обновление можно было откатить.
    /// </summary>
    public static void Backup(AppPaths paths, int keep)
    {
        if (!File.Exists(paths.DatabaseFile)) return;

        var target = Path.Combine(paths.BackupDirectory,
            $"medical-equipment-{DateTime.Now:yyyyMMdd-HHmmss}.db");
        try
        {
            using (var connection = new SqliteConnection(paths.ConnectionString))
            {
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = "VACUUM INTO $path;";
                command.Parameters.AddWithValue("$path", target);
                command.ExecuteNonQuery();
            }

            foreach (var old in new DirectoryInfo(paths.BackupDirectory)
                         .GetFiles("medical-equipment-*.db")
                         .OrderByDescending(f => f.Name)
                         .Skip(keep))
                old.Delete();
        }
        catch (Exception ex)
        {
            // Бэкап не должен мешать запуску, но пользователь должен об этом узнать
            Console.WriteLine($"ВНИМАНИЕ: не удалось сделать резервную копию базы: {ex.Message}");
        }
    }
}

/// <summary>
/// В SQLite встроенные lower()/upper() и LIKE без учёта регистра работают только для ASCII:
/// поиск "иван" не найдёт "Иван". Переопределяем функции .NET-реализацией.
/// </summary>
sealed class UnicodeCaseInterceptor : DbConnectionInterceptor
{
    public override void ConnectionOpened(DbConnection connection, ConnectionEndEventData eventData)
        => Register(connection);

    public override Task ConnectionOpenedAsync(DbConnection connection, ConnectionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        Register(connection);
        return Task.CompletedTask;
    }

    private static void Register(DbConnection connection)
    {
        if (connection is not SqliteConnection sqlite) return;
        sqlite.CreateFunction("lower", (string? s) => s?.ToLower(CultureInfo.InvariantCulture), isDeterministic: true);
        sqlite.CreateFunction("upper", (string? s) => s?.ToUpper(CultureInfo.InvariantCulture), isDeterministic: true);
    }
}

/// <summary>
/// Для `dotnet ef migrations add`: инструменты EF создают контекст здесь,
/// не запуская Program.cs (мьютекс, бэкап, веб-сервер).
/// </summary>
sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("Data Source=design-time.db")
            .Options;
        return new ApplicationDbContext(options);
    }
}
