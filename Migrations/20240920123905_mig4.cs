using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StokSayisi",
                table: "Stoklar");

            migrationBuilder.AlterColumn<int>(
                name: "HayvanCinsiyet",
                table: "Hayvanlar",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StokSayisi",
                table: "Stoklar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "HayvanCinsiyet",
                table: "Hayvanlar",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6eca004e-d58c-448c-bc3f-04b31d200f3b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "55d02645-393e-4e5b-8507-e9f26b8a9c30", "AQAAAAEAACcQAAAAENO6P0KPiGvlOmmtJgSj9EvJghFgUbq52tNqi0MvWGOiiW5Ou3of/TnZ+YeAEec5jw==", "f55ad952-4cbd-41cc-8a57-a27ce2d772b5", new DateTime(3023, 9, 12, 13, 1, 32, 541, DateTimeKind.Local).AddTicks(3306), new DateTime(2024, 9, 12, 13, 1, 32, 540, DateTimeKind.Local).AddTicks(959) });
        }
    }
}
