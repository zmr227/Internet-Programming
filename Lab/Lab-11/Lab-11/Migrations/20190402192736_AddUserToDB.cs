using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab11.Migrations
{
    public partial class AddUserToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Stories",
                newName: "UserID1");

            migrationBuilder.AlterColumn<string>(
                name: "UserID1",
                table: "Stories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Stories",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_UserID1",
                table: "Stories",
                column: "UserID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Users_UserID1",
                table: "Stories",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Users_UserID1",
                table: "Stories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Stories_UserID1",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Stories");

            migrationBuilder.RenameColumn(
                name: "UserID1",
                table: "Stories",
                newName: "Author");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Stories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
