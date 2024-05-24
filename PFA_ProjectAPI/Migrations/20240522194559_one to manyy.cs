using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA_ProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class onetomanyy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("0f679471-a668-46bd-914b-0b0e7b493a3b"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Animator", "Description", "EndDate", "EventId", "StartDate", "Status", "TeamBuilding", "Title" },
                values: new object[] { new Guid("0f679471-a668-46bd-914b-0b0e7b493a3b"), "The animator", "Les descriptions ", "The end date ", new Guid("00000000-0000-0000-0000-000000000000"), "The start date ", 0, "The tembuilding", "The first title" });
        }
    }
}
