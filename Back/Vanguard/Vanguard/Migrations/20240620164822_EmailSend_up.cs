using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class EmailSend_up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "EmailSends",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "EmailSends");
        }
    }
}
