using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models; // Замени на пространство имен, где лежит твой класс MedicalEquipment

namespace MyMvcApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Здесь указываем DbSet для каждой таблицы, которую хочешь создать
        public DbSet<MedicalEquipment> MedicalEquipments { get; set; } 
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Опционально: если хочешь жестко задать имя таблицы без 's' на конце
            modelBuilder.Entity<MedicalEquipment>().ToTable("medical_equipment");
        }
    }
}
