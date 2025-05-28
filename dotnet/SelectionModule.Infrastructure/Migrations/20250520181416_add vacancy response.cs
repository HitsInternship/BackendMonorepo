using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelectionModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addvacancyresponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VacancyResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VacancyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CandidateId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    VacancyId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancyResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacancyResponse_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacancyResponse_Candidates_CandidateId1",
                        column: x => x.CandidateId1,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VacancyResponse_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacancyResponse_Vacancies_VacancyId1",
                        column: x => x.VacancyId1,
                        principalTable: "Vacancies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacancyResponse_CandidateId",
                table: "VacancyResponse",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyResponse_CandidateId1",
                table: "VacancyResponse",
                column: "CandidateId1");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyResponse_VacancyId",
                table: "VacancyResponse",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyResponse_VacancyId1",
                table: "VacancyResponse",
                column: "VacancyId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacancyResponse");
        }
    }
}
