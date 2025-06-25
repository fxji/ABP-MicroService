using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedBack.Migrations
{
    /// <inheritdoc />
    public partial class updateprogram2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedBackShapeInfo_FeedBackProgramInfo_ProgramId",
                table: "FeedBackShapeInfo");

            migrationBuilder.DropIndex(
                name: "IX_FeedBackShapeInfo_ProgramId",
                table: "FeedBackShapeInfo");

            migrationBuilder.RenameColumn(
                name: "Windows",
                table: "FeedBackProgramInfo",
                newName: "Slip");

            migrationBuilder.AddColumn<int>(
                name: "Failure",
                table: "FeedBackProgramInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Good",
                table: "FeedBackProgramInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Failure",
                table: "FeedBackProgramInfo");

            migrationBuilder.DropColumn(
                name: "Good",
                table: "FeedBackProgramInfo");

            migrationBuilder.RenameColumn(
                name: "Slip",
                table: "FeedBackProgramInfo",
                newName: "Windows");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBackShapeInfo_ProgramId",
                table: "FeedBackShapeInfo",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedBackShapeInfo_FeedBackProgramInfo_ProgramId",
                table: "FeedBackShapeInfo",
                column: "ProgramId",
                principalTable: "FeedBackProgramInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
