using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_Razor.Migrations
{
    public partial class AddMorePropertiesToStory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Stories",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Stories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Stories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Stories");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Stories",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
