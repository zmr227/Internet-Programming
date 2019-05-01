using Microsoft.EntityFrameworkCore.Migrations;

namespace Storyphase.Migrations
{
    public partial class AddBlockNumToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlockNumber",
                table: "StoryBlocks",
                newName: "Position");

            migrationBuilder.AddColumn<int>(
                name: "BlockNumber",
                table: "Stories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockNumber",
                table: "Stories");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "StoryBlocks",
                newName: "BlockNumber");
        }
    }
}
