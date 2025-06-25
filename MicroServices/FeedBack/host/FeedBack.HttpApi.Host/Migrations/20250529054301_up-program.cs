using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedBack.Migrations
{
    /// <inheritdoc />
    public partial class upprogram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedBackProgramInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Windows = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBackProgramInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedBackShapeInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Line = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodWindows = table.Column<int>(type: "int", nullable: false),
                    SlipWindows = table.Column<int>(type: "int", nullable: false),
                    FailureWindows = table.Column<int>(type: "int", nullable: false),
                    HasChanged = table.Column<bool>(type: "bit", nullable: false),
                    HasError = table.Column<bool>(type: "bit", nullable: false),
                    Cause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBackShapeInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedBackShapeInfo_FeedBackProgramInfo_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "FeedBackProgramInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedBackShapeInfo_ProgramId",
                table: "FeedBackShapeInfo",
                column: "ProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedBackShapeInfo");

            migrationBuilder.DropTable(
                name: "FeedBackProgramInfo");
        }
    }
}
