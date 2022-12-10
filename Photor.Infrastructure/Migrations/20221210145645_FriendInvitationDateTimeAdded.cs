using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class FriendInvitationDateTimeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "FriendInvitations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                columns: new[] { "ConcurrencyStamp", "ImageUrl", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b6c24ea7-6e0f-4fc3-b5f6-b0b5ccb15e53", "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey", "AQAAAAEAACcQAAAAEMO1y0eN1QWHkX2LvGnkgTBNjHLmW91AuKuyfrFwDCB5TOp4NX0XtSJfPXn6x1fSvw==", "79d1d0e8-71de-456b-a6a3-0cd06432de7b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "FriendInvitations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "f5c20fd5-9f36-4442-b958-aa1bca9cce30");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "ImageUrl", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd691ad2-2a65-4fb0-b5f7-c8423dc6e1b3", "https://preview.redd.it/h5gnz1ji36o61.png?width=225&format=png&auto=webp&s=84379f8d3bbe593a2e863c438cd03e84c8a474fa", "AQAAAAEAACcQAAAAEDUjfXJhMcEwaRL9yBeMj+qeDrBkfQj43fApLro+02vwlyPowhIPHt4/gI5KhqhU4g==", "d58e0f23-0d0d-4ed2-a785-4553c96eaf55" });
        }
    }
}
