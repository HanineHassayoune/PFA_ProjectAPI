using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA_ProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateattributesevent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pictures",
                table: "Activities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pictures",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("0f679471-a668-46bd-914b-0b0e7b493a3b"),
                column: "Pictures",
                value: "The pictures");
        }
    }
}
