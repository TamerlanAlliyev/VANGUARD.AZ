using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class HomeBanner_Up_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HomeBannerId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "HomeBanners",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_HomeBannerId",
                table: "Images",
                column: "HomeBannerId",
                unique: true,
                filter: "[HomeBannerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_HomeBanners_HomeBannerId",
                table: "Images",
                column: "HomeBannerId",
                principalTable: "HomeBanners",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_HomeBanners_HomeBannerId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_HomeBannerId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "HomeBannerId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "HomeBanners");
        }
    }
}
