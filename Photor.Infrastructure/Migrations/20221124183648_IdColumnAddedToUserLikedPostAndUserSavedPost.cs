using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class IdColumnAddedToUserLikedPostAndUserSavedPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersSavedPosts",
                table: "UsersSavedPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersLikedPosts",
                table: "UsersLikedPosts");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UsersSavedPosts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UsersLikedPosts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "UsersLikedPosts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Posts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersSavedPosts",
                table: "UsersSavedPosts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersLikedPosts",
                table: "UsersLikedPosts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersSavedPosts_UserId",
                table: "UsersSavedPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLikedPosts_UserId",
                table: "UsersLikedPosts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersSavedPosts",
                table: "UsersSavedPosts");

            migrationBuilder.DropIndex(
                name: "IX_UsersSavedPosts_UserId",
                table: "UsersSavedPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersLikedPosts",
                table: "UsersLikedPosts");

            migrationBuilder.DropIndex(
                name: "IX_UsersLikedPosts_UserId",
                table: "UsersLikedPosts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersSavedPosts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersLikedPosts");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "UsersLikedPosts");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Posts",
                type: "NVARCHAR(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersSavedPosts",
                table: "UsersSavedPosts",
                columns: new[] { "UserId", "PostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersLikedPosts",
                table: "UsersLikedPosts",
                columns: new[] { "UserId", "PostId" });
        }
    }
}
