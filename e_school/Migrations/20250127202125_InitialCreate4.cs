using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_school.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacherSubjects_Teachers_SubjectId_TeacherId",
                table: "ClassTeacherSubjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeacherSubjects_Teachers_SubjectId_TeacherId",
                table: "ClassTeacherSubjects",
                columns: new[] { "SubjectId", "TeacherId" },
                principalTable: "Teachers",
                principalColumns: new[] { "SubjectId", "TeacherId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
