using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class PortfolioFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PortfolioDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    FileType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioDocuments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 15, 5, 251, DateTimeKind.Utc).AddTicks(658), new DateTime(2024, 12, 2, 14, 15, 5, 251, DateTimeKind.Utc).AddTicks(659) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 15, 5, 251, DateTimeKind.Utc).AddTicks(660), new DateTime(2024, 12, 2, 14, 15, 5, 251, DateTimeKind.Utc).AddTicks(661) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 15, 5, 251, DateTimeKind.Utc).AddTicks(662), new DateTime(2024, 12, 2, 14, 15, 5, 251, DateTimeKind.Utc).AddTicks(663) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 15, 5, 251, DateTimeKind.Utc).AddTicks(664), new DateTime(2024, 12, 2, 14, 15, 5, 251, DateTimeKind.Utc).AddTicks(664) });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioDocuments_UserId",
                table: "PortfolioDocuments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioDocuments");

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
