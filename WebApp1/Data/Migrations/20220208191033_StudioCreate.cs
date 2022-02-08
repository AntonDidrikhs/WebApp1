using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp1.Data.Migrations
{
    public partial class StudioCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropColumn(
                name: "Studio",
                table: "Game");

            migrationBuilder.AddColumn<int>(
                name: "StudioId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Studio",
                columns: table => new
                {
                    StudioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudioName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudioDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studio", x => x.StudioId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_StudioId",
                table: "Game",
                column: "StudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Studio_StudioId",
                table: "Game",
                column: "StudioId",
                principalTable: "Studio",
                principalColumn: "StudioId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Studio_StudioId",
                table: "Game");

            migrationBuilder.DropTable(
                name: "Studio");

            migrationBuilder.DropIndex(
                name: "IX_Game_StudioId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "StudioId",
                table: "Game");

            migrationBuilder.AddColumn<string>(
                name: "Studio",
                table: "Game",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                });
        }
    }
}
