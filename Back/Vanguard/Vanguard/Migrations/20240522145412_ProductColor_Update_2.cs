using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class ProductColor_Update_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorProductColor");

            migrationBuilder.DropTable(
                name: "ProductProductColor");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ColorId",
                table: "ProductColor",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ProductId",
                table: "ProductColor",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Colors_ColorId",
                table: "ProductColor",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Products_ProductId",
                table: "ProductColor",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Colors_ColorId",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Products_ProductId",
                table: "ProductColor");

            migrationBuilder.DropIndex(
                name: "IX_ProductColor_ColorId",
                table: "ProductColor");

            migrationBuilder.DropIndex(
                name: "IX_ProductColor_ProductId",
                table: "ProductColor");

            migrationBuilder.CreateTable(
                name: "ColorProductColor",
                columns: table => new
                {
                    ColorsId = table.Column<int>(type: "int", nullable: false),
                    ProductColorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorProductColor", x => new { x.ColorsId, x.ProductColorsId });
                    table.ForeignKey(
                        name: "FK_ColorProductColor_Colors_ColorsId",
                        column: x => x.ColorsId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColorProductColor_ProductColor_ProductColorsId",
                        column: x => x.ProductColorsId,
                        principalTable: "ProductColor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_ColorProductColor_ProductColorsId",
                table: "ColorProductColor",
                column: "ProductColorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductColor_ProductsId",
                table: "ProductProductColor",
                column: "ProductsId");
        }
    }
}
