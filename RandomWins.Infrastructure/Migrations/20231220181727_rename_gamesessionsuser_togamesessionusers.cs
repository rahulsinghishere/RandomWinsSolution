using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RandomWins.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rename_gamesessionsuser_togamesessionusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionsUser_GameSessions_GameSessionId",
                table: "GameSessionsUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionsUser_Users_UserId",
                table: "GameSessionsUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameSessionsUser",
                table: "GameSessionsUser");

            migrationBuilder.RenameTable(
                name: "GameSessionsUser",
                newName: "GameSessionUsers");

            migrationBuilder.RenameIndex(
                name: "IX_GameSessionsUser_UserId",
                table: "GameSessionUsers",
                newName: "IX_GameSessionUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GameSessionsUser_GameSessionId",
                table: "GameSessionUsers",
                newName: "IX_GameSessionUsers_GameSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameSessionUsers",
                table: "GameSessionUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionUsers_GameSessions_GameSessionId",
                table: "GameSessionUsers",
                column: "GameSessionId",
                principalTable: "GameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionUsers_Users_UserId",
                table: "GameSessionUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionUsers_GameSessions_GameSessionId",
                table: "GameSessionUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionUsers_Users_UserId",
                table: "GameSessionUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameSessionUsers",
                table: "GameSessionUsers");

            migrationBuilder.RenameTable(
                name: "GameSessionUsers",
                newName: "GameSessionsUser");

            migrationBuilder.RenameIndex(
                name: "IX_GameSessionUsers_UserId",
                table: "GameSessionsUser",
                newName: "IX_GameSessionsUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GameSessionUsers_GameSessionId",
                table: "GameSessionsUser",
                newName: "IX_GameSessionsUser_GameSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameSessionsUser",
                table: "GameSessionsUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionsUser_GameSessions_GameSessionId",
                table: "GameSessionsUser",
                column: "GameSessionId",
                principalTable: "GameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionsUser_Users_UserId",
                table: "GameSessionsUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
