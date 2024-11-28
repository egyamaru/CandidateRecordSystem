using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateRecordSystem.Migrations
{
    public partial class changePrefferedTimeDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PreferredTimeToCall",
                table: "Candidate",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "TIME",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "PreferredTimeToCall",
                table: "Candidate",
                type: "TIME",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
