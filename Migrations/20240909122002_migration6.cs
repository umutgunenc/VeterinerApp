using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fd617bdc-62ea-4049-836d-d825b95aa2dd", "ADMIN", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "94d258d0-f997-4e18-91d2-8d734115da3e", "AQAAAAEAACcQAAAAELlZZDq38Wo2VBntllwLZH7V/vqaQWS3nD6FHspvf+mj6uMk3VcMFhDGKb8/JS3zZQ==", "ce5f1deb-64d3-4999-a497-06125e896682", new DateTime(3023, 9, 9, 15, 20, 1, 82, DateTimeKind.Local).AddTicks(5652), new DateTime(2024, 9, 9, 15, 20, 1, 81, DateTimeKind.Local).AddTicks(5406) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fcb75942-fa56-4afb-a99b-e02616ec24b0", "ADMİN", "ADMİN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "30a5f66a-de46-4882-b91e-688ae205afe0", "AQAAAAEAACcQAAAAEHj7HBuKw56vELbqezziUlkq7cdK4WlMckOK7dAxH+iWBcC5VTq9dm+esPJEQtu0pg==", "8484cc53-78e6-498d-b04e-92c7dc511177", new DateTime(3023, 9, 9, 14, 57, 32, 871, DateTimeKind.Local).AddTicks(2246), new DateTime(2024, 9, 9, 14, 57, 32, 869, DateTimeKind.Local).AddTicks(5257) });
        }
    }
}
