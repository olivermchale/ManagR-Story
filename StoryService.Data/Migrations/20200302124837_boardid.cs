using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoryService.Data.Migrations
{
    public partial class boardid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BoardId",
                table: "Tasks",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BoardId",
                table: "SuperStories",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BoardId",
                table: "Stories",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "SuperStories");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Stories");
        }
    }
}
