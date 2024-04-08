using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendsOfUsers_Users_UserId",
                table: "FriendsOfUsers");

            migrationBuilder.DropTable(
                name: "FriendRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "FriendsOfUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequestAccepted",
                table: "FriendsOfUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendsOfUsers_Users_UserId",
                table: "FriendsOfUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendsOfUsers_Users_UserId",
                table: "FriendsOfUsers");

            migrationBuilder.DropColumn(
                name: "IsRequestAccepted",
                table: "FriendsOfUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "FriendsOfUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "FriendRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SendRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_UserId",
                table: "FriendRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendsOfUsers_Users_UserId",
                table: "FriendsOfUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
