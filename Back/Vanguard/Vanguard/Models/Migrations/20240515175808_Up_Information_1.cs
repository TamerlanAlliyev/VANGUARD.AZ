using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class Up_Information_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Informations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Informations_ProductId",
                table: "Informations",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Informations_Products_ProductId",
                table: "Informations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Informations_Products_ProductId",
                table: "Informations");

            migrationBuilder.DropIndex(
                name: "IX_Informations_ProductId",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Informations");
        }
    }
}
