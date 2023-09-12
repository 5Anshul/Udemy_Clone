using Microsoft.EntityFrameworkCore.Migrations;

namespace Edtech_backend_API.Migrations
{
    public partial class RemovedCommentFromInstructor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseInstructorComment",
                table: "CourseInstructors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseInstructorComment",
                table: "CourseInstructors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
