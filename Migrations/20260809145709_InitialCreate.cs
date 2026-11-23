using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Manufacturer = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CountryOfOrigin = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SerialNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InventoryNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Barcode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ClassRisk = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OkofCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TechnicalSpecification = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CommissioningDate = table.Column<DateTime>(type: "date", nullable: true),
                    DecommissioningDate = table.Column<DateTime>(type: "date", nullable: true),
                    Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ResponsiblePerson = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    LastMaintenanceDate = table.Column<DateTime>(type: "date", nullable: true),
                    NextMaintenanceDate = table.Column<DateTime>(type: "date", nullable: true),
                    MaintenanceNotes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ManualFilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CertificateFilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
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
