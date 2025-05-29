using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeCompanyPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPersons_Companies_CompanyId",
                table: "CompanyPersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyPersons",
                table: "CompanyPersons");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "CompanyPersons");

            migrationBuilder.RenameTable(
                name: "CompanyPersons",
                newName: "Curators");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyPersons_CompanyId",
                table: "Curators",
                newName: "IX_Curators_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Curators",
                table: "Curators",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Curators_Companies_CompanyId",
                table: "Curators",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curators_Companies_CompanyId",
                table: "Curators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Curators",
                table: "Curators");

            migrationBuilder.RenameTable(
                name: "Curators",
                newName: "CompanyPersons");

            migrationBuilder.RenameIndex(
                name: "IX_Curators_CompanyId",
                table: "CompanyPersons",
                newName: "IX_CompanyPersons_CompanyId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "CompanyPersons",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyPersons",
                table: "CompanyPersons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPersons_Companies_CompanyId",
                table: "CompanyPersons",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
