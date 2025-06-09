using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelectionModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addisconfirmed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Selections",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Selections");
        }
    }
}
