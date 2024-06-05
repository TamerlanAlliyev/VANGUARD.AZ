using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class Blog_Up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Blog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Blog",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blog_AppUserId1",
                table: "Blog",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_AspNetUsers_AppUserId1",
                table: "Blog",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_AspNetUsers_AppUserId1",
                table: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_AppUserId1",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Blog");
        }
    }
}
