using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable

namespace Vanguard.Migrations
{
    public partial class HomeBanner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_HomeBanners_Categories_Id",
                table: "HomeBanners");

            // Drop FK in the Images table
            migrationBuilder.DropForeignKey(
                name: "FK_Images_HomeBanners_HomeBannerId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeBanners",
                table: "HomeBanners");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HomeBanners");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "HomeBanners",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeBanners",
                table: "HomeBanners",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HomeBanners_CategoryId",
                table: "HomeBanners",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeBanners_Categories_CategoryId",
                table: "HomeBanners",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            // Re-add the FK to the Images table
            migrationBuilder.AddForeignKey(
                name: "FK_Images_HomeBanners_HomeBannerId",
                table: "Images",
                column: "HomeBannerId",
                principalTable: "HomeBanners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Id", table: "HomeBanners");
            migrationBuilder.DropForeignKey(
                name: "FK_HomeBanners_Categories_CategoryId",
                table: "HomeBanners");

            migrationBuilder.DropIndex(
                name: "IX_HomeBanners_CategoryId",
                table: "HomeBanners");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "HomeBanners",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
     name: "Id",
     table: "HomeBanners",
     type: "int",
     nullable: false)
     .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeBanners_Categories_Id",
                table: "HomeBanners",
                column: "Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
