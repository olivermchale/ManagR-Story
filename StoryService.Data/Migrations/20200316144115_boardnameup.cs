using Microsoft.EntityFrameworkCore.Migrations;

namespace StoryService.Data.Migrations
{
    public partial class boardnameup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BoardDto",
                table: "BoardDto");

            migrationBuilder.RenameTable(
                name: "BoardDto",
                newName: "Boards");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boards",
                table: "Boards",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Boards",
                table: "Boards");

            migrationBuilder.RenameTable(
                name: "Boards",
                newName: "BoardDto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoardDto",
                table: "BoardDto",
                column: "Id");
        }
    }
}
