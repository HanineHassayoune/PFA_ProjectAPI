using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA_ProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class changetoenumerate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Activities",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("0f679471-a668-46bd-914b-0b0e7b493a3b"),
                column: "Status",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("0f679471-a668-46bd-914b-0b0e7b493a3b"),
                column: "Status",
                value: "The status");
        }
    }
}
