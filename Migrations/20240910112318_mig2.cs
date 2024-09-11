using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserHayvan");

            migrationBuilder.CreateTable(
                name: "SahipHayvan",
                columns: table => new
                {
                    HayvanId = table.Column<int>(type: "int", nullable: false),
                    SahiplerId = table.Column<int>(type: "int", nullable: false),
                    SahiplenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SahiplenmeCikisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SahipHayvan", x => new { x.SahiplerId, x.HayvanId });
                    table.ForeignKey(
                        name: "FK_SahipHayvan_AspNetUsers_SahiplerId",
                        column: x => x.SahiplerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SahipHayvan_Hayvanlar_HayvanId",
                        column: x => x.HayvanId,
                        principalTable: "Hayvanlar",
                        principalColumn: "HayvanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f39bbf6f-7dd9-4b5a-89ea-7bfef6fd4b58");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "7b9c4974-e2fc-404c-821f-a4771c1a6557", "AQAAAAEAACcQAAAAEIi1XpaXXrHMrmwTT7W7Skwnxh3I2zT4AXnbRTV+bpzWxpKYa+U00/IFsvY8aU1UhQ==", "cbd1951d-9bba-4c76-9c87-de3b0613b30b", new DateTime(3023, 9, 10, 14, 23, 18, 118, DateTimeKind.Local).AddTicks(2639), new DateTime(2024, 9, 10, 14, 23, 18, 117, DateTimeKind.Local).AddTicks(2346) });

            migrationBuilder.CreateIndex(
                name: "IX_SahipHayvan_HayvanId",
                table: "SahipHayvan",
                column: "HayvanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SahipHayvan");

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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c05809fa-65a7-4e9e-8e2d-a70a502bc3dc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "38abaa00-22ff-45b0-9bbf-3e067357ee12", "AQAAAAEAACcQAAAAEBOxF4d39NgXRv822KJbQInBGtEY9X3XQIaNAwmOBhXA/Q5cYFy92nQfJel89mgVkQ==", "ca5ba3a4-60af-4c92-a762-4225cca2a944", new DateTime(3023, 9, 10, 13, 15, 27, 490, DateTimeKind.Local).AddTicks(4335), new DateTime(2024, 9, 10, 13, 15, 27, 489, DateTimeKind.Local).AddTicks(5128) });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserHayvan_SahiplerId",
                table: "AppUserHayvan",
                column: "SahiplerId");
        }
    }
}
