using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LT.dal.Migrations
{
    /// <inheritdoc />
    public partial class AddDatetimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTimestamp",
                table: "Test",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTimestamp",
                table: "Test",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTimestamp",
                schema: "dbo",
                table: "Scores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTimestamp",
                schema: "dbo",
                table: "Scores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTimestamp",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "UpdatedTimestamp",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "CreatedTimestamp",
                schema: "dbo",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "UpdatedTimestamp",
                schema: "dbo",
                table: "Scores");
        }
    }
}
