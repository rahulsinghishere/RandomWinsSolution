using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RandomWins.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifieduserlogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionUsers_Users_UserId",
                table: "GameSessionUsers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_GameSessionUsers_UserId",
                table: "GameSessionUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameSessionUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserEmailAddress",
                table: "GameSessionUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserFullName",
                table: "GameSessionUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmailAddress",
                table: "GameSessionUsers");

            migrationBuilder.DropColumn(
                name: "UserFullName",
                table: "GameSessionUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GameSessionUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSessionUsers_UserId",
                table: "GameSessionUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionUsers_Users_UserId",
                table: "GameSessionUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
