using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class Blog_Update_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "Blog",
                newName: "BlogTTagId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Blog",
                newName: "BlogCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlogTTagId",
                table: "Blog",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "BlogCategoryId",
                table: "Blog",
                newName: "CategoryId");
        }
    }
}
