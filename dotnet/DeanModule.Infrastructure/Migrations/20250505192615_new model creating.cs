using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeanModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newmodelcreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StreamSemesters_Semesters_SemesterId",
                table: "StreamSemesters");

            migrationBuilder.AddForeignKey(
                name: "FK_StreamSemesters_Semesters_SemesterId",
                table: "StreamSemesters",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StreamSemesters_Semesters_SemesterId",
                table: "StreamSemesters");

            migrationBuilder.AddForeignKey(
                name: "FK_StreamSemesters_Semesters_SemesterId",
                table: "StreamSemesters",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
