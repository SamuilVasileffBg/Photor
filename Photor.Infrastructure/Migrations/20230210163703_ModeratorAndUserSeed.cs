using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class ModeratorAndUserSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "FirstName", "ImageUrl", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "dea12856-c198-4129-b3f3-b893d839508M", 0, "cb9694e9-49c0-4126-82d7-dc7b42163c29", null, "samimoderator@mail.com", false, "Samuil", "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey", "Moderatorov", false, null, "SAMIMODERATOR@MAIL.COM", "SAMIMODERATOR", "AQAAAAEAACcQAAAAEAoK2KbFzf/jRMkyIFb9mLmnKRqBlIG/7WcBrWQv1PLMXkLb2fSabPL9rcLz/mKmHg==", null, false, "5ba7e3a6-0495-4fa9-ac26-a95b8449022e", false, "SamiModerator" },
                    { "dea12856-c198-4129-b3f3-b893d839508U", 0, "b052c419-5588-4d47-bb89-f06444c68359", null, "samiuser@mail.com", false, "Samuil", "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey", "Userov", false, null, "SAMIUSER@MAIL.COM", "SAMIUSER", "AQAAAAEAACcQAAAAEBU99Pjrv6VDj5+PSceEbeKeOtsI+jiD2VFgSTVh4kv6uq7q/Iw58xiLbAls4mVQZg==", null, false, "6722f58b-6a14-4a56-8c29-38f21bb23117", false, "SamiUser" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "145bdb34-18fa-4644-b002-6e4a02147b77", "dea12856-c198-4129-b3f3-b893d839508M" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "683cd19b-7a34-4cc8-b3cc-64547e4f125f", "dea12856-c198-4129-b3f3-b893d839508M" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "683cd19b-7a34-4cc8-b3cc-64547e4f125f", "dea12856-c198-4129-b3f3-b893d839508U" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "145bdb34-18fa-4644-b002-6e4a02147b77", "dea12856-c198-4129-b3f3-b893d839508M" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "683cd19b-7a34-4cc8-b3cc-64547e4f125f", "dea12856-c198-4129-b3f3-b893d839508M" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "683cd19b-7a34-4cc8-b3cc-64547e4f125f", "dea12856-c198-4129-b3f3-b893d839508U" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d839508M");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d839508U");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "145bdb34-18fa-4644-b002-6e4a02147b77",
                column: "ConcurrencyStamp",
                value: "1249130e-99ba-4376-b0aa-e09940b2adba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "51759143-c218-4eee-bcd3-b9e19eb86405");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "683cd19b-7a34-4cc8-b3cc-64547e4f125f",
                column: "ConcurrencyStamp",
                value: "660dd255-cf26-4a1f-ab69-16af01df33b4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "246f2f89-ffc0-4b5a-b92a-1a798ead923f", "AQAAAAEAACcQAAAAEOmtFBPIpuUn+VM4cdIQVcyIv4SUtEeFPRy8/He6ZUa1RiEISH7LYl+vHetTBQbMqg==", "dae7a220-3e99-43ee-be95-1da19f4bda6e" });
        }
    }
}
