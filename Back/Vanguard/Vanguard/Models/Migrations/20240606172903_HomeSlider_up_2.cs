using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class HomeSlider_up_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "HomeSliders");

            migrationBuilder.AddColumn<int>(
                name: "HomeSliderId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "HomeSliders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HomeSliders_ImageId",
                table: "HomeSliders",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeSliders_Images_ImageId",
                table: "HomeSliders",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeSliders_Images_ImageId",
                table: "HomeSliders");

            migrationBuilder.DropIndex(
                name: "IX_HomeSliders_ImageId",
                table: "HomeSliders");

            migrationBuilder.DropColumn(
                name: "HomeSliderId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "HomeSliders");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "HomeSliders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
