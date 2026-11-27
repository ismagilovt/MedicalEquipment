using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class AddComponents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Komplect",
                table: "medical_equipment",
                type: "TEXT",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Komplect",
                table: "medical_equipment");
        }
    }
}
