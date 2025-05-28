using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelectionModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Selections_SelectionId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_SelectionId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "SelectionId",
                table: "Candidates");

            migrationBuilder.CreateIndex(
                name: "IX_Selections_CandidateId",
                table: "Selections",
                column: "CandidateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Selections_Candidates_CandidateId",
                table: "Selections",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Selections_Candidates_CandidateId",
                table: "Selections");

            migrationBuilder.DropIndex(
                name: "IX_Selections_CandidateId",
                table: "Selections");

            migrationBuilder.AddColumn<Guid>(
                name: "SelectionId",
                table: "Candidates",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_SelectionId",
                table: "Candidates",
                column: "SelectionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Selections_SelectionId",
                table: "Candidates",
                column: "SelectionId",
                principalTable: "Selections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
