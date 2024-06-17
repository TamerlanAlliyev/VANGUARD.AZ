using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class About_up_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_About_Images_ImageId",
                table: "About");

            migrationBuilder.DropIndex(
                name: "IX_About_ImageId",
                table: "About");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "About");

            migrationBuilder.RenameColumn(
                name: "Aboutid",
                table: "Images",
                newName: "AboutId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AboutId",
                table: "Images",
                column: "AboutId",
                unique: true,
                filter: "[AboutId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_About_AboutId",
                table: "Images",
                column: "AboutId",
                principalTable: "About",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_About_AboutId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AboutId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "AboutId",
                table: "Images",
                newName: "Aboutid");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "About",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_About_ImageId",
                table: "About",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_About_Images_ImageId",
                table: "About",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
