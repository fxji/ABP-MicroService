using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAA.Migrations
{
    /// <inheritdoc />
    public partial class newpropertyforIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerGroup",
                table: "Issue",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Project",
                table: "Issue",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerGroup",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "Project",
                table: "Issue");
        }
    }
}
