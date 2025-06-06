using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class appointmentChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeSlotNumber",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSlotNumber",
                table: "Appointments");
        }
    }
}
