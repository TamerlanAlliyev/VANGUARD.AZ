using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class HomeSlider_up_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeSliders_Images_ImageId",
                table: "HomeSliders");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeSliders_Tags_TagId",
                table: "HomeSliders");

            migrationBuilder.DropIndex(
                name: "IX_HomeSliders_ImageId",
                table: "HomeSliders");

            migrationBuilder.DropIndex(
                name: "IX_HomeSliders_TagId",
                table: "HomeSliders");

            migrationBuilder.CreateIndex(
                name: "IX_Images_HomeSliderId",
                table: "Images",
                column: "HomeSliderId",
                unique: true,
                filter: "[HomeSliderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSliders_TagId",
                table: "HomeSliders",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeSliders_Tags_TagId",
                table: "HomeSliders",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_HomeSliders_HomeSliderId",
                table: "Images",
                column: "HomeSliderId",
                principalTable: "HomeSliders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeSliders_Tags_TagId",
                table: "HomeSliders");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_HomeSliders_HomeSliderId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_HomeSliderId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_HomeSliders_TagId",
                table: "HomeSliders");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSliders_ImageId",
                table: "HomeSliders",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSliders_TagId",
                table: "HomeSliders",
                column: "TagId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeSliders_Images_ImageId",
                table: "HomeSliders",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeSliders_Tags_TagId",
                table: "HomeSliders",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
