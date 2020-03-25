using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoryService.Data.Migrations
{
    public partial class boardname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardDto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BoardName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardDto", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardDto");
        }
    }
}
