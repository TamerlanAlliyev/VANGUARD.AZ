using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class AboutEmploeeUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutEmploees_Images_ImageId",
                table: "AboutEmploees");

            migrationBuilder.DropIndex(
                name: "IX_AboutEmploees_ImageId",
                table: "AboutEmploees");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "AboutEmploees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_AboutEmploees_ImageId",
                table: "AboutEmploees",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutEmploees_Images_ImageId",
                table: "AboutEmploees",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutEmploees_Images_ImageId",
                table: "AboutEmploees");

            migrationBuilder.DropIndex(
                name: "IX_AboutEmploees_ImageId",
                table: "AboutEmploees");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "AboutEmploees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AboutEmploees_ImageId",
                table: "AboutEmploees",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AboutEmploees_Images_ImageId",
                table: "AboutEmploees",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
