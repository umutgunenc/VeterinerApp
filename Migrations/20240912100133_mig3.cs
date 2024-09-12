using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "HayvanCinsiyet",
                table: "Hayvanlar",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HayvanCinsiyet",
                table: "Hayvanlar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

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
        }
    }
}
