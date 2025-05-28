using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelectionModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacancyResponse_Candidates_CandidateId1",
                table: "VacancyResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_VacancyResponse_Vacancies_VacancyId1",
                table: "VacancyResponse");

            migrationBuilder.DropIndex(
                name: "IX_VacancyResponse_CandidateId1",
                table: "VacancyResponse");

            migrationBuilder.DropIndex(
                name: "IX_VacancyResponse_VacancyId1",
                table: "VacancyResponse");

            migrationBuilder.DropColumn(
                name: "CandidateId1",
                table: "VacancyResponse");

            migrationBuilder.DropColumn(
                name: "VacancyId1",
                table: "VacancyResponse");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CandidateId1",
                table: "VacancyResponse",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VacancyId1",
                table: "VacancyResponse",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VacancyResponse_CandidateId1",
                table: "VacancyResponse",
                column: "CandidateId1");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyResponse_VacancyId1",
                table: "VacancyResponse",
                column: "VacancyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_VacancyResponse_Candidates_CandidateId1",
                table: "VacancyResponse",
                column: "CandidateId1",
                principalTable: "Candidates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VacancyResponse_Vacancies_VacancyId1",
                table: "VacancyResponse",
                column: "VacancyId1",
                principalTable: "Vacancies",
                principalColumn: "Id");
        }
    }
}
