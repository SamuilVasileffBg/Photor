using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class UserRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "8b88ae38-d80b-464d-89bc-b5ac99002a33");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bb4e660-4c2b-4e97-9d1c-38f9287cb19c", "AQAAAAEAACcQAAAAEH6fy+iKSnH8DAA+j3MZYdHm7FcKJ4fGf1N4qb32u8hXxe9j+A+fTE38B8xs9h5HUw==", "dbf4da2a-9cd8-4392-b563-79b294c5f1a7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "4e214686-5046-4972-82a8-0d8536533e59");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b2f43e4-e624-4401-bab0-4e69dedbd365", "AQAAAAEAACcQAAAAEH/pj6HatsXV60C3p/WqH+tTwsiQ6N4wPM7OwvtmQsxpIoq1zKoUhBuHBuTCSjbIbg==", "b1cb9af0-2805-483a-ac9d-f6633900141e" });
        }
    }
}
