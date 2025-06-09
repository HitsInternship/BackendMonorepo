using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addglobalpractice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentPracticeCharacteristic_PracticeId",
                table: "StudentPracticeCharacteristic");

            migrationBuilder.DropIndex(
                name: "IX_PracticeDiary_PracticeId",
                table: "PracticeDiary");

            migrationBuilder.DropColumn(
                name: "PracticeType",
                table: "Practice");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "Practice",
                newName: "GlobalPracticeId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StudentPracticeCharacteristicComment",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StudentPracticeCharacteristic",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PracticeDiaryComment",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PracticeDiary",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Mark",
                table: "Practice",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "GlobalPractice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PracticeType = table.Column<int>(type: "integer", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uuid", nullable: false),
                    StreamId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiaryPatternDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacteristicsPatternDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalPractice", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentPracticeCharacteristic_PracticeId",
                table: "StudentPracticeCharacteristic",
                column: "PracticeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PracticeDiary_PracticeId",
                table: "PracticeDiary",
                column: "PracticeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Practice_GlobalPracticeId",
                table: "Practice",
                column: "GlobalPracticeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Practice_GlobalPractice_GlobalPracticeId",
                table: "Practice",
                column: "GlobalPracticeId",
                principalTable: "GlobalPractice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Practice_GlobalPractice_GlobalPracticeId",
                table: "Practice");

            migrationBuilder.DropTable(
                name: "GlobalPractice");

            migrationBuilder.DropIndex(
                name: "IX_StudentPracticeCharacteristic_PracticeId",
                table: "StudentPracticeCharacteristic");

            migrationBuilder.DropIndex(
                name: "IX_PracticeDiary_PracticeId",
                table: "PracticeDiary");

            migrationBuilder.DropIndex(
                name: "IX_Practice_GlobalPracticeId",
                table: "Practice");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StudentPracticeCharacteristicComment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StudentPracticeCharacteristic");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PracticeDiaryComment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PracticeDiary");

            migrationBuilder.RenameColumn(
                name: "GlobalPracticeId",
                table: "Practice",
                newName: "SemesterId");

            migrationBuilder.AlterColumn<int>(
                name: "Mark",
                table: "Practice",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PracticeType",
                table: "Practice",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentPracticeCharacteristic_PracticeId",
                table: "StudentPracticeCharacteristic",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticeDiary_PracticeId",
                table: "PracticeDiary",
                column: "PracticeId");
        }
    }
}
