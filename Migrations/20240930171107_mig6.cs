using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FaceData = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFaces_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "30d185c6-ec6a-4c6b-ab3f-5b210d555828");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "3e754a66-5cb1-4a76-a365-6d99bf2ca30e", "AQAAAAEAACcQAAAAEMW0WlcYneTR+B+dL2ftRlOGEtc6SsYLPcF/MMNmXtWmh8yS4TDe5duNKpkCxWT6Ew==", "adf4f8ba-6ab2-41eb-b477-dd353d711eaf", new DateTime(3023, 9, 30, 20, 11, 6, 184, DateTimeKind.Local).AddTicks(1759), new DateTime(2024, 9, 30, 20, 11, 6, 181, DateTimeKind.Local).AddTicks(4118) });

            migrationBuilder.CreateIndex(
                name: "IX_UserFaces_UserId",
                table: "UserFaces",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFaces");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8040350d-1dbf-4e5a-8bee-520e63a99201");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "d593069b-b6c0-4988-9f3e-4197a61e3daf", "AQAAAAEAACcQAAAAEKhw1OMTo3Rf21oo/pzj96tH4iYnTl4EthC9icWy2zUGZnp++A+v0QGJUiCj7oprzA==", "af509222-a3d6-4b87-942a-f7a194d249c8", new DateTime(3023, 9, 24, 15, 58, 32, 439, DateTimeKind.Local).AddTicks(2291), new DateTime(2024, 9, 24, 15, 58, 32, 437, DateTimeKind.Local).AddTicks(5570) });
        }
    }
}
