using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAA.Migrations
{
    /// <inheritdoc />
    public partial class newFieldv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CauseId",
                table: "CorrectiveAction",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ContainmentAction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "ContainmentAction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRelevent",
                table: "Cause",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CauseId",
                table: "CorrectiveAction");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ContainmentAction");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "ContainmentAction");

            migrationBuilder.DropColumn(
                name: "IsRelevent",
                table: "Cause");
        }
    }
}
