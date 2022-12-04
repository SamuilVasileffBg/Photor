using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class DateTimeAddedToUserPostComment2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "UsersPostComments",
                newName: "DateTimeOfLastEdit");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeOfCreation",
                table: "UsersPostComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeOfCreation",
                table: "UsersPostComments");

            migrationBuilder.RenameColumn(
                name: "DateTimeOfLastEdit",
                table: "UsersPostComments",
                newName: "DateTime");
        }
    }
}
