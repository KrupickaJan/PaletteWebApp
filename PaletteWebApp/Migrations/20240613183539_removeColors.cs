using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaletteWebApp.Migrations
{
    /// <inheritdoc />
    public partial class removeColors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorPalette");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.AddColumn<string>(
                name: "Colors",
                table: "Palettes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colors",
                table: "Palettes");

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorCode = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

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
    }
}
