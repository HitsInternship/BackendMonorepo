using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelectionModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_BaseEntity_SelectionEntityId",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_SelectionEntityId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "SelectionEntityId",
                table: "BaseEntity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SelectionEntityId",
                table: "BaseEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_SelectionEntityId",
                table: "BaseEntity",
                column: "SelectionEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_BaseEntity_SelectionEntityId",
                table: "BaseEntity",
                column: "SelectionEntityId",
                principalTable: "BaseEntity",
                principalColumn: "Id");
        }
    }
}
