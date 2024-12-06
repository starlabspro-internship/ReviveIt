using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class SelectedJobApplicants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelectedJobApplicants",
                columns: table => new
                {
                    SelectedApplicantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelectedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobID = table.Column<int>(type: "int", nullable: false),
                    ApplicationID = table.Column<int>(type: "int", nullable: false),
                    SelectedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedJobApplicants", x => x.SelectedApplicantID);
                    table.ForeignKey(
                        name: "FK_SelectedJobApplicants_AspNetUsers_SelectedByUserId",
                        column: x => x.SelectedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SelectedJobApplicants_JobApplications_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "JobApplications",
                        principalColumn: "ApplicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelectedJobApplicants_Jobs_JobID",
                        column: x => x.JobID,
                        principalTable: "Jobs",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 5, 9, 24, 33, 681, DateTimeKind.Utc).AddTicks(2416), new DateTime(2024, 12, 5, 9, 24, 33, 681, DateTimeKind.Utc).AddTicks(2417) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 5, 9, 24, 33, 681, DateTimeKind.Utc).AddTicks(2419), new DateTime(2024, 12, 5, 9, 24, 33, 681, DateTimeKind.Utc).AddTicks(2419) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 5, 9, 24, 33, 681, DateTimeKind.Utc).AddTicks(2421), new DateTime(2024, 12, 5, 9, 24, 33, 681, DateTimeKind.Utc).AddTicks(2422) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 5, 9, 24, 33, 681, DateTimeKind.Utc).AddTicks(2423), new DateTime(2024, 12, 5, 9, 24, 33, 681, DateTimeKind.Utc).AddTicks(2424) });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedJobApplicants_ApplicationID",
                table: "SelectedJobApplicants",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedJobApplicants_JobID",
                table: "SelectedJobApplicants",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedJobApplicants_SelectedByUserId",
                table: "SelectedJobApplicants",
                column: "SelectedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedJobApplicants");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 22, 1, 54, 934, DateTimeKind.Utc).AddTicks(986), new DateTime(2024, 12, 3, 22, 1, 54, 934, DateTimeKind.Utc).AddTicks(987) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 22, 1, 54, 934, DateTimeKind.Utc).AddTicks(989), new DateTime(2024, 12, 3, 22, 1, 54, 934, DateTimeKind.Utc).AddTicks(990) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 22, 1, 54, 934, DateTimeKind.Utc).AddTicks(992), new DateTime(2024, 12, 3, 22, 1, 54, 934, DateTimeKind.Utc).AddTicks(993) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 22, 1, 54, 934, DateTimeKind.Utc).AddTicks(995), new DateTime(2024, 12, 3, 22, 1, 54, 934, DateTimeKind.Utc).AddTicks(995) });
        }
    }
}
