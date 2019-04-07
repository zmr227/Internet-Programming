using Microsoft.EntityFrameworkCore.Migrations;

namespace Storyphase.Data.Migrations
{
    public partial class addPrivacyToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrivacyTagId",
                table: "Stories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stories_PrivacyTagId",
                table: "Stories",
                column: "PrivacyTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_PrivacyTags_PrivacyTagId",
                table: "Stories",
                column: "PrivacyTagId",
                principalTable: "PrivacyTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_PrivacyTags_PrivacyTagId",
                table: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Stories_PrivacyTagId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "PrivacyTagId",
                table: "Stories");
        }
    }
}
