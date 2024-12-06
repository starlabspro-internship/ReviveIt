using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class AddSelectedJobApplicantWithUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SelectedApplicantUserId",
                table: "SelectedJobApplicants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 5, 9, 46, 55, 178, DateTimeKind.Utc).AddTicks(2809), new DateTime(2024, 12, 5, 9, 46, 55, 178, DateTimeKind.Utc).AddTicks(2809) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 5, 9, 46, 55, 178, DateTimeKind.Utc).AddTicks(2811), new DateTime(2024, 12, 5, 9, 46, 55, 178, DateTimeKind.Utc).AddTicks(2812) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 5, 9, 46, 55, 178, DateTimeKind.Utc).AddTicks(2814), new DateTime(2024, 12, 5, 9, 46, 55, 178, DateTimeKind.Utc).AddTicks(2814) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 5, 9, 46, 55, 178, DateTimeKind.Utc).AddTicks(2816), new DateTime(2024, 12, 5, 9, 46, 55, 178, DateTimeKind.Utc).AddTicks(2816) });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedJobApplicants_SelectedApplicantUserId",
                table: "SelectedJobApplicants",
                column: "SelectedApplicantUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedJobApplicants_AspNetUsers_SelectedApplicantUserId",
                table: "SelectedJobApplicants",
                column: "SelectedApplicantUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedJobApplicants_AspNetUsers_SelectedApplicantUserId",
                table: "SelectedJobApplicants");

            migrationBuilder.DropIndex(
                name: "IX_SelectedJobApplicants_SelectedApplicantUserId",
                table: "SelectedJobApplicants");

            migrationBuilder.DropColumn(
                name: "SelectedApplicantUserId",
                table: "SelectedJobApplicants");

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
        }
    }
}
