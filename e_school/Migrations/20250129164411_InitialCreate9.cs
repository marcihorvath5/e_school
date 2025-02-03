using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_school.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Classes_ClassId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Classes_ClassId",
                table: "AspNetUsers",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Classes_ClassId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Classes_ClassId",
                table: "AspNetUsers",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }
    }
}
