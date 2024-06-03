using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class Up_Information_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Informations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Informations_ColorId",
                table: "Informations",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Informations_Colors_ColorId",
                table: "Informations",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Informations_Colors_ColorId",
                table: "Informations");

            migrationBuilder.DropIndex(
                name: "IX_Informations_ColorId",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Informations");
        }
    }
}
