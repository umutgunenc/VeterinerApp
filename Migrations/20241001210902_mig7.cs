using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class mig7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceImgUrl",
                table: "UserFaces");

            migrationBuilder.AddColumn<byte[]>(
                name: "FaceData",
                table: "UserFaces",
                type: "varbinary(max)",
                nullable: true);

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
        }
    }
}
