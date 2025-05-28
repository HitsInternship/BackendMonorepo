using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "CompanyPersons");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CompanyPersons");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "CompanyPersons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CompanyPersons",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CompanyPersons",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "CompanyPersons",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
