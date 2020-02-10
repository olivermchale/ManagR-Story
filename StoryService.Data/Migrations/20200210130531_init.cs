using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoryService.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SuperStories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    DueBy = table.Column<DateTime>(nullable: false),
                    Prority = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperStories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    DueBy = table.Column<DateTime>(nullable: false),
                    Prority = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    SuperStoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stories_SuperStories_SuperStoryId",
                        column: x => x.SuperStoryId,
                        principalTable: "SuperStories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    DueBy = table.Column<DateTime>(nullable: false),
                    Prority = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    StoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssigneeDto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    StoryDtoId = table.Column<Guid>(nullable: true),
                    SuperStoryDtoId = table.Column<Guid>(nullable: true),
                    TaskDtoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssigneeDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssigneeDto_Stories_StoryDtoId",
                        column: x => x.StoryDtoId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssigneeDto_SuperStories_SuperStoryDtoId",
                        column: x => x.SuperStoryDtoId,
                        principalTable: "SuperStories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssigneeDto_Tasks_TaskDtoId",
                        column: x => x.TaskDtoId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssigneeDto_StoryDtoId",
                table: "AssigneeDto",
                column: "StoryDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssigneeDto_SuperStoryDtoId",
                table: "AssigneeDto",
                column: "SuperStoryDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssigneeDto_TaskDtoId",
                table: "AssigneeDto",
                column: "TaskDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_SuperStoryId",
                table: "Stories",
                column: "SuperStoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StoryId",
                table: "Tasks",
                column: "StoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssigneeDto");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "SuperStories");
        }
    }
}
