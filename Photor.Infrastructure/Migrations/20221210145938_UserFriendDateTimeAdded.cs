using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class UserFriendDateTimeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "UsersFriends",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "da2e43b5-d1b0-4ad2-a69b-7cdfeed8f8ef");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d08033c8-bd1d-47dd-a924-9b8e0379f638", "AQAAAAEAACcQAAAAENJOEp5RUUHOislWN5ttjIk/DOkkICY9VrgQ5go6pRBgt96Z2BnvDcfsTOkOTzBCHw==", "3143b642-3d4a-40a4-9849-bed4c972e810" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "UsersFriends");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "afc3caa4-74e6-4ebf-92ee-f09502a698d1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b6c24ea7-6e0f-4fc3-b5f6-b0b5ccb15e53", "AQAAAAEAACcQAAAAEMO1y0eN1QWHkX2LvGnkgTBNjHLmW91AuKuyfrFwDCB5TOp4NX0XtSJfPXn6x1fSvw==", "79d1d0e8-71de-456b-a6a3-0cd06432de7b" });
        }
    }
}
