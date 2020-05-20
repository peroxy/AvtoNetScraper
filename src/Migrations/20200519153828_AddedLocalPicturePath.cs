using Microsoft.EntityFrameworkCore.Migrations;

namespace AvtoNetScraper.Migrations
{
    public partial class AddedLocalPicturePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalPicturePath",
                table: "Cars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalPicturePath",
                table: "Cars");
        }
    }
}
