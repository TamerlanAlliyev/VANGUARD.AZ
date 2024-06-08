using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class Banners_up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_HomeBanners_HomeBannerId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_ShopBanner_HomeBannerId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ShopBannerId",
                table: "Images",
                column: "ShopBannerId",
                unique: true,
                filter: "[ShopBannerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_HomeBanners_HomeBannerId",
                table: "Images",
                column: "HomeBannerId",
                principalTable: "HomeBanners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ShopBanner_ShopBannerId",
                table: "Images",
                column: "ShopBannerId",
                principalTable: "ShopBanner",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_HomeBanners_HomeBannerId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_ShopBanner_ShopBannerId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ShopBannerId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_HomeBanners_HomeBannerId",
                table: "Images",
                column: "HomeBannerId",
                principalTable: "HomeBanners",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ShopBanner_HomeBannerId",
                table: "Images",
                column: "HomeBannerId",
                principalTable: "ShopBanner",
                principalColumn: "Id");
        }
    }
}
