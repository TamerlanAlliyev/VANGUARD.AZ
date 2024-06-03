using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class Up_Information_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Informations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Informations_SizeId",
                table: "Informations",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Informations_Sizes_SizeId",
                table: "Informations",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Informations_Sizes_SizeId",
                table: "Informations");

            migrationBuilder.DropIndex(
                name: "IX_Informations_SizeId",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Informations");
        }
    }
}
