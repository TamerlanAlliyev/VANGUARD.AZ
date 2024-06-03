using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class Wish_Update_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishs_Informations_InformationId",
                table: "Wishs");

            migrationBuilder.RenameColumn(
                name: "InformationId",
                table: "Wishs",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Wishs_InformationId",
                table: "Wishs",
                newName: "IX_Wishs_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishs_Products_ProductId",
                table: "Wishs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishs_Products_ProductId",
                table: "Wishs");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Wishs",
                newName: "InformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Wishs_ProductId",
                table: "Wishs",
                newName: "IX_Wishs_InformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishs_Informations_InformationId",
                table: "Wishs",
                column: "InformationId",
                principalTable: "Informations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
