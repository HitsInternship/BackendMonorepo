using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelectionModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedvacancyresponsestatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "VacancyResponse",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "VacancyResponse");
        }
    }
}
