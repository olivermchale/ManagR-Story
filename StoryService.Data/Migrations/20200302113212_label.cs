using Microsoft.EntityFrameworkCore.Migrations;

namespace StoryService.Data.Migrations
{
    public partial class label : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomLabel",
                table: "Stories",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StoryPoints",
                table: "Stories",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomLabel",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "StoryPoints",
                table: "Stories");
        }
    }
}
