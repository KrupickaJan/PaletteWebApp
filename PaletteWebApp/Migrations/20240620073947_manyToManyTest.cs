using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaletteWebApp.Migrations
{
    /// <inheritdoc />
    public partial class manyToManyTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserPalette_AspNetUsers_AppUserId",
                table: "AppUserPalette");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserPalette_Palettes_PaletteId",
                table: "AppUserPalette");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserPalette",
                table: "AppUserPalette");

            migrationBuilder.DropIndex(
                name: "IX_AppUserPalette_PaletteId",
                table: "AppUserPalette");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "AppUserPalette",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "AppUserPalette",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserPalette",
                table: "AppUserPalette",
                columns: new[] { "AppUserId", "PaletteId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserPalette_PaletteId",
                table: "AppUserPalette",
                column: "PaletteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserPalette_AspNetUsers_AppUserId",
                table: "AppUserPalette",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserPalette_Palettes_PaletteId",
                table: "AppUserPalette",
                column: "PaletteId",
                principalTable: "Palettes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
