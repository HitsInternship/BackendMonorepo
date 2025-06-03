using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Practice",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PracticeDiaryComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    DiaryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeDiaryComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticeDiaryComment_PracticeDiary_DiaryId",
                        column: x => x.DiaryId,
                        principalTable: "PracticeDiary",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentPracticeCharacteristicComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    PracticeCharacteristicId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPracticeCharacteristicComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentPracticeCharacteristicComment_StudentPracticeCharact~",
                        column: x => x.PracticeCharacteristicId,
                        principalTable: "StudentPracticeCharacteristic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PracticeDiaryComment_DiaryId",
                table: "PracticeDiaryComment",
                column: "DiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPracticeCharacteristicComment_PracticeCharacteristic~",
                table: "StudentPracticeCharacteristicComment",
                column: "PracticeCharacteristicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PracticeDiaryComment");

            migrationBuilder.DropTable(
                name: "StudentPracticeCharacteristicComment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Practice");
        }
    }
}
