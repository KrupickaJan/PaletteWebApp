using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaletteWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addNameToColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Color",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Color");
        }
    }
}
