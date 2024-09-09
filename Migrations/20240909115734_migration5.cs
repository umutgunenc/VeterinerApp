using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hayvanlar_CinsTur_CinsTurCinsId_CinsTurTurId",
                table: "Hayvanlar");

            migrationBuilder.DropTable(
                name: "AppUserHayvan");

            migrationBuilder.DropTable(
                name: "MuayeneStok");

            migrationBuilder.DropTable(
                name: "MuayeneTedavi");

            migrationBuilder.DropIndex(
                name: "IX_Hayvanlar_CinsTurCinsId_CinsTurTurId",
                table: "Hayvanlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CinsTur",
                table: "CinsTur");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Turler",
                newName: "TurId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Renkler",
                newName: "RenkId");

            migrationBuilder.RenameColumn(
                name: "CinsTurTurId",
                table: "Hayvanlar",
                newName: "TurId");

            migrationBuilder.RenameColumn(
                name: "CinsTurCinsId",
                table: "Hayvanlar",
                newName: "CinsId");

            migrationBuilder.AddColumn<int>(
                name: "CinsTurId",
                table: "Hayvanlar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CinsTur",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "HayvanId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinsTur",
                table: "CinsTur",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MuayeneStoklar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MuayeneId = table.Column<int>(type: "int", nullable: false),
                    StokId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuayeneStoklar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MuayeneStoklar_Muayeneler_MuayeneId",
                        column: x => x.MuayeneId,
                        principalTable: "Muayeneler",
                        principalColumn: "MuayeneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuayeneStoklar_Stoklar_StokId",
                        column: x => x.StokId,
                        principalTable: "Stoklar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SahipHayvanlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HayvanId = table.Column<int>(type: "int", nullable: false),
                    SahipId = table.Column<int>(type: "int", nullable: false),
                    SahiplikTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SahiplikCikisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SahipHayvanlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SahipHayvanlar_AspNetUsers_SahipId",
                        column: x => x.SahipId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SahipHayvanlar_Hayvanlar_HayvanId",
                        column: x => x.HayvanId,
                        principalTable: "Hayvanlar",
                        principalColumn: "HayvanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StokMuayeneler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlacBarkod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MuayeneId = table.Column<int>(type: "int", nullable: false),
                    StokId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokMuayeneler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StokMuayeneler_Muayeneler_MuayeneId",
                        column: x => x.MuayeneId,
                        principalTable: "Muayeneler",
                        principalColumn: "MuayeneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StokMuayeneler_Stoklar_StokId",
                        column: x => x.StokId,
                        principalTable: "Stoklar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TedaviMuayeneler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TedaviId = table.Column<int>(type: "int", nullable: false),
                    MuayeneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TedaviMuayeneler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TedaviMuayeneler_Muayeneler_MuayeneId",
                        column: x => x.MuayeneId,
                        principalTable: "Muayeneler",
                        principalColumn: "MuayeneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TedaviMuayeneler_Tedaviler_TedaviId",
                        column: x => x.TedaviId,
                        principalTable: "Tedaviler",
                        principalColumn: "TedaviId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fcb75942-fa56-4afb-a99b-e02616ec24b0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "30a5f66a-de46-4882-b91e-688ae205afe0", "AQAAAAEAACcQAAAAEHj7HBuKw56vELbqezziUlkq7cdK4WlMckOK7dAxH+iWBcC5VTq9dm+esPJEQtu0pg==", "8484cc53-78e6-498d-b04e-92c7dc511177", new DateTime(3023, 9, 9, 14, 57, 32, 871, DateTimeKind.Local).AddTicks(2246), new DateTime(2024, 9, 9, 14, 57, 32, 869, DateTimeKind.Local).AddTicks(5257) });

            migrationBuilder.CreateIndex(
                name: "IX_Hayvanlar_CinsTurId",
                table: "Hayvanlar",
                column: "CinsTurId");

            migrationBuilder.CreateIndex(
                name: "IX_CinsTur_CinsId",
                table: "CinsTur",
                column: "CinsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HayvanId",
                table: "AspNetUsers",
                column: "HayvanId");

            migrationBuilder.CreateIndex(
                name: "IX_MuayeneStoklar_MuayeneId",
                table: "MuayeneStoklar",
                column: "MuayeneId");

            migrationBuilder.CreateIndex(
                name: "IX_MuayeneStoklar_StokId",
                table: "MuayeneStoklar",
                column: "StokId");

            migrationBuilder.CreateIndex(
                name: "IX_SahipHayvanlar_HayvanId",
                table: "SahipHayvanlar",
                column: "HayvanId");

            migrationBuilder.CreateIndex(
                name: "IX_SahipHayvanlar_SahipId",
                table: "SahipHayvanlar",
                column: "SahipId");

            migrationBuilder.CreateIndex(
                name: "IX_StokMuayeneler_MuayeneId",
                table: "StokMuayeneler",
                column: "MuayeneId");

            migrationBuilder.CreateIndex(
                name: "IX_StokMuayeneler_StokId",
                table: "StokMuayeneler",
                column: "StokId");

            migrationBuilder.CreateIndex(
                name: "IX_TedaviMuayeneler_MuayeneId",
                table: "TedaviMuayeneler",
                column: "MuayeneId");

            migrationBuilder.CreateIndex(
                name: "IX_TedaviMuayeneler_TedaviId",
                table: "TedaviMuayeneler",
                column: "TedaviId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hayvanlar_HayvanId",
                table: "AspNetUsers",
                column: "HayvanId",
                principalTable: "Hayvanlar",
                principalColumn: "HayvanId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hayvanlar_CinsTur_CinsTurId",
                table: "Hayvanlar",
                column: "CinsTurId",
                principalTable: "CinsTur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hayvanlar_HayvanId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Hayvanlar_CinsTur_CinsTurId",
                table: "Hayvanlar");

            migrationBuilder.DropTable(
                name: "MuayeneStoklar");

            migrationBuilder.DropTable(
                name: "SahipHayvanlar");

            migrationBuilder.DropTable(
                name: "StokMuayeneler");

            migrationBuilder.DropTable(
                name: "TedaviMuayeneler");

            migrationBuilder.DropIndex(
                name: "IX_Hayvanlar_CinsTurId",
                table: "Hayvanlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CinsTur",
                table: "CinsTur");

            migrationBuilder.DropIndex(
                name: "IX_CinsTur_CinsId",
                table: "CinsTur");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HayvanId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CinsTurId",
                table: "Hayvanlar");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CinsTur");

            migrationBuilder.DropColumn(
                name: "HayvanId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TurId",
                table: "Turler",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RenkId",
                table: "Renkler",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TurId",
                table: "Hayvanlar",
                newName: "CinsTurTurId");

            migrationBuilder.RenameColumn(
                name: "CinsId",
                table: "Hayvanlar",
                newName: "CinsTurCinsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinsTur",
                table: "CinsTur",
                columns: new[] { "CinsId", "TurId" });

            migrationBuilder.CreateTable(
                name: "AppUserHayvan",
                columns: table => new
                {
                    HayvanlarHayvanId = table.Column<int>(type: "int", nullable: false),
                    SahiplerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserHayvan", x => new { x.HayvanlarHayvanId, x.SahiplerId });
                    table.ForeignKey(
                        name: "FK_AppUserHayvan_AspNetUsers_SahiplerId",
                        column: x => x.SahiplerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserHayvan_Hayvanlar_HayvanlarHayvanId",
                        column: x => x.HayvanlarHayvanId,
                        principalTable: "Hayvanlar",
                        principalColumn: "HayvanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MuayeneStok",
                columns: table => new
                {
                    MuayenelerMuayeneId = table.Column<int>(type: "int", nullable: false),
                    StoklarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuayeneStok", x => new { x.MuayenelerMuayeneId, x.StoklarId });
                    table.ForeignKey(
                        name: "FK_MuayeneStok_Muayeneler_MuayenelerMuayeneId",
                        column: x => x.MuayenelerMuayeneId,
                        principalTable: "Muayeneler",
                        principalColumn: "MuayeneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuayeneStok_Stoklar_StoklarId",
                        column: x => x.StoklarId,
                        principalTable: "Stoklar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MuayeneTedavi",
                columns: table => new
                {
                    MuayenelerMuayeneId = table.Column<int>(type: "int", nullable: false),
                    TedavilerTedaviId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuayeneTedavi", x => new { x.MuayenelerMuayeneId, x.TedavilerTedaviId });
                    table.ForeignKey(
                        name: "FK_MuayeneTedavi_Muayeneler_MuayenelerMuayeneId",
                        column: x => x.MuayenelerMuayeneId,
                        principalTable: "Muayeneler",
                        principalColumn: "MuayeneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuayeneTedavi_Tedaviler_TedavilerTedaviId",
                        column: x => x.TedavilerTedaviId,
                        principalTable: "Tedaviler",
                        principalColumn: "TedaviId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4388daab-d4a6-433c-aa7d-7dfce39b9a8a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "afe719d9-cd17-4f42-96c6-d12dc3ed228f", "AQAAAAEAACcQAAAAEOqVVu0mIpOLjOOvxjGuCVXpbx6PZ2sNB2fB5JjYipakbCjaPTiRTrrgR4nOQqWTHg==", "a58e14b2-78c6-494b-8bff-1c6f7de0cb72", new DateTime(3023, 9, 9, 2, 36, 24, 679, DateTimeKind.Local).AddTicks(8719), new DateTime(2024, 9, 9, 2, 36, 24, 678, DateTimeKind.Local).AddTicks(1727) });

            migrationBuilder.CreateIndex(
                name: "IX_Hayvanlar_CinsTurCinsId_CinsTurTurId",
                table: "Hayvanlar",
                columns: new[] { "CinsTurCinsId", "CinsTurTurId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserHayvan_SahiplerId",
                table: "AppUserHayvan",
                column: "SahiplerId");

            migrationBuilder.CreateIndex(
                name: "IX_MuayeneStok_StoklarId",
                table: "MuayeneStok",
                column: "StoklarId");

            migrationBuilder.CreateIndex(
                name: "IX_MuayeneTedavi_TedavilerTedaviId",
                table: "MuayeneTedavi",
                column: "TedavilerTedaviId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hayvanlar_CinsTur_CinsTurCinsId_CinsTurTurId",
                table: "Hayvanlar",
                columns: new[] { "CinsTurCinsId", "CinsTurTurId" },
                principalTable: "CinsTur",
                principalColumns: new[] { "CinsId", "TurId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
