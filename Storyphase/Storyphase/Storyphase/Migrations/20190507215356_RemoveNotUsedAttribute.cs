using Microsoft.EntityFrameworkCore.Migrations;

namespace Storyphase.Migrations
{
    public partial class RemoveNotUsedAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StoryBlocks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Stories",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StoryBlocks",
                nullable: true,
                oldClrType: typeof(string));

          
            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Stories",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
