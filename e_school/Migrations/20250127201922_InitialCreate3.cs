using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_school.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacherSubjects_Teachers_TeacherSubjectSubjectId_Teache~",
                table: "ClassTeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Users_TeacherId1",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_TeacherId1",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_ClassTeacherSubjects_TeacherSubjectSubjectId_TeacherSubjectT~",
                table: "ClassTeacherSubjects");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "TeacherSubjectSubjectId",
                table: "ClassTeacherSubjects");

            migrationBuilder.DropColumn(
                name: "TeacherSubjectTeacherId",
                table: "ClassTeacherSubjects");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "Teachers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_TeacherId",
                table: "Teachers",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeacherSubjects_Teachers_SubjectId_TeacherId",
                table: "ClassTeacherSubjects",
                columns: new[] { "SubjectId", "TeacherId" },
                principalTable: "Teachers",
                principalColumns: new[] { "SubjectId", "TeacherId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Users_TeacherId",
                table: "Teachers",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacherSubjects_Teachers_SubjectId_TeacherId",
                table: "ClassTeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Users_TeacherId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_TeacherId",
                table: "Teachers");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Teachers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TeacherId1",
                table: "Teachers",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TeacherSubjectSubjectId",
                table: "ClassTeacherSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherSubjectTeacherId",
                table: "ClassTeacherSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_TeacherId1",
                table: "Teachers",
                column: "TeacherId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeacherSubjects_TeacherSubjectSubjectId_TeacherSubjectT~",
                table: "ClassTeacherSubjects",
                columns: new[] { "TeacherSubjectSubjectId", "TeacherSubjectTeacherId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeacherSubjects_Teachers_TeacherSubjectSubjectId_Teache~",
                table: "ClassTeacherSubjects",
                columns: new[] { "TeacherSubjectSubjectId", "TeacherSubjectTeacherId" },
                principalTable: "Teachers",
                principalColumns: new[] { "SubjectId", "TeacherId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Users_TeacherId1",
                table: "Teachers",
                column: "TeacherId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
