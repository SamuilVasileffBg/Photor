using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class ReportDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "UsersPostReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "5aaf3d3a-707b-464f-af63-da9590a3021e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17aaa7e1-f5e2-4c3a-a5e1-11cd03b8171f", "AQAAAAEAACcQAAAAEFFWK/Z7+4MFqm9+j6aishgZGsC5UZLFvJs8HG0yWZyIgl6oa4xT1JWevOp8lMSbEw==", "2b2c6760-b5c8-4848-83c7-59b3b1fd486a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "UsersPostReports");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "8b88ae38-d80b-464d-89bc-b5ac99002a33");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bb4e660-4c2b-4e97-9d1c-38f9287cb19c", "AQAAAAEAACcQAAAAEH6fy+iKSnH8DAA+j3MZYdHm7FcKJ4fGf1N4qb32u8hXxe9j+A+fTE38B8xs9h5HUw==", "dbf4da2a-9cd8-4392-b563-79b294c5f1a7" });
        }
    }
}
