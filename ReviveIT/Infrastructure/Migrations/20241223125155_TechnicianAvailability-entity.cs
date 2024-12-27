using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class TechnicianAvailabilityentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechnicianAvailabilities",
                columns: table => new
                {
                    AvailabilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnicianId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DaysAvailable = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MonthsUnavailable = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SpecificUnavailableDates = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianAvailabilities", x => x.AvailabilityID);
                    table.ForeignKey(
                        name: "FK_TechnicianAvailabilities_AspNetUsers_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianAvailabilities_TechnicianId",
                table: "TechnicianAvailabilities",
                column: "TechnicianId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicianAvailabilities");
        }
    }
}
