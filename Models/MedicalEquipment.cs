using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyMvcApp.Models
{
[Table("medical_equipment")]
public class MedicalEquipment
{
    [Key]
    public int Id { get; set; }

    // Идентификация
    [Required, StringLength(100)]
    [Display(Name = "оборудование")]
    public string Name { get; set; }                 // Наименование изделия (как в РУ)
    
    [StringLength(100)]
    [Display(Name = "модель")]
    public string Model { get; set; }               // Модель/исполнение
    
    [StringLength(100)]
    [Display(Name = "производитель")]
    public string Manufacturer { get; set; }       // Производитель
    
    [StringLength(100)]
    [Display(Name = "страна произвосттва")]
    public string CountryOfOrigin { get; set; }     // Страна происхождения

    // Регистрационные и учётные данные
    [StringLength(50)]
    [Display(Name = "регистрация и удалёные данные")]
    public string RegistrationNumber { get; set; } // Номер РУ (регистрационного удостоверения)
    
    [StringLength(50)]
    [Display(Name = "номер завотской")]
    public string SerialNumber { get; set; }       // Заводской номер
    
    [StringLength(50)]
    [Display(Name = "инвентарьный номер ")]
    public string InventoryNumber { get; set; }    // Инвентарный номер МО
    
    [StringLength(50)]
    [Display(Name = "штришкод")]
    public string Barcode { get; set; }             // Штрих‑код (опционально)

    // Технические характеристики
    [StringLength(100)]
    [Display(Name = "тип/вид")]
    public string InventoryType { get; set; }                // Тип/вид МИ (по номенклатурной классификации)
    
    [StringLength(100)]
    [Display(Name = "класс риска")]
    public string ClassRisk { get; set; }           // Класс потенциального риска (1, 2а, 2б, 3)
    
    [StringLength(100)]
    [Display(Name = "код ОКОФ")]
    public string OkofCode { get; set; }            // Код ОКОФ (для бухучёта)
    
    [StringLength(100)]
    [Display(Name = "короткое техписание")]
    public string TechnicalSpecification { get; set; } // Краткое техописание/ТУ

    // Эксплуатация
    [Column(TypeName = "date")]
    [Display(Name = "дата ввода в эксплуатацию")]
    public DateTime? CommissioningDate { get; set; }  // Дата ввода в эксплуатацию
    
    [Column(TypeName = "date")]
    [Display(Name = "дата списания")]
    public DateTime? DecommissioningDate { get; set; } // Дата списания
    
    [StringLength(200)]
    [Display(Name = "кобинет")]
    public string Location { get; set; }           // Подразделение/кабинет
    
    [StringLength(200)]
    [Display(Name = "отстетствинный сотрудник")]
    public string ResponsiblePerson { get; set; } // Ответственный сотрудник

    // Обслуживание и контроль
    [Column(TypeName = "date")]
    [Display(Name = "дата последниго ТО")]
    public DateTime? LastMaintenanceDate { get; set; } // Дата последнего ТО
    
    [Column(TypeName = "date")]
    [Display(Name = "дата плонового ТО")]
    public DateTime? NextMaintenanceDate { get; set; } // Дата планового ТО
    
    [StringLength(500)]
    [Display(Name = "помечания по обслуживанию")]
    public string MaintenanceNotes { get; set; }   // Примечания по обслуживанию

    // Документы и файлы
    [StringLength(500)]
    [Display(Name = "путь к РЭ")]
    public string ManualFilePath { get; set; }     // Путь к РЭ (руководству по эксплуатации)
    
    [StringLength(500)]
    [Display(Name = "путь к сиртификату")]
    public string CertificateFilePath { get; set; } // Путь к сертификату/декларации

    // Статусы и доп. атрибуты
    public bool IsActive { get; set; } = true;    // В эксплуатации (true) или списано (false)
    
    [StringLength(500)]
    [Display(Name = "общие применчания")]
    public string Notes { get; set; }             // Общие примечания
}}
