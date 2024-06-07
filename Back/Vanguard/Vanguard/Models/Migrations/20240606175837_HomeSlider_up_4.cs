using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class HomeSlider_up_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeSliders_Images_ImageId",
                table: "HomeSliders");

            migrationBuilder.DropIndex(
                name: "IX_HomeSliders_ImageId",
                table: "HomeSliders");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "HomeSliders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSliders_ImageId",
                table: "HomeSliders",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeSliders_Images_ImageId",
                table: "HomeSliders",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
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

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "HomeSliders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
