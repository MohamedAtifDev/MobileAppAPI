using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileApp.Migrations
{
    /// <inheritdoc />
    public partial class builddb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_schedules_AcademicYearCoursesTeachers_AcademicYearId_CourseId_TeacherId",
                        columns: x => new { x.AcademicYearId, x.CourseId, x.TeacherId },
                        principalTable: "AcademicYearCoursesTeachers",
                        principalColumns: new[] { "AcademicYearId", "CourseId", "TeacherId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_schedules_AcademicYearId_CourseId_TeacherId",
                table: "schedules",
                columns: new[] { "AcademicYearId", "CourseId", "TeacherId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "schedules");
        }
    }
}
