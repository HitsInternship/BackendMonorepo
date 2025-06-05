using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Messages",
                newName: "MessageStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageStatus",
                table: "Messages",
                newName: "Status");
        }
    }
}
