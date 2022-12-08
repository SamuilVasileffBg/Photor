using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class ApplicationUserImageUrlAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

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
    }
}
