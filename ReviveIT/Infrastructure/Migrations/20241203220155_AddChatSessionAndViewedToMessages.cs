using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class AddChatSessionAndViewedToMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatSessionId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Viewed",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ChatSessions",
                columns: table => new
                {
                    ChatSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnicianId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSessions", x => x.ChatSessionId);
                    table.ForeignKey(
                        name: "FK_ChatSessions_AspNetUsers_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatSessions_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatSessions_AspNetUsers_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatSessionId",
                table: "Messages",
                column: "ChatSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSessions_CompanyId",
                table: "ChatSessions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSessions_CustomerId",
                table: "ChatSessions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSessions_TechnicianId",
                table: "ChatSessions",
                column: "TechnicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ChatSessions_ChatSessionId",
                table: "Messages",
                column: "ChatSessionId",
                principalTable: "ChatSessions",
                principalColumn: "ChatSessionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ChatSessions_ChatSessionId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "ChatSessions");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatSessionId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ChatSessionId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Viewed",
                table: "Messages");

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
        }
    }
}
