using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileApp.Migrations
{
    /// <inheritdoc />
    public partial class builddb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CourseMaterialLinks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "CourseMaterialLinks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CourseMaterialLinks");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "CourseMaterialLinks");
        }
    }
}
