using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class HomeSlider_up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "HomeSliders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagId",
                table: "HomeSliders");
        }
    }
}
