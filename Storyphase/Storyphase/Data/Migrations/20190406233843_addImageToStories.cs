using Microsoft.EntityFrameworkCore.Migrations;

namespace Storyphase.Data.Migrations
{
    public partial class addImageToStories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Stories",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PrivacyTags",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Stories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PrivacyTags",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
