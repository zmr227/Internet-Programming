using Microsoft.EntityFrameworkCore.Migrations;

namespace Storyphase.Migrations
{
    public partial class AddAuthorToStories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stories_Title",
                table: "Stories");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Stories",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Stories");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_Title",
                table: "Stories",
                column: "Title",
                unique: true);
        }
    }
}
