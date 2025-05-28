using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelectionModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcommentupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacancyResponseEntity_Candidates_CandidateId",
                table: "VacancyResponseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_VacancyResponseEntity_Vacancies_VacancyId",
                table: "VacancyResponseEntity");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.DropTable(
                name: "Selections");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VacancyResponseEntity",
                table: "VacancyResponseEntity");

            migrationBuilder.RenameTable(
                name: "VacancyResponseEntity",
                newName: "BaseEntity");

            migrationBuilder.RenameIndex(
                name: "IX_VacancyResponseEntity_VacancyId",
                table: "BaseEntity",
                newName: "IX_BaseEntity_VacancyId");

            migrationBuilder.RenameIndex(
                name: "IX_VacancyResponseEntity_CandidateId",
                table: "BaseEntity",
                newName: "IX_BaseEntity_CandidateId");

            migrationBuilder.AlterColumn<Guid>(
                name: "VacancyId",
                table: "BaseEntity",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "BaseEntity",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "CandidateId",
                table: "BaseEntity",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "Comment_UserId",
                table: "BaseEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "BaseEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeadLine",
                table: "BaseEntity",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseEntity",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "BaseEntity",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "BaseEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PositionEntity_Description",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PositionId",
                table: "BaseEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SelectionEntityId",
                table: "BaseEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SelectionEntity_CandidateId",
                table: "BaseEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SelectionStatus",
                table: "BaseEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "BaseEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "BaseEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "BaseEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseEntity",
                table: "BaseEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_ParentId",
                table: "BaseEntity",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_PositionId",
                table: "BaseEntity",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_SelectionEntity_CandidateId",
                table: "BaseEntity",
                column: "SelectionEntity_CandidateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_SelectionEntityId",
                table: "BaseEntity",
                column: "SelectionEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_CandidateId",
                table: "BaseEntity",
                column: "CandidateId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_ParentId",
                table: "BaseEntity",
                column: "ParentId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_PositionId",
                table: "BaseEntity",
                column: "PositionId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_SelectionEntityId",
                table: "BaseEntity",
                column: "SelectionEntityId",
                principalTable: "BaseEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_SelectionEntity_CandidateId",
                table: "BaseEntity",
                column: "SelectionEntity_CandidateId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_VacancyId",
                table: "BaseEntity",
                column: "VacancyId",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_CandidateId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_ParentId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_PositionId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_SelectionEntityId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_SelectionEntity_CandidateId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_VacancyId",
                table: "BaseEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseEntity",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_ParentId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_PositionId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_SelectionEntity_CandidateId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_SelectionEntityId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Comment_UserId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "DeadLine",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "PositionEntity_Description",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "SelectionEntityId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "SelectionEntity_CandidateId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "SelectionStatus",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BaseEntity");

            migrationBuilder.RenameTable(
                name: "BaseEntity",
                newName: "VacancyResponseEntity");

            migrationBuilder.RenameIndex(
                name: "IX_BaseEntity_VacancyId",
                table: "VacancyResponseEntity",
                newName: "IX_VacancyResponseEntity_VacancyId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseEntity_CandidateId",
                table: "VacancyResponseEntity",
                newName: "IX_VacancyResponseEntity_CandidateId");

            migrationBuilder.AlterColumn<Guid>(
                name: "VacancyId",
                table: "VacancyResponseEntity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "VacancyResponseEntity",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CandidateId",
                table: "VacancyResponseEntity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacancyResponseEntity",
                table: "VacancyResponseEntity",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Selections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    SelectionStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Selections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Selections_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacancies_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    SelectionEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    VacancyResponseEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Selections_SelectionEntityId",
                        column: x => x.SelectionEntityId,
                        principalTable: "Selections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_VacancyResponseEntity_VacancyResponseEntityId",
                        column: x => x.VacancyResponseEntityId,
                        principalTable: "VacancyResponseEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SelectionEntityId",
                table: "Comments",
                column: "SelectionEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_VacancyResponseEntityId",
                table: "Comments",
                column: "VacancyResponseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Selections_CandidateId",
                table: "Selections",
                column: "CandidateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_PositionId",
                table: "Vacancies",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_VacancyResponseEntity_Candidates_CandidateId",
                table: "VacancyResponseEntity",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VacancyResponseEntity_Vacancies_VacancyId",
                table: "VacancyResponseEntity",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
