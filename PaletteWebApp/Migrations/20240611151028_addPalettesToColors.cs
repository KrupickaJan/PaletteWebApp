
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaletteWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addPalettesToColors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Color_Palettes_PaletteId",
                table: "Color");

            migrationBuilder.DropIndex(
                name: "IX_Color_PaletteId",
                table: "Color");

            migrationBuilder.DropColumn(
                name: "PaletteId",
                table: "Color");

            migrationBuilder.CreateTable(
                name: "ColorPalette",
                columns: table => new
                {
                    ColorsId = table.Column<int>(type: "int", nullable: false),
                    palettesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorPalette", x => new { x.ColorsId, x.palettesId });
                    table.ForeignKey(
                        name: "FK_ColorPalette_Color_ColorsId",
                        column: x => x.ColorsId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColorPalette_Palettes_palettesId",
                        column: x => x.palettesId,
                        principalTable: "Palettes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColorPalette_palettesId",
                table: "ColorPalette",
                column: "palettesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorPalette");

            migrationBuilder.AddColumn<int>(
                name: "PaletteId",
                table: "Color",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Color_PaletteId",
                table: "Color",
                column: "PaletteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Color_Palettes_PaletteId",
                table: "Color",
                column: "PaletteId",
                principalTable: "Palettes",
                principalColumn: "Id");
        }
    }
}
