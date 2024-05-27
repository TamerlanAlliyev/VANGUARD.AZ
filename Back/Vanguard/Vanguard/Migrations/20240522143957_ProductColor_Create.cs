using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class ProductColor_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductColor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    ProductColorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductColor_ProductColor_ProductColorId",
                        column: x => x.ProductColorId,
                        principalTable: "ProductColor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductColor_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ColorProductColor_ProductColorsId",
                table: "ColorProductColor",
                column: "ProductColorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ProductColorId",
                table: "ProductColor",
                column: "ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ProductId",
                table: "ProductColor",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorProductColor");

            migrationBuilder.DropTable(
                name: "ProductColor");
        }
    }
}
