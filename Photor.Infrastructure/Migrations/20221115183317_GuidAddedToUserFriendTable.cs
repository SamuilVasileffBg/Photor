using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class GuidAddedToUserFriendTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersFriends",
                table: "UsersFriends");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UsersFriends",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersFriends",
                table: "UsersFriends",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersFriends_UserId",
                table: "UsersFriends",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersFriends",
                table: "UsersFriends");

            migrationBuilder.DropIndex(
                name: "IX_UsersFriends_UserId",
                table: "UsersFriends");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersFriends");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersFriends",
                table: "UsersFriends",
                columns: new[] { "UserId", "FriendId" });
        }
    }
}
