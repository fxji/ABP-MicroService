using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAA.Migrations
{
    /// <inheritdoc />
    public partial class usestringstoredict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "A3");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "A3");

            migrationBuilder.AddColumn<string>(
                name: "Process",
                table: "A3",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "A3",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Process",
                table: "A3");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "A3");

            migrationBuilder.AddColumn<Guid>(
                name: "ProcessId",
                table: "A3",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "A3",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
