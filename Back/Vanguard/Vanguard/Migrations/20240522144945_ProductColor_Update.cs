using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class ProductColor_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_ProductColor_ProductColorId",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Products_ProductId",
                table: "ProductColor");

            migrationBuilder.DropIndex(
                name: "IX_ProductColor_ProductColorId",
                table: "ProductColor");

            migrationBuilder.DropIndex(
                name: "IX_ProductColor_ProductId",
                table: "ProductColor");

            migrationBuilder.DropColumn(
                name: "ProductColorId",
                table: "ProductColor");

            migrationBuilder.CreateTable(
                name: "ProductProductColor",
                columns: table => new
                {
                    ProductColorsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductColor", x => new { x.ProductColorsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductProductColor_ProductColor_ProductColorsId",
                        column: x => x.ProductColorsId,
                        principalTable: "ProductColor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductColor_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductColor_ProductsId",
                table: "ProductProductColor",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProductColor");

            migrationBuilder.AddColumn<int>(
                name: "ProductColorId",
                table: "ProductColor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ProductColorId",
                table: "ProductColor",
                column: "ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ProductId",
                table: "ProductColor",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_ProductColor_ProductColorId",
                table: "ProductColor",
                column: "ProductColorId",
                principalTable: "ProductColor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Products_ProductId",
                table: "ProductColor",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
