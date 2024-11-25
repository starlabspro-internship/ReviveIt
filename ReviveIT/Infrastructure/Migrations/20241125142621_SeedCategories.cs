using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 25, 14, 26, 21, 408, DateTimeKind.Utc).AddTicks(5373), "Electronics Repair", new DateTime(2024, 11, 25, 14, 26, 21, 408, DateTimeKind.Utc).AddTicks(5373) },
                    { 2, new DateTime(2024, 11, 25, 14, 26, 21, 408, DateTimeKind.Utc).AddTicks(5375), "Furniture Restoration", new DateTime(2024, 11, 25, 14, 26, 21, 408, DateTimeKind.Utc).AddTicks(5375) },
                    { 3, new DateTime(2024, 11, 25, 14, 26, 21, 408, DateTimeKind.Utc).AddTicks(5377), "Home Appliance Repair", new DateTime(2024, 11, 25, 14, 26, 21, 408, DateTimeKind.Utc).AddTicks(5377) },
                    { 4, new DateTime(2024, 11, 25, 14, 26, 21, 408, DateTimeKind.Utc).AddTicks(5379), "Automotive Repair", new DateTime(2024, 11, 25, 14, 26, 21, 408, DateTimeKind.Utc).AddTicks(5379) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4);
        }
    }
}
