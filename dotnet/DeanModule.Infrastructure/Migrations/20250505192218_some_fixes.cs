using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeanModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class some_fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StreamSemesters_Semesters_SemesterEntityId",
                table: "StreamSemesters");

            migrationBuilder.DropIndex(
                name: "IX_StreamSemesters_SemesterEntityId",
                table: "StreamSemesters");

            migrationBuilder.DropColumn(
                name: "SemesterEntityId",
                table: "StreamSemesters");

            migrationBuilder.CreateIndex(
                name: "IX_StreamSemesters_SemesterId",
                table: "StreamSemesters",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_StreamSemesters_Semesters_SemesterId",
                table: "StreamSemesters",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StreamSemesters_Semesters_SemesterId",
                table: "StreamSemesters");

            migrationBuilder.DropIndex(
                name: "IX_StreamSemesters_SemesterId",
                table: "StreamSemesters");

            migrationBuilder.AddColumn<Guid>(
                name: "SemesterEntityId",
                table: "StreamSemesters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StreamSemesters_SemesterEntityId",
                table: "StreamSemesters",
                column: "SemesterEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_StreamSemesters_Semesters_SemesterEntityId",
                table: "StreamSemesters",
                column: "SemesterEntityId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
