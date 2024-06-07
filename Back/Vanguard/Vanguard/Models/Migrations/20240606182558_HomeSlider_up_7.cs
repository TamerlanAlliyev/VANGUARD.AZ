using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanguard.Migrations
{
    /// <inheritdoc />
    public partial class HomeSlider_up_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "HomeSliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "HomeSliders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "HomeSliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HomeSliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "HomeSliders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "HomeSliders",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HomeSliders");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "HomeSliders");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "HomeSliders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HomeSliders");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "HomeSliders");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "HomeSliders");
        }
    }
}
