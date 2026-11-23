using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class MedicalEquipmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicalEquipmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MedicalEquipment
        public async Task<IActionResult> Index()
        {
            // Фильтруем только активное оборудование (не списанное)
            var equipmentList = await _context.MedicalEquipments
               // .Where(e => e.DecommissioningDate == null)
                .ToListAsync();

            return View(equipmentList);
        }

        // GET: MedicalEquipment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var equipment = await _context.MedicalEquipments.FindAsync(id);
            if (equipment == null)
                return NotFound();

            return View(equipment);
        }

        // GET: MedicalEquipment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MedicalEquipment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Brand,Model, RegistrationNumber,Location,IsMeasurementDevice,MeasurementDeviceRegistrationNumber,MaintenanceDate,MaintenanceDocument,NextMaintenanceDate,DecommissioningDate,TechnicalSpecification,ClassRisk,OkofCode,InventoryNumber,SerialNumber,Barcode,InventoryType,Manufacturer,CountryOfOrigin,ResponsiblePerson,MaintenanceNotes,ManualFilePath,CertificateFilePath,Notes")] MedicalEquipment equipment)
        {
           Console.WriteLine(equipment);
            //if (ModelState.IsValid)
           // {
                _context.Add(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            return View(equipment);
        }

        // GET: MedicalEquipment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var equipment = await _context.MedicalEquipments.FindAsync(id);
            if (equipment == null)
                return NotFound();

            return View(equipment);
        }

        // POST: MedicalEquipment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Manufacturer,Model,CountryOfOrigin,InventoryType,RegistrationNumber,SerialNumber,InventoryNumber,Barcode,ClassRisk,OkofCode,TechnicalSpecification,CommissioningDate,DecommissioningDate,Location,ResponsiblePerson,LastMaintenanceDate,NextMaintenanceDate,MaintenanceNotes,ManualFilePath,CertificateFilePath,IsActive,Notes")] MedicalEquipment equipment)
        {
            if (id != equipment.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalEquipmentExists(equipment.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

       

       // POST: MedicalEquipment/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var equipment = await _context.MedicalEquipments.FindAsync(id);
        if (equipment == null)
            return NotFound();

        _context.MedicalEquipments.Remove(equipment);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool MedicalEquipmentExists(int id)
    {
        return _context.MedicalEquipments.Any(e => e.Id == id);
    }
}
}
