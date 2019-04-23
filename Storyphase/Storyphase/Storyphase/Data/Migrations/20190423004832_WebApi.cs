using Microsoft.EntityFrameworkCore.Migrations;

namespace Storyphase.Migrations
{
    public partial class WebApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoryBlockId",
                table: "StoryBlocks",
                newName: "StoryBlocksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoryBlocksId",
                table: "StoryBlocks",
                newName: "StoryBlockId");
        }
    }
}
