using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class sd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "145bdb34-18fa-4644-b002-6e4a02147b77",
                column: "ConcurrencyStamp",
                value: "d775baf7-e3fd-4ae4-a7e8-8306abdd3703");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "e773f69a-ca3d-4913-9350-224eaeff08ec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "683cd19b-7a34-4cc8-b3cc-64547e4f125f",
                column: "ConcurrencyStamp",
                value: "80f64c80-f95b-4b98-a8ab-3dba46d95d41");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e74c299a-4742-49d5-b2d5-45b00faf6a11", "AQAAAAEAACcQAAAAEMOo7HqifT4D44xVegCJXvEdOzkA9j69kGMWTXBQ0U7FgyhrWEyt/JGBotLFR6gvHA==", "2e03d1d5-19bd-4237-868a-ddb98e3c5089" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d839508M",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "201bf987-20eb-4c82-9e4d-ac57b7301f6d", "AQAAAAEAACcQAAAAEKzhCA9phAJ9+b/XfAdD5y453VgZ4NC5HLE+YW79RVMKXKM3OcF7YnWZy2sSl4ZIiQ==", "2d7dfad9-1994-4cc2-b938-b440b6886c57" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d839508U",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fa110c89-761e-4080-9585-674bf27be16d", "AQAAAAEAACcQAAAAEDUpAj+hpyKo9lgEQh54tpFQN7Ey8DpcIQ0GUfPORvqZvKQU52NFGFlK4R/Pe8z8lQ==", "8dca05f4-c3cc-41e6-a8de-285afcb38252" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "145bdb34-18fa-4644-b002-6e4a02147b77",
                column: "ConcurrencyStamp",
                value: "c4c8155e-536d-4894-ab26-a83c8861ab17");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "dc4e637b-2b41-4df0-a023-54cc3d68159c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "683cd19b-7a34-4cc8-b3cc-64547e4f125f",
                column: "ConcurrencyStamp",
                value: "949d7f51-61cb-412f-958d-54acd3a51e7d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd35ea2d-5cc1-4b57-82ce-7b4a49a3e060", "AQAAAAEAACcQAAAAEKo5IOwXintcZliA8vWjsUUnF9ihD7H+s+O+LC24MyBBgmf9Eih+tDwFfVyTx4BtaQ==", "fd7d5671-2569-4723-9bae-572d41db61d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d839508M",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb9694e9-49c0-4126-82d7-dc7b42163c29", "AQAAAAEAACcQAAAAEAoK2KbFzf/jRMkyIFb9mLmnKRqBlIG/7WcBrWQv1PLMXkLb2fSabPL9rcLz/mKmHg==", "5ba7e3a6-0495-4fa9-ac26-a95b8449022e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d839508U",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b052c419-5588-4d47-bb89-f06444c68359", "AQAAAAEAACcQAAAAEBU99Pjrv6VDj5+PSceEbeKeOtsI+jiD2VFgSTVh4kv6uq7q/Iw58xiLbAls4mVQZg==", "6722f58b-6a14-4a56-8c29-38f21bb23117" });
        }
    }
}
