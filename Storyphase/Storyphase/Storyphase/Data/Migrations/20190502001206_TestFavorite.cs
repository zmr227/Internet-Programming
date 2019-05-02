using Microsoft.EntityFrameworkCore.Migrations;

namespace Storyphase.Migrations
{
    public partial class TestFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "StoriesAddToFavorites",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoriesAddToFavorites_UserId",
                table: "StoriesAddToFavorites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoriesAddToFavorites_AspNetUsers_UserId",
                table: "StoriesAddToFavorites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoriesAddToFavorites_AspNetUsers_UserId",
                table: "StoriesAddToFavorites");

            migrationBuilder.DropIndex(
                name: "IX_StoriesAddToFavorites_UserId",
                table: "StoriesAddToFavorites");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StoriesAddToFavorites");
        }
    }
}
