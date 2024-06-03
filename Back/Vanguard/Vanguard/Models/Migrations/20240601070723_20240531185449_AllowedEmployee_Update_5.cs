using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class _20240531185449_AllowedEmployee_Update_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AllowedEmployees",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AllowedEmployees_RoleId",
                table: "AllowedEmployees",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllowedEmployees_AspNetRoles_RoleId",
                table: "AllowedEmployees",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllowedEmployees_AspNetRoles_RoleId",
                table: "AllowedEmployees");

            migrationBuilder.DropIndex(
                name: "IX_AllowedEmployees_RoleId",
                table: "AllowedEmployees");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AllowedEmployees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
