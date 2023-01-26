using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homework17_LiudvynskyiV.S.Migrations
{
    public partial class fixTicketProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Cinemas_CinemaId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_CinemaId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "CinemaId",
                table: "Seats");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CinemaId",
                table: "Seats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Seats_CinemaId",
                table: "Seats",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Cinemas_CinemaId",
                table: "Seats",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
