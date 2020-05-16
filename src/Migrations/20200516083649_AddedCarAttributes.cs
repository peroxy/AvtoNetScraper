using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AvtoNetScraper.Migrations
{
    public partial class AddedCarAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdLocation",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BodyShape",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CO2Emissions",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChassisNumber",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityConsumption",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CombinedConsumption",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmissionClass",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Engine",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EngineType",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstRegistration",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interior",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InternalNumber",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MileageInKm",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDoors",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OutOfTownConsumption",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductionYear",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StockStatus",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TechnicalInspectionValidUntil",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Transmission",
                table: "Cars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdLocation",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BodyShape",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CO2Emissions",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ChassisNumber",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CityConsumption",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CombinedConsumption",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EmissionClass",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Engine",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EngineType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "FirstRegistration",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Interior",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "InternalNumber",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "MileageInKm",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "NumberOfDoors",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "OutOfTownConsumption",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ProductionYear",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "StockStatus",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "TechnicalInspectionValidUntil",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "Cars");
        }
    }
}
