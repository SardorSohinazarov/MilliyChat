﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messenger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Chat_User_Unique_Constraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChatUsers_UserId_ChatId",
                table: "ChatUsers");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_UserId_ChatId",
                table: "ChatUsers",
                columns: new[] { "UserId", "ChatId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChatUsers_UserId_ChatId",
                table: "ChatUsers");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_UserId_ChatId",
                table: "ChatUsers",
                columns: new[] { "UserId", "ChatId" });
        }
    }
}
