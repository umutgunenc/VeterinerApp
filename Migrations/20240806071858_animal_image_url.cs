using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class animal_image_url : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imgURl",
                table: "Hayvan",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imgURl",
                table: "Hayvan");
        }
    }
}
