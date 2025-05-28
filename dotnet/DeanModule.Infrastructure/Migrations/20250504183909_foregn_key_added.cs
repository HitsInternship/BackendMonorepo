using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeanModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class foregn_key_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
