using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAA.Migrations
{
    /// <inheritdoc />
    public partial class renametitletoname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activites",
                table: "CorrectiveAction");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ContainmentAction");

            migrationBuilder.RenameColumn(
                name: "Factor",
                table: "RiskAssessment",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_RiskAssessment_Factor",
                table: "RiskAssessment",
                newName: "IX_RiskAssessment_Name");

            migrationBuilder.RenameColumn(
                name: "Activities",
                table: "ContainmentAction",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_ContainmentAction_Activities",
                table: "ContainmentAction",
                newName: "IX_ContainmentAction_Name");

            migrationBuilder.AlterColumn<Guid>(
                name: "Probability",
                table: "RiskAssessment",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Functionally",
                table: "RiskAssessment",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "A3Id",
                table: "RiskAssessment",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Status",
                table: "CorrectiveAction",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "A3Id",
                table: "CorrectiveAction",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Type",
                table: "Cause",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Status",
                table: "Cause",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "Cause",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "A3Id",
                table: "Cause",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RiskAssessment",
                newName: "Factor");

            migrationBuilder.RenameIndex(
                name: "IX_RiskAssessment_Name",
                table: "RiskAssessment",
                newName: "IX_RiskAssessment_Factor");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ContainmentAction",
                newName: "Activities");

            migrationBuilder.RenameIndex(
                name: "IX_ContainmentAction_Name",
                table: "ContainmentAction",
                newName: "IX_ContainmentAction_Activities");

            migrationBuilder.AlterColumn<Guid>(
                name: "Probability",
                table: "RiskAssessment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Functionally",
                table: "RiskAssessment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "A3Id",
                table: "RiskAssessment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Status",
                table: "CorrectiveAction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "A3Id",
                table: "CorrectiveAction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Activites",
                table: "CorrectiveAction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ContainmentAction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "Type",
                table: "Cause",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Status",
                table: "Cause",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "Cause",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "A3Id",
                table: "Cause",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
