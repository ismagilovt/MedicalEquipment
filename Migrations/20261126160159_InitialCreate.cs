using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "medical_equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Manufacturer = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CountryOfOrigin = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    RegistrationNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    SerialNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    InventoryNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Barcode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    InventoryType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ClassRisk = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    OkofCode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    TechnicalSpecification = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CommissioningDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DecommissioningDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ResponsiblePerson = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    LastMaintenanceDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NextMaintenanceDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MaintenanceNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ManualFilePath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CertificateFilePath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_equipment", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medical_equipment");
        }
    }
}
