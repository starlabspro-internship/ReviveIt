using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class ReviewTableChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Services_ServiceID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ServiceID",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ServiceID",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "ReviewedUserId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewedUserId",
                table: "Reviews",
                column: "ReviewedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewedUserId",
                table: "Reviews",
                column: "ReviewedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewedUserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReviewedUserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewedUserId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "ServiceID",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ServiceID",
                table: "Reviews",
                column: "ServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Services_ServiceID",
                table: "Reviews",
                column: "ServiceID",
                principalTable: "Services",
                principalColumn: "ServiceID");
        }
    }
}
