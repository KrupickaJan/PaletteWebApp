using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaletteWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addNameToPalette : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Color");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Palettes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Palettes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Color",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
