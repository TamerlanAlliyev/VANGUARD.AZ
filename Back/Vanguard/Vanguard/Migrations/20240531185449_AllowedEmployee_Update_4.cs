using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class AllowedEmployee_Update_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AllowedEmployees_AllowedEmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserAddresses_UserAddressId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AllowedEmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AllowedEmployees");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "AllowedEmployees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllowedEmployees_AppUserId",
                table: "AllowedEmployees",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AllowedEmployees_AspNetUsers_AppUserId",
                table: "AllowedEmployees",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserAddresses_UserAddressId",
                table: "AspNetUsers",
                column: "UserAddressId",
                principalTable: "UserAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllowedEmployees_AspNetUsers_AppUserId",
                table: "AllowedEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserAddresses_UserAddressId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AllowedEmployees_AppUserId",
                table: "AllowedEmployees");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "AllowedEmployees");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AllowedEmployees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AllowedEmployeeId",
                table: "AspNetUsers",
                column: "AllowedEmployeeId",
                unique: true,
                filter: "[AllowedEmployeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AllowedEmployees_AllowedEmployeeId",
                table: "AspNetUsers",
                column: "AllowedEmployeeId",
                principalTable: "AllowedEmployees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserAddresses_UserAddressId",
                table: "AspNetUsers",
                column: "UserAddressId",
                principalTable: "UserAddresses",
                principalColumn: "Id");
        }
    }
}
