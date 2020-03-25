using Microsoft.EntityFrameworkCore.Migrations;

namespace StoryService.Data.Migrations
{
    public partial class time : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "EstimatedTime",
                table: "AgileItems",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LoggedTime",
                table: "AgileItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedTime",
                table: "AgileItems");

            migrationBuilder.DropColumn(
                name: "LoggedTime",
                table: "AgileItems");
        }
    }
}
