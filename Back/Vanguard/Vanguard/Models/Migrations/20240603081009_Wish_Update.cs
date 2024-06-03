using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class Wish_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishs_AspNetUsers_UserId",
                table: "Wishs");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishs_Products_ProductId",
                table: "Wishs");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishs_Sizes_SizeId",
                table: "Wishs");

            migrationBuilder.DropIndex(
                name: "IX_Wishs_ProductId",
                table: "Wishs");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Wishs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Wishs",
                newName: "AppUserId");

            migrationBuilder.RenameColumn(
                name: "SizeId",
                table: "Wishs",
                newName: "InformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Wishs_UserId",
                table: "Wishs",
                newName: "IX_Wishs_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Wishs_SizeId",
                table: "Wishs",
                newName: "IX_Wishs_InformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishs_AspNetUsers_AppUserId",
                table: "Wishs",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishs_Informations_InformationId",
                table: "Wishs",
                column: "InformationId",
                principalTable: "Informations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishs_AspNetUsers_AppUserId",
                table: "Wishs");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishs_Informations_InformationId",
                table: "Wishs");

            migrationBuilder.RenameColumn(
                name: "InformationId",
                table: "Wishs",
                newName: "SizeId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Wishs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Wishs_InformationId",
                table: "Wishs",
                newName: "IX_Wishs_SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_Wishs_AppUserId",
                table: "Wishs",
                newName: "IX_Wishs_UserId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Wishs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wishs_ProductId",
                table: "Wishs",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishs_AspNetUsers_UserId",
                table: "Wishs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishs_Products_ProductId",
                table: "Wishs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishs_Sizes_SizeId",
                table: "Wishs",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
