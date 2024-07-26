using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaletteWebApp.Migrations
{
    /// <inheritdoc />
    public partial class manyToManyTest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "AppUserPalette");

            migrationBuilder.RenameColumn(
                name: "PaletteId",
                table: "AppUserPalette",
                newName: "palettesId");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "AppUserPalette",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserPalette",
                table: "AppUserPalette",
                columns: new[] { "UsersId", "palettesId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserPalette_palettesId",
                table: "AppUserPalette",
                column: "palettesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserPalette_AspNetUsers_UsersId",
                table: "AppUserPalette",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserPalette_Palettes_palettesId",
                table: "AppUserPalette",
                column: "palettesId",
                principalTable: "Palettes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserPalette_AspNetUsers_UsersId",
                table: "AppUserPalette");

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
                name: "UsersId",
                table: "AppUserPalette");

            migrationBuilder.RenameColumn(
                name: "palettesId",
                table: "AppUserPalette",
                newName: "PaletteId");

            migrationBuilder.AddColumn<int>(
                name: "PaletteId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "AppUserPalette",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
