using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoryService.Data.Migrations
{
    public partial class singleassignee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssigneeDto_Stories_StoryDtoId",
                table: "AssigneeDto");

            migrationBuilder.DropForeignKey(
                name: "FK_AssigneeDto_SuperStories_SuperStoryDtoId",
                table: "AssigneeDto");

            migrationBuilder.DropIndex(
                name: "IX_AssigneeDto_StoryDtoId",
                table: "AssigneeDto");

            migrationBuilder.DropIndex(
                name: "IX_AssigneeDto_SuperStoryDtoId",
                table: "AssigneeDto");

            migrationBuilder.DropColumn(
                name: "StoryDtoId",
                table: "AssigneeDto");

            migrationBuilder.DropColumn(
                name: "SuperStoryDtoId",
                table: "AssigneeDto");

            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeId",
                table: "Stories",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeId",
                table: "AssigneeDto",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Stories_AssigneeId",
                table: "Stories",
                column: "AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_AssigneeDto_AssigneeId",
                table: "Stories",
                column: "AssigneeId",
                principalTable: "AssigneeDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_AssigneeDto_AssigneeId",
                table: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Stories_AssigneeId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "AssigneeDto");

            migrationBuilder.AddColumn<Guid>(
                name: "StoryDtoId",
                table: "AssigneeDto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SuperStoryDtoId",
                table: "AssigneeDto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssigneeDto_StoryDtoId",
                table: "AssigneeDto",
                column: "StoryDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssigneeDto_SuperStoryDtoId",
                table: "AssigneeDto",
                column: "SuperStoryDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssigneeDto_Stories_StoryDtoId",
                table: "AssigneeDto",
                column: "StoryDtoId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssigneeDto_SuperStories_SuperStoryDtoId",
                table: "AssigneeDto",
                column: "SuperStoryDtoId",
                principalTable: "SuperStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
