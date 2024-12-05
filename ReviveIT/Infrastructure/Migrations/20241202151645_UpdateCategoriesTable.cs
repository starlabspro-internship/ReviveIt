using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 16, 43, 251, DateTimeKind.Utc).AddTicks(9117), new DateTime(2024, 12, 2, 15, 16, 43, 251, DateTimeKind.Utc).AddTicks(9118) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 16, 43, 251, DateTimeKind.Utc).AddTicks(9123), new DateTime(2024, 12, 2, 15, 16, 43, 251, DateTimeKind.Utc).AddTicks(9124) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 16, 43, 251, DateTimeKind.Utc).AddTicks(9129), new DateTime(2024, 12, 2, 15, 16, 43, 251, DateTimeKind.Utc).AddTicks(9130) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 16, 43, 251, DateTimeKind.Utc).AddTicks(9135), new DateTime(2024, 12, 2, 15, 16, 43, 251, DateTimeKind.Utc).AddTicks(9136) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
