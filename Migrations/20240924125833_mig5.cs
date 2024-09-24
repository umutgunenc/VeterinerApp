using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "StokGirisAdet",
                table: "StokHareketler",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "StokCikisAdet",
                table: "StokHareketler",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StokGirisAdet",
                table: "StokHareketler",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StokCikisAdet",
                table: "StokHareketler",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "df15bc09-1680-428a-bb1e-4ec4652786a3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "54814032-89a1-4692-8e81-21a46538d462", "AQAAAAEAACcQAAAAEINcQ9dBUSDPIIFjUNfAETRwc57khJ0HPkGBUfVAnZ5mRriGPULCOXE3ggJQFjdcTQ==", "c19d4d1f-b5d1-43ac-82a4-1d4b3c75fb1b", new DateTime(3023, 9, 20, 15, 39, 4, 441, DateTimeKind.Local).AddTicks(595), new DateTime(2024, 9, 20, 15, 39, 4, 440, DateTimeKind.Local).AddTicks(1564) });
        }
    }
}
