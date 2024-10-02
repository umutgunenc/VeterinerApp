using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceImgUrl",
                table: "UserFaces");

            migrationBuilder.AddColumn<byte[]>(
                name: "FaceData",
                table: "UserFaces",
                type: "varbinary(max)",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2a448829-a6e5-49fb-beb7-73b8ab212a78");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "af094e63-02e7-43c7-b124-c860510426ab", "AQAAAAEAACcQAAAAELv7XdQJj4f72SPzILq6AoRUJdfXPFzM1Sxkb4/zXkgzx78tn+wx5+hhVZBpgj9Oaw==", "1c1e5cc7-0ef0-4fd4-9a4e-7368f1cad152", new DateTime(3023, 10, 2, 15, 44, 48, 964, DateTimeKind.Local).AddTicks(6595), new DateTime(2024, 10, 2, 15, 44, 48, 961, DateTimeKind.Local).AddTicks(4706) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceData",
                table: "UserFaces");

            migrationBuilder.AddColumn<string>(
                name: "FaceImgUrl",
                table: "UserFaces",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "706a7803-c87a-4615-8ae7-574c5cc20076");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "bfe7dd21-d5ee-429e-9405-e8b0c67b5c37", "AQAAAAEAACcQAAAAEENghBAqOgiDXu/JxzK942kUExJg8dY9MH7D2YyLOK8pymQq623+EoXxTFNBNZt2zQ==", "ac5f45d8-769f-466f-9092-358b5bc02555", new DateTime(3023, 10, 2, 0, 9, 0, 972, DateTimeKind.Local).AddTicks(3289), new DateTime(2024, 10, 2, 0, 9, 0, 970, DateTimeKind.Local).AddTicks(1051) });
        }
    }
}
