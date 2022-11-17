using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photor.Infrastructure.Migrations
{
    public partial class FriendInvitationsTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvitation_AspNetUsers_ReceiverId",
                table: "FriendInvitation");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvitation_AspNetUsers_SenderId",
                table: "FriendInvitation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendInvitation",
                table: "FriendInvitation");

            migrationBuilder.RenameTable(
                name: "FriendInvitation",
                newName: "FriendInvitations");

            migrationBuilder.RenameIndex(
                name: "IX_FriendInvitation_SenderId",
                table: "FriendInvitations",
                newName: "IX_FriendInvitations_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendInvitation_ReceiverId",
                table: "FriendInvitations",
                newName: "IX_FriendInvitations_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendInvitations",
                table: "FriendInvitations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvitations_AspNetUsers_ReceiverId",
                table: "FriendInvitations",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvitations_AspNetUsers_SenderId",
                table: "FriendInvitations",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvitations_AspNetUsers_ReceiverId",
                table: "FriendInvitations");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvitations_AspNetUsers_SenderId",
                table: "FriendInvitations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendInvitations",
                table: "FriendInvitations");

            migrationBuilder.RenameTable(
                name: "FriendInvitations",
                newName: "FriendInvitation");

            migrationBuilder.RenameIndex(
                name: "IX_FriendInvitations_SenderId",
                table: "FriendInvitation",
                newName: "IX_FriendInvitation_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendInvitations_ReceiverId",
                table: "FriendInvitation",
                newName: "IX_FriendInvitation_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendInvitation",
                table: "FriendInvitation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvitation_AspNetUsers_ReceiverId",
                table: "FriendInvitation",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvitation_AspNetUsers_SenderId",
                table: "FriendInvitation",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
