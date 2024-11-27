using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateRecordSystem.Migrations
{
    public partial class makePreferredTimeToCallNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "PreferredTimeToCall",
                table: "Candidate",
                type: "TIME",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "TIME");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "PreferredTimeToCall",
                table: "Candidate",
                type: "TIME",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "TIME",
                oldNullable: true);
        }
    }
}
