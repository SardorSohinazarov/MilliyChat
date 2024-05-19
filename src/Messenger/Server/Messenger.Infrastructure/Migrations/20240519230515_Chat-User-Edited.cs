using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messenger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChatUserEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChatUsers_UserId",
                table: "ChatUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "ChatUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_UserId_ChatId",
                table: "ChatUsers",
                columns: new[] { "UserId", "ChatId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChatUsers_UserId_ChatId",
                table: "ChatUsers");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "ChatUsers");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_UserId",
                table: "ChatUsers",
                column: "UserId");
        }
    }
}
