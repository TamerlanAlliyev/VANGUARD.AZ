using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class Basket_Wish_Update_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Wishs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wishs_SizeId",
                table: "Wishs",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_SizeId",
                table: "Baskets",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Sizes_SizeId",
                table: "Baskets",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishs_Sizes_SizeId",
                table: "Wishs",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Sizes_SizeId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishs_Sizes_SizeId",
                table: "Wishs");

            migrationBuilder.DropIndex(
                name: "IX_Wishs_SizeId",
                table: "Wishs");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_SizeId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Wishs");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Baskets");
        }
    }
}
