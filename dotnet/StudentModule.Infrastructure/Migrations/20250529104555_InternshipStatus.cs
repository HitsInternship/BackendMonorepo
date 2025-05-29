using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InternshipStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InternshipStatus",
                table: "SStudents",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternshipStatus",
                table: "SStudents");
        }
    }
}
