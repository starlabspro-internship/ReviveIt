using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCityrelatedtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "OperatingCities",
                columns: table => new
                {
                    OperatingCityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingCities", x => x.OperatingCityId);
                    table.ForeignKey(
                        name: "FK_OperatingCities_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperatingCities_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CityName" },
                values: new object[,]
                {
                    { 1, "Deçan" },
                    { 2, "Dragash" },
                    { 3, "Drenas" },
                    { 4, "Ferizaj" },
                    { 5, "Fushë Kosovë" },
                    { 6, "Gjakovë" },
                    { 7, "Gjilan" },
                    { 8, "Istog" },
                    { 9, "Kaçanik" },
                    { 10, "Kamenicë" },
                    { 11, "Klinë" },
                    { 12, "Lipjan" },
                    { 13, "Malishevë" },
                    { 14, "Mitrovicë" },
                    { 15, "Obiliq" },
                    { 16, "Pejë" },
                    { 17, "Podujevë" },
                    { 18, "Prishtinë" },
                    { 19, "Prizren" },
                    { 20, "Rahovec" },
                    { 21, "Skenderaj" },
                    { 22, "Suharekë" },
                    { 23, "Shtërpcë" },
                    { 24, "Shtime" },
                    { 25, "Viti" },
                    { 26, "Vushtrri" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperatingCities_CityId",
                table: "OperatingCities",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatingCities_userId",
                table: "OperatingCities",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperatingCities");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
