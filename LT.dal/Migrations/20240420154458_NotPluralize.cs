using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LT.dal.Migrations
{
    /// <inheritdoc />
    public partial class NotPluralize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Scores",
                schema: "dbo",
                table: "Scores");

            migrationBuilder.RenameTable(
                name: "Scores",
                schema: "dbo",
                newName: "Score");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Score",
                table: "Score",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Score",
                table: "Score");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Score",
                newName: "Scores",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scores",
                schema: "dbo",
                table: "Scores",
                column: "Id");
        }
    }
}
