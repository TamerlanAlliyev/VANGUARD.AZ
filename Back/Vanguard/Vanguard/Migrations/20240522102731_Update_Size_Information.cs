using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class Update_Size_Information : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Dimensions",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Sizes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Sizes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Dimensions",
                table: "Sizes",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "Sizes",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
