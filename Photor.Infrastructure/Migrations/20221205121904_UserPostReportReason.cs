using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class UserPostReportReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "UsersPostReports");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "UsersPostReports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "UsersPostReports");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UsersPostReports",
                type: "NVARCHAR(200)",
                nullable: false,
                defaultValue: "");
        }
    }
}
