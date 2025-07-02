using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tablestudentsrenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SStudents_Groups_GroupId",
                table: "SStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SStudents",
                table: "SStudents");

            migrationBuilder.RenameTable(
                name: "SStudents",
                newName: "Students");

            migrationBuilder.RenameIndex(
                name: "IX_SStudents_GroupId",
                table: "Students",
                newName: "IX_Students_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "SStudents");

            migrationBuilder.RenameIndex(
                name: "IX_Students_GroupId",
                table: "SStudents",
                newName: "IX_SStudents_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SStudents",
                table: "SStudents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SStudents_Groups_GroupId",
                table: "SStudents",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
