using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendsOfUsers_Users_UserId",
                table: "FriendsOfUsers");

            migrationBuilder.CreateIndex(
                name: "IX_FriendsOfUsers_FriendOrRequestId",
                table: "FriendsOfUsers",
                column: "FriendOrRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendsOfUsers_Users_FriendOrRequestId",
                table: "FriendsOfUsers",
                column: "FriendOrRequestId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendsOfUsers_Users_UserId",
                table: "FriendsOfUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendsOfUsers_Users_FriendOrRequestId",
                table: "FriendsOfUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendsOfUsers_Users_UserId",
                table: "FriendsOfUsers");

            migrationBuilder.DropIndex(
                name: "IX_FriendsOfUsers_FriendOrRequestId",
                table: "FriendsOfUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendsOfUsers_Users_UserId",
                table: "FriendsOfUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
