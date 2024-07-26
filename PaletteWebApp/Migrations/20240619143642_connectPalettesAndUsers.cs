using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaletteWebApp.Migrations
{
    /// <inheritdoc />
    public partial class connectPalettesAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaletteId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PaletteId",
                table: "AspNetUsers",
                column: "PaletteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Palettes_PaletteId",
                table: "AspNetUsers",
                column: "PaletteId",
                principalTable: "Palettes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Palettes_PaletteId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PaletteId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PaletteId",
                table: "AspNetUsers");
        }
    }
}
