using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hayvanlar_CinsTur_CinsTurCinsId_CinsTurTurId",
                table: "Hayvanlar");

            migrationBuilder.DropColumn(
                name: "CinsId",
                table: "Hayvanlar");

            migrationBuilder.DropColumn(
                name: "TurId",
                table: "Hayvanlar");

            migrationBuilder.AlterColumn<int>(
                name: "CinsTurTurId",
                table: "Hayvanlar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CinsTurCinsId",
                table: "Hayvanlar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hayvanlar_CinsTur_CinsTurCinsId_CinsTurTurId",
                table: "Hayvanlar",
                columns: new[] { "CinsTurCinsId", "CinsTurTurId" },
                principalTable: "CinsTur",
                principalColumns: new[] { "CinsId", "TurId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hayvanlar_CinsTur_CinsTurCinsId_CinsTurTurId",
                table: "Hayvanlar");

            migrationBuilder.AlterColumn<int>(
                name: "CinsTurTurId",
                table: "Hayvanlar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CinsTurCinsId",
                table: "Hayvanlar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CinsId",
                table: "Hayvanlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TurId",
                table: "Hayvanlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Hayvanlar_CinsTur_CinsTurCinsId_CinsTurTurId",
                table: "Hayvanlar",
                columns: new[] { "CinsTurCinsId", "CinsTurTurId" },
                principalTable: "CinsTur",
                principalColumns: new[] { "CinsId", "TurId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
