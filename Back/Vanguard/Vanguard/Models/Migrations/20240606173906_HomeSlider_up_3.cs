using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class HomeSlider_up_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HomeSliderId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeSliders_TagId",
                table: "HomeSliders",
                column: "TagId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeSliders_Tags_TagId",
                table: "HomeSliders",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeSliders_Tags_TagId",
                table: "HomeSliders");

            migrationBuilder.DropIndex(
                name: "IX_HomeSliders_TagId",
                table: "HomeSliders");

            migrationBuilder.DropColumn(
                name: "HomeSliderId",
                table: "Tags");
        }
    }
}
