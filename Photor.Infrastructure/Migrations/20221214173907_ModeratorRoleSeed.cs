using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class ModeratorRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "145bdb34-18fa-4644-b002-6e4a02147b77", "1249130e-99ba-4376-b0aa-e09940b2adba", "Moderator", "MODERATOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "246f2f89-ffc0-4b5a-b92a-1a798ead923f", "AQAAAAEAACcQAAAAEOmtFBPIpuUn+VM4cdIQVcyIv4SUtEeFPRy8/He6ZUa1RiEISH7LYl+vHetTBQbMqg==", "dae7a220-3e99-43ee-be95-1da19f4bda6e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "145bdb34-18fa-4644-b002-6e4a02147b77");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "edcb9b26-73b2-47b5-9150-5a3d816cbab5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "683cd19b-7a34-4cc8-b3cc-64547e4f125f",
                column: "ConcurrencyStamp",
                value: "fdbf6dfb-6122-4baa-96e0-8d5e137ace71");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "283fadbb-44b7-4cd4-89ba-4726cc5e3b00", "AQAAAAEAACcQAAAAED/gUs0EPPE1cQwqF1Tf7YIoNuBy0B7vBq2TsM+G3ysu+E2n2YO1AyIvWLHgzZhCLA==", "0619c5c2-1e1f-420d-8291-fba702344549" });
        }
    }
}
