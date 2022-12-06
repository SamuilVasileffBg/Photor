using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class ReportIsDeletedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UsersPostReports",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UsersPostReports");

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
    }
}
