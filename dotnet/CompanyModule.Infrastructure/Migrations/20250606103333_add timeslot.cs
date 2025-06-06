using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtimeslot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TimeSlotNumber",
                table: "Appointments");

            migrationBuilder.AddColumn<Guid>(
                name: "timeslotId",
                table: "Appointments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Timeslots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodNumber = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeslots", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_timeslotId",
                table: "Appointments",
                column: "timeslotId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Timeslots_timeslotId",
                table: "Appointments",
                column: "timeslotId",
                principalTable: "Timeslots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Timeslots_timeslotId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "Timeslots");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_timeslotId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "timeslotId",
                table: "Appointments");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TimeSlotNumber",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
