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
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSessions", x => x.ChatSessionId);
                    table.ForeignKey(
                        name: "FK_ChatSessions_AspNetUsers_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatSessions_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatSessions_AspNetUsers_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 20, 32, 1, 927, DateTimeKind.Utc).AddTicks(4806), new DateTime(2024, 12, 3, 20, 32, 1, 927, DateTimeKind.Utc).AddTicks(4807) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 20, 32, 1, 927, DateTimeKind.Utc).AddTicks(4809), new DateTime(2024, 12, 3, 20, 32, 1, 927, DateTimeKind.Utc).AddTicks(4809) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 20, 32, 1, 927, DateTimeKind.Utc).AddTicks(4811), new DateTime(2024, 12, 3, 20, 32, 1, 927, DateTimeKind.Utc).AddTicks(4812) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 20, 32, 1, 927, DateTimeKind.Utc).AddTicks(4814), new DateTime(2024, 12, 3, 20, 32, 1, 927, DateTimeKind.Utc).AddTicks(4814) });

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
