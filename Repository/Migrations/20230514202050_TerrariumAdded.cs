using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class TerrariumAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Terrarium",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    terrariumBoundariesId = table.Column<Guid>(type: "uuid", nullable: false),
                    terrariumLimitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    measurementsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terrarium", x => x.name);
                    table.ForeignKey(
                        name: "FK_Terrarium_Measurements_measurementsId",
                        column: x => x.measurementsId,
                        principalTable: "Measurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Terrarium_TerrariumBoundaries_terrariumBoundariesId",
                        column: x => x.terrariumBoundariesId,
                        principalTable: "TerrariumBoundaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Terrarium_TerrariumLimits_terrariumLimitsId",
                        column: x => x.terrariumLimitsId,
                        principalTable: "TerrariumLimits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Terrarium_measurementsId",
                table: "Terrarium",
                column: "measurementsId");

            migrationBuilder.CreateIndex(
                name: "IX_Terrarium_terrariumBoundariesId",
                table: "Terrarium",
                column: "terrariumBoundariesId");

            migrationBuilder.CreateIndex(
                name: "IX_Terrarium_terrariumLimitsId",
                table: "Terrarium",
                column: "terrariumLimitsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Terrarium");
        }
    }
}
