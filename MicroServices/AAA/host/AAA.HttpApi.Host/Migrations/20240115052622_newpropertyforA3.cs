using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAA.Migrations
{
    /// <inheritdoc />
    public partial class newpropertyforA3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "A3",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "A3");
        }
    }
}
