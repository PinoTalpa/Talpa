using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talpa.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "ActivityDates",
                newName: "StartDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Suggestions",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ActivityDates",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ActivityDates");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "ActivityDates",
                newName: "Date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Suggestions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);
        }
    }
}
