using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class Color_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ColorId",
                table: "Images",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Colors_ColorId",
                table: "Images",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Colors_ColorId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ColorId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Images");
        }
    }
}
