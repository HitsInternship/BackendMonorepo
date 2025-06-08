using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelectionModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addglobalselection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GlobalSelectionId",
                table: "Selections",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "GlobalSelections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StreamId = table.Column<Guid>(type: "uuid", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalSelections", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Selections_GlobalSelectionId",
                table: "Selections",
                column: "GlobalSelectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Selections_GlobalSelections_GlobalSelectionId",
                table: "Selections",
                column: "GlobalSelectionId",
                principalTable: "GlobalSelections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Selections_GlobalSelections_GlobalSelectionId",
                table: "Selections");

            migrationBuilder.DropTable(
                name: "GlobalSelections");

            migrationBuilder.DropIndex(
                name: "IX_Selections_GlobalSelectionId",
                table: "Selections");

            migrationBuilder.DropColumn(
                name: "GlobalSelectionId",
                table: "Selections");
        }
    }
}
