using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class UserRoleAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "edcb9b26-73b2-47b5-9150-5a3d816cbab5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "683cd19b-7a34-4cc8-b3cc-64547e4f125f", "fdbf6dfb-6122-4baa-96e0-8d5e137ace71", "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "283fadbb-44b7-4cd4-89ba-4726cc5e3b00", "AQAAAAEAACcQAAAAED/gUs0EPPE1cQwqF1Tf7YIoNuBy0B7vBq2TsM+G3ysu+E2n2YO1AyIvWLHgzZhCLA==", "0619c5c2-1e1f-420d-8291-fba702344549" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "683cd19b-7a34-4cc8-b3cc-64547e4f125f", "dea12856-c198-4129-b3f3-b893d8395082" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "683cd19b-7a34-4cc8-b3cc-64547e4f125f", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "683cd19b-7a34-4cc8-b3cc-64547e4f125f");

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
    }
}
