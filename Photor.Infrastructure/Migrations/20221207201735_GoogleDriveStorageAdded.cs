using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class GoogleDriveStorageAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "43087852-e6e6-4391-8738-a0084f6621c4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69ab2e73-5b8b-46a4-84c0-7e049809c4b8", "AQAAAAEAACcQAAAAECkRA3C4O8vkJ/D90ZfEkGUK6/rZzYkkzz4cuVMF7KVXUJarfwxBa3u/FkfQEuUbHQ==", "af05b71e-6fae-46cb-b12a-d72a7697d0d1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "ae35170c-5435-46ff-b836-4c5262580465");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d87a77c-2b17-4bc0-97c9-52329becf737", "AQAAAAEAACcQAAAAEHMMGXESQGGHS54/wye1WHnT8I40P9Ov+ujwLvWCCWCK9mHicdR4Tv37Ee8oqXfdxw==", "f88925e6-719e-48c4-b41f-7e6169891949" });
        }
    }
}
