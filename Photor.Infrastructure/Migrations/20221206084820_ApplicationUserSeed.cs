using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class ApplicationUserSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "4e214686-5046-4972-82a8-0d8536533e59");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dea12856-c198-4129-b3f3-b893d8395082", 0, "9b2f43e4-e624-4401-bab0-4e69dedbd365", null, "samiadmin@mail.com", false, "Samuil", "Vasilev", false, null, "SAMIADMIN@MAIL.COM", "SAMIADMIN", "AQAAAAEAACcQAAAAEH/pj6HatsXV60C3p/WqH+tTwsiQ6N4wPM7OwvtmQsxpIoq1zKoUhBuHBuTCSjbIbg==", null, false, "b1cb9af0-2805-483a-ac9d-f6633900141e", false, "SamiAdmin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "bb79a713-2457-43c7-a58e-c365d9aa7a40");
        }
    }
}
