using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTerrarium : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terrarium_Measurements_measurementsId",
                table: "Terrarium");

            migrationBuilder.DropIndex(
                name: "IX_Terrarium_measurementsId",
                table: "Terrarium");

            migrationBuilder.DropColumn(
                name: "measurementsId",
                table: "Terrarium");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "measurementsId",
                table: "Terrarium",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Terrarium_measurementsId",
                table: "Terrarium",
                column: "measurementsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Terrarium_Measurements_measurementsId",
                table: "Terrarium",
                column: "measurementsId",
                principalTable: "Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
