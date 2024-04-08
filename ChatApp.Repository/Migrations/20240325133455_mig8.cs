using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FriendId",
                table: "FriendsOfUsers",
                newName: "FriendOrRequestId");

            migrationBuilder.RenameColumn(
                name: "AddDate",
                table: "FriendsOfUsers",
                newName: "RequestSenderDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestAcceptDate",
                table: "FriendsOfUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestAcceptDate",
                table: "FriendsOfUsers");

            migrationBuilder.RenameColumn(
                name: "RequestSenderDate",
                table: "FriendsOfUsers",
                newName: "AddDate");

            migrationBuilder.RenameColumn(
                name: "FriendOrRequestId",
                table: "FriendsOfUsers",
                newName: "FriendId");
        }
    }
}
