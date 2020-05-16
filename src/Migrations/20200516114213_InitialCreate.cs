using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AvtoNetScraper.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UrlId = table.Column<int>(nullable: false),
                    Age = table.Column<string>(nullable: true),
                    FirstRegistration = table.Column<DateTime>(nullable: true),
                    TechnicalInspectionValidUntil = table.Column<DateTime>(nullable: true),
                    ProductionYear = table.Column<int>(nullable: true),
                    MileageInKm = table.Column<int>(nullable: true),
                    Engine = table.Column<string>(nullable: true),
                    EngineType = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: true),
                    Transmission = table.Column<string>(nullable: true),
                    BodyShape = table.Column<string>(nullable: true),
                    DoorNumber = table.Column<int>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Interior = table.Column<string>(nullable: true),
                    ChassisNumber = table.Column<string>(nullable: true),
                    AdLocation = table.Column<string>(nullable: true),
                    CombinedConsumption = table.Column<string>(nullable: true),
                    OutOfTownConsumption = table.Column<string>(nullable: true),
                    CityConsumption = table.Column<string>(nullable: true),
                    EmissionClass = table.Column<string>(nullable: true),
                    CO2Emissions = table.Column<string>(nullable: true),
                    InternalNumber = table.Column<string>(nullable: true),
                    StockStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Urls");
        }
    }
}
