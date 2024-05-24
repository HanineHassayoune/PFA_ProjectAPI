using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA_ProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class onetomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("0f679471-a668-46bd-914b-0b0e7b493a3b"),
                column: "EventId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Activities_EventId",
                table: "Activities",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Events_EventId",
                table: "Activities",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Events_EventId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_EventId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Activities");
        }
    }
}
