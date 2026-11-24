using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMvcApp.Models;

[Table("medical_equipment")]
public class MedicalEquipment
{
    [Key]
    public int Id { get; set; }

    // ---- Идентификация ----
    [Required(ErrorMessage = "Укажите наименование")]
    [StringLength(100)]
    [Display(Name = "Наименование")]
    public string Name { get; set; } = "";               // Наименование изделия (как в РУ)

    [StringLength(100)]
    [Display(Name = "Модель")]
    public string? Model { get; set; }                   // Модель/исполнение

    [StringLength(100)]
    [Display(Name = "Производитель")]
    public string? Manufacturer { get; set; }

    [StringLength(100)]
    [Display(Name = "Страна производства")]
    public string? CountryOfOrigin { get; set; }

    // ---- Регистрационные и учётные данные ----
    [StringLength(50)]
    [Display(Name = "Номер РУ")]
    public string? RegistrationNumber { get; set; }      // Номер регистрационного удостоверения

    [StringLength(50)]
    [Display(Name = "Заводской номер")]
    public string? SerialNumber { get; set; }

    [StringLength(50)]
    [Display(Name = "Инвентарный номер")]
    public string? InventoryNumber { get; set; }         // Инвентарный номер МО

    [StringLength(50)]
    [Display(Name = "Штрихкод")]
    public string? Barcode { get; set; }

    // ---- Технические характеристики ----
    [StringLength(100)]
    [Display(Name = "Тип/вид")]
    public string? InventoryType { get; set; }           // Тип/вид МИ по номенклатурной классификации

    [StringLength(100)]
    [Display(Name = "Класс риска")]
    public string? ClassRisk { get; set; }               // 1, 2а, 2б, 3

    [StringLength(100)]
    [Display(Name = "Код ОКОФ")]
    public string? OkofCode { get; set; }

    [StringLength(1000)]
    [Display(Name = "Краткое техописание")]
    public string? TechnicalSpecification { get; set; }  // Краткое техописание/ТУ

    // ---- Эксплуатация ----
    // DataType.Date обязателен: без него <input type="date"> получает значение с временем,
    // браузер показывает пустое поле, и при сохранении Edit дата затирается.
    [DataType(DataType.Date)]
    [Display(Name = "Дата ввода в эксплуатацию")]
    public DateTime? CommissioningDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Дата списания")]
    public DateTime? DecommissioningDate { get; set; }

    [StringLength(200)]
    [Display(Name = "Подразделение/кабинет")]
    public string? Location { get; set; }

    [StringLength(200)]
    [Display(Name = "Ответственный сотрудник")]
    public string? ResponsiblePerson { get; set; }

    // ---- Обслуживание и контроль ----
    [DataType(DataType.Date)]
    [Display(Name = "Дата последнего ТО")]
    public DateTime? LastMaintenanceDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Дата планового ТО")]
    public DateTime? NextMaintenanceDate { get; set; }

    [StringLength(500)]
    [Display(Name = "Примечания по обслуживанию")]
    public string? MaintenanceNotes { get; set; }

    // ---- Документы ----
    // Относительный путь внутри UserData/docs (например "РЭ/аппарат-1.pdf") или URL.
    // Абсолютные пути (E:\...) ломаются при смене буквы флешки.
    [StringLength(500)]
    [Display(Name = "Руководство по эксплуатации (файл)")]
    public string? ManualFilePath { get; set; }

    [StringLength(500)]
    [Display(Name = "Сертификат/декларация (файл)")]
    public string? CertificateFilePath { get; set; }

    // ---- Статусы ----
    [Display(Name = "В эксплуатации")]
    public bool IsActive { get; set; } = true;           // true — в эксплуатации, false — списано

    [StringLength(500)]
    [Display(Name = "Общие примечания")]
    public string? Notes { get; set; }
}
