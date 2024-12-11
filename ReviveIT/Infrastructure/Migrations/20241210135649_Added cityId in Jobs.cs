using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class AddedcityIdinJobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cityId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_cityId",
                table: "Jobs",
                column: "cityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Cities_cityId",
                table: "Jobs",
                column: "cityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Cities_cityId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_cityId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "cityId",
                table: "Jobs");
        }
    }
}
