using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateRecordSystem.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PreferredTimeToCall = table.Column<TimeOnly>(type: "TIME", nullable: false),
                    LinkedInProfileUrl = table.Column<string>(type: "TEXT", nullable: false),
                    GithubUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.CandidateId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_Email",
                table: "Candidate",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidate");
        }
    }
}
