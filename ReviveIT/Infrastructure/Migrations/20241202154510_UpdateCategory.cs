using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 5, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9294), "Plumbing Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9294) },
                    { 6, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9296), "Electrical Repairs", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9296) },
                    { 7, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9297), "Cleaning Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9298) },
                    { 8, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9299), "Carpentry", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9299) },
                    { 9, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9301), "Landscaping", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9301) },
                    { 10, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9303), "Painting", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9303) },
                    { 11, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9304), "Roofing", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9305) },
                    { 12, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9306), "HVAC Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9306) },
                    { 13, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9307), "Pest Control", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9308) },
                    { 14, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9309), "Moving Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9310) },
                    { 15, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9311), "Interior Design", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9311) },
                    { 16, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9313), "IT Support", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9313) },
                    { 17, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9314), "Handyman Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9315) },
                    { 18, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9316), "Masonry", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9316) },
                    { 19, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9318), "Welding", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9318) },
                    { 20, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9319), "Security Systems Installation", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9320) },
                    { 21, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9321), "Window Installation", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9321) },
                    { 22, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9323), "Flooring Installation", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9323) },
                    { 23, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9324), "Bathroom Remodeling", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9325) },
                    { 24, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9326), "Kitchen Remodeling", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9326) },
                    { 25, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9328), "Solar Panel Installation", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9328) },
                    { 26, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9329), "Tree Trimming", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9330) },
                    { 27, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9331), "Pool Maintenance", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9332) },
                    { 28, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9333), "Locksmith Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9333) },
                    { 29, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9335), "Event Planning", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9336) },
                    { 30, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9337), "Photography", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9337) },
                    { 31, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9338), "Tutoring", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9339) },
                    { 32, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9340), "Courier Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9341) },
                    { 33, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9342), "Legal Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9342) },
                    { 34, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9343), "Accounting Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9344) },
                    { 35, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9345), "Health and Fitness", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9345) },
                    { 36, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9347), "Child Care", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9347) },
                    { 37, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9348), "Elderly Care", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9349) },
                    { 38, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9350), "Pressure Washing", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9350) },
                    { 39, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9351), "Junk Removal", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9352) },
                    { 40, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9353), "Commercial Cleaning", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9353) },
                    { 41, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9355), "Digital Marketing", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9355) },
                    { 42, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9356), "SEO Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9357) },
                    { 43, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9358), "Social Media Management", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9358) },
                    { 44, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9360), "Web Development", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9360) },
                    { 45, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9361), "Graphic Design", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9361) },
                    { 46, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9363), "Content Writing", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9363) },
                    { 47, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9364), "Video Editing", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9365) },
                    { 48, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9366), "3D Printing", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9366) },
                    { 49, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9367), "Custom Software Development", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9368) },
                    { 50, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9369), "Mobile App Development", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9369) },
                    { 51, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9371), "Photography Editing", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9371) },
                    { 52, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9372), "Data Entry Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9372) },
                    { 53, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9374), "Virtual Assistance", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9374) },
                    { 54, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9375), "Business Consulting", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9376) },
                    { 55, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9423), "Market Research", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9424) },
                    { 56, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9425), "Project Management", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9426) },
                    { 57, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9427), "Branding", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9427) },
                    { 58, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9428), "Event Coordination", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9429) },
                    { 59, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9430), "Public Relations", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9430) },
                    { 60, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9432), "Translation Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9432) },
                    { 61, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9433), "Voiceover Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9434) },
                    { 62, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9435), "Legal Consultation", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9435) },
                    { 63, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9437), "Property Management", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9437) },
                    { 64, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9438), "Real Estate Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9439) },
                    { 65, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9440), "Insurance Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9440) },
                    { 66, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9441), "Financial Planning", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9442) },
                    { 67, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9443), "Investment Advice", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9443) },
                    { 68, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9445), "Tax Preparation", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9445) },
                    { 69, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9446), "Debt Counseling", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9447) },
                    { 70, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9448), "Retirement Planning", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9448) },
                    { 71, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9450), "Mortgage Advice", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9450) },
                    { 72, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9451), "Estate Planning", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9452) },
                    { 73, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9453), "Human Resources", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9453) },
                    { 74, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9455), "Recruitment Services", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9455) },
                    { 75, new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9456), "Employee Training", new DateTime(2024, 12, 2, 15, 45, 10, 278, DateTimeKind.Utc).AddTicks(9456) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 75);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 32, 28, 334, DateTimeKind.Utc).AddTicks(9863), new DateTime(2024, 12, 2, 15, 32, 28, 334, DateTimeKind.Utc).AddTicks(9863) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 32, 28, 334, DateTimeKind.Utc).AddTicks(9865), new DateTime(2024, 12, 2, 15, 32, 28, 334, DateTimeKind.Utc).AddTicks(9865) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 32, 28, 334, DateTimeKind.Utc).AddTicks(9867), new DateTime(2024, 12, 2, 15, 32, 28, 334, DateTimeKind.Utc).AddTicks(9867) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 32, 28, 334, DateTimeKind.Utc).AddTicks(9869), new DateTime(2024, 12, 2, 15, 32, 28, 334, DateTimeKind.Utc).AddTicks(9869) });
        }
    }
}
