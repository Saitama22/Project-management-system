using Microsoft.EntityFrameworkCore.Migrations;

namespace Jira.Migrations
{
    public partial class ProjectLTs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "TaskStates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskStates_ProjectId",
                table: "TaskStates",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskStates_Projects_ProjectId",
                table: "TaskStates",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskStates_Projects_ProjectId",
                table: "TaskStates");

            migrationBuilder.DropIndex(
                name: "IX_TaskStates_ProjectId",
                table: "TaskStates");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "TaskStates");
        }
    }
}
