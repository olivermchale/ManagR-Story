using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoryService.Data.Migrations
{
    public partial class board : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomLabel",
                table: "AgileItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "BoardEnd",
                table: "Boards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "BoardStart",
                table: "Boards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardEnd",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "BoardStart",
                table: "Boards");

            migrationBuilder.AddColumn<string>(
                name: "CustomLabel",
                table: "AgileItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
