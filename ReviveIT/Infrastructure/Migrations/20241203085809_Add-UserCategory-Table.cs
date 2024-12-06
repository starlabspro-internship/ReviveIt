using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCategories",
                columns: table => new
                {
                    UserCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategories", x => x.UserCategoryId);
                    table.ForeignKey(
                        name: "FK_UserCategories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9075), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9077) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9084), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9086) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9091), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9092) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9097), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9099) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9104), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9105) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9110), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9112) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9117), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9119) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9173), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9175) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9179), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9181) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9186), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9187) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9192), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9194) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9198), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9200) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9205), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9206) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9211), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9212) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9217), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9218) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9224), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9226) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9230), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9232) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9237), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9238) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9242), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9244) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9249), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9250) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9255), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9256) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9261), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9262) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9267), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9268) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9273), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9274) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 25,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9279), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9281) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 26,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9286), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9287) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 27,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9292), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9293) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 28,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9298), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9299) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 29,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9304), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9305) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 30,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9309), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9311) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 31,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9315), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9317) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 32,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9321), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9323) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 33,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9327), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9329) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 34,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9333), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9335) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 35,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9340), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9341) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 36,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9346), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9347) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 37,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9352), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9353) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 38,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9358), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9359) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 39,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9364), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9365) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 40,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9370), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9371) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 41,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9375), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9377) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 42,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9381), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9383) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 43,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9387), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9389) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 44,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9394), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9395) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 45,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9399), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9401) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 46,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9406), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9407) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 47,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9411), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9413) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 48,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9417), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9419) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 49,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9423), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9424) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 50,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9429), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9430) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 51,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9434), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9436) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 52,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9440), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9442) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 53,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9446), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9448) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 54,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9452), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9454) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 55,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9458), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9460) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 56,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9464), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9466) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 57,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9470), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9471) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 58,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9476), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9478) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 59,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9482), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9483) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 60,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9488), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9489) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 61,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9494), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9495) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 62,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9500), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9501) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 63,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9506), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9507) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 64,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9511), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9513) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 65,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9517), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9519) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 66,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9523), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9525) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 67,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9529), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9531) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 68,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9535), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9536) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 69,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9541), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9542) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 70,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9547), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9548) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 71,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9552), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9554) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 72,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9558), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9559) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 73,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9563), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9565) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 74,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9569), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9571) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 75,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9575), new DateTime(2024, 12, 3, 8, 58, 6, 764, DateTimeKind.Utc).AddTicks(9576) });

            migrationBuilder.CreateIndex(
                name: "IX_UserCategories_CategoryId",
                table: "UserCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategories_UserId",
                table: "UserCategories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCategories");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9286), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9286) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9288), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9289) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9290), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9291) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9292), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9292) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9294), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9294) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9296), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9296) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9297), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9298) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9299), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9299) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9301), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9301) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9303), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9303) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9304), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9305) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9306), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9306) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9307), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9308) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9309), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9310) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9311), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9311) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9313), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9313) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9314), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9315) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9316), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9316) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9318), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9318) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9319), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9320) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9321), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9321) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9323), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9323) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9324), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9325) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9326), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9326) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 25,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9328) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 26,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9329), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9330) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 27,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9331), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9332) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 28,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9333), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9333) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 29,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9335), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9336) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 30,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9337), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9337) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 31,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9338), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9339) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 32,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9340), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9341) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 33,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9342), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9342) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 34,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9343), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9344) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 35,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9345), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9345) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 36,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9347), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9347) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 37,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9348), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9349) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 38,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9350), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9350) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 39,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9351), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9352) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 40,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9353), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9353) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 41,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9355), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9355) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 42,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9356), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9357) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 43,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9358), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9358) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 44,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9360), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9360) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 45,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9361), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9361) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 46,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9363), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9363) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 47,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9364), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9365) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 48,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9366), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9366) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 49,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9367), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9368) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 50,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9369), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9369) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 51,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9371), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9371) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 52,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9372), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9372) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 53,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9374), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9374) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 54,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9375), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9376) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 55,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9423), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9424) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 56,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9425), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9426) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 57,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9427), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9427) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 58,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9428), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9429) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 59,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9430), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9430) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 60,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9432), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9432) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 61,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9433), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9434) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 62,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9435), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9435) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 63,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9437), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9437) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 64,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9438), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9439) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 65,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9440), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9440) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 66,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9441), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9442) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 67,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9443), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9443) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 68,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9445), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9445) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 69,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9446), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9447) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 70,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9448), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9448) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 71,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9450), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9450) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 72,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9451), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9452) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 73,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9453), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9453) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 74,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9455), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9455) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 75,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9456), new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9456) });
        }
    }
}
