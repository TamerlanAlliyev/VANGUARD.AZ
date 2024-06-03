using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class Basket_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_AspNetUsers_UserId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Products_ProductId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Sizes_SizeId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_SizeId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Baskets");

            migrationBuilder.RenameColumn(
                name: "SizeId",
                table: "Baskets",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Baskets",
                newName: "InformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_ProductId",
                table: "Baskets",
                newName: "IX_Baskets_InformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Informations_InformationId",
                table: "Baskets",
                column: "InformationId",
                principalTable: "Informations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Informations_InformationId",
                table: "Baskets");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Baskets",
                newName: "SizeId");

            migrationBuilder.RenameColumn(
                name: "InformationId",
                table: "Baskets",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_InformationId",
                table: "Baskets",
                newName: "IX_Baskets_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Baskets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_SizeId",
                table: "Baskets",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_AspNetUsers_UserId",
                table: "Baskets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Products_ProductId",
                table: "Baskets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Sizes_SizeId",
                table: "Baskets",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
