using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_school.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacherSubjects_Teachers_SubjectId_TeacherId",
                table: "ClassTeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacherSubjects_Users_TeacherId1",
                table: "ClassTeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_ClassTeacherSubjects_TeacherId1",
                table: "ClassTeacherSubjects");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "ClassTeacherSubjects");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "ClassTeacherSubjects",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
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
                name: "IX_ClassTeacherSubjects_TeacherId",
                table: "ClassTeacherSubjects",
                column: "TeacherId");

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
                name: "FK_ClassTeacherSubjects_Users_TeacherId",
                table: "ClassTeacherSubjects",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacherSubjects_Teachers_TeacherSubjectSubjectId_Teache~",
                table: "ClassTeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacherSubjects_Users_TeacherId",
                table: "ClassTeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_ClassTeacherSubjects_TeacherId",
                table: "ClassTeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_ClassTeacherSubjects_TeacherSubjectSubjectId_TeacherSubjectT~",
                table: "ClassTeacherSubjects");

            migrationBuilder.DropColumn(
                name: "TeacherSubjectSubjectId",
                table: "ClassTeacherSubjects");

            migrationBuilder.DropColumn(
                name: "TeacherSubjectTeacherId",
                table: "ClassTeacherSubjects");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "ClassTeacherSubjects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TeacherId1",
                table: "ClassTeacherSubjects",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeacherSubjects_TeacherId1",
                table: "ClassTeacherSubjects",
                column: "TeacherId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeacherSubjects_Teachers_SubjectId_TeacherId",
                table: "ClassTeacherSubjects",
                columns: new[] { "SubjectId", "TeacherId" },
                principalTable: "Teachers",
                principalColumns: new[] { "SubjectId", "TeacherId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeacherSubjects_Users_TeacherId1",
                table: "ClassTeacherSubjects",
                column: "TeacherId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
