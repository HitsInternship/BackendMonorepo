using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeanModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class documentID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentUrl",
                table: "Applications");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "Applications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Applications");

            migrationBuilder.AddColumn<string>(
                name: "DocumentUrl",
                table: "Applications",
                type: "text",
                nullable: true);
        }
    }
}
