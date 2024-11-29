using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 26, 14, 28, 56, 159, DateTimeKind.Utc).AddTicks(3317), new DateTime(2024, 11, 26, 14, 28, 56, 159, DateTimeKind.Utc).AddTicks(3318) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 26, 14, 28, 56, 159, DateTimeKind.Utc).AddTicks(3320), new DateTime(2024, 11, 26, 14, 28, 56, 159, DateTimeKind.Utc).AddTicks(3320) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 26, 14, 28, 56, 159, DateTimeKind.Utc).AddTicks(3322), new DateTime(2024, 11, 26, 14, 28, 56, 159, DateTimeKind.Utc).AddTicks(3322) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 26, 14, 28, 56, 159, DateTimeKind.Utc).AddTicks(3324), new DateTime(2024, 11, 26, 14, 28, 56, 159, DateTimeKind.Utc).AddTicks(3324) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 25, 15, 2, 41, 459, DateTimeKind.Utc).AddTicks(3237), new DateTime(2024, 11, 25, 15, 2, 41, 459, DateTimeKind.Utc).AddTicks(3238) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 25, 15, 2, 41, 459, DateTimeKind.Utc).AddTicks(3239), new DateTime(2024, 11, 25, 15, 2, 41, 459, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 25, 15, 2, 41, 459, DateTimeKind.Utc).AddTicks(3241), new DateTime(2024, 11, 25, 15, 2, 41, 459, DateTimeKind.Utc).AddTicks(3241) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 25, 15, 2, 41, 459, DateTimeKind.Utc).AddTicks(3243), new DateTime(2024, 11, 25, 15, 2, 41, 459, DateTimeKind.Utc).AddTicks(3243) });
        }
    }
}
