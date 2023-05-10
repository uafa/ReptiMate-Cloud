using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class BoundariesAndLimitsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TerrariumLimits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemperatureLimitMax = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureLimitMin = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerrariumLimits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TerrariumBoundaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemperatureBoundaryMax = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureBoundaryMin = table.Column<double>(type: "double precision", nullable: false),
                    HumidityBoundaryMax = table.Column<double>(type: "double precision", nullable: false),
                    HumidityBoundaryMin = table.Column<double>(type: "double precision", nullable: false),
                    CO2BoundaryMax = table.Column<double>(type: "double precision", nullable: false),
                    CO2BoundaryMin = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerrariumBoundaries", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TerrariumLimits");
            migrationBuilder.DropTable(
                name: "TerrariumBounadries");
        }
    }
}
