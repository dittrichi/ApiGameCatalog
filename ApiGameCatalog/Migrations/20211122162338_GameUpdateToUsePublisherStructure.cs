using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiGameCatalog.Migrations
{
    public partial class GameUpdateToUsePublisherStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Games");

            migrationBuilder.AddColumn<Guid>(
                name: "PublisherId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherId",
                table: "Games",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Publishers_PublisherId",
                table: "Games",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Publishers_PublisherId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_PublisherId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
