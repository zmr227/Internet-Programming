using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Storyphase.Migrations
{
    public partial class addTestApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "StoryBlocks",
                newName: "StoryBlockId");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "StoryBlocks",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "StoryBlocks",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StoryBlocks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "StoryBlocks",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StoryId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "StoryBlocks");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "StoryBlocks");

            migrationBuilder.RenameColumn(
                name: "StoryBlockId",
                table: "StoryBlocks",
                newName: "Id");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "StoryBlocks",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "StoryBlocks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StoryId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
