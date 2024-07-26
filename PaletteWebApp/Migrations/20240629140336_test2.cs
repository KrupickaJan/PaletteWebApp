using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaletteWebApp.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserPalette_Palettes_palettesId",
                table: "AppUserPalette");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserPalette",
                table: "AppUserPalette");

            migrationBuilder.DropIndex(
                name: "IX_AppUserPalette_palettesId",
                table: "AppUserPalette");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Palettes");

            migrationBuilder.RenameColumn(
                name: "palettesId",
                table: "AppUserPalette",
                newName: "PalettesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserPalette",
                table: "AppUserPalette",
                columns: new[] { "PalettesId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserPalette_UsersId",
                table: "AppUserPalette",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserPalette_Palettes_PalettesId",
                table: "AppUserPalette",
                column: "PalettesId",
                principalTable: "Palettes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserPalette_Palettes_PalettesId",
                table: "AppUserPalette");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserPalette",
                table: "AppUserPalette");

            migrationBuilder.DropIndex(
                name: "IX_AppUserPalette_UsersId",
                table: "AppUserPalette");

            migrationBuilder.RenameColumn(
                name: "PalettesId",
                table: "AppUserPalette",
                newName: "palettesId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Palettes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserPalette",
                table: "AppUserPalette",
                columns: new[] { "UsersId", "palettesId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserPalette_palettesId",
                table: "AppUserPalette",
                column: "palettesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserPalette_Palettes_palettesId",
                table: "AppUserPalette",
                column: "palettesId",
                principalTable: "Palettes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
