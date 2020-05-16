using Microsoft.EntityFrameworkCore.Migrations;

namespace AvtoNetScraper.Migrations
{
    public partial class AddedPictureUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Cars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Cars");
        }
    }
}
