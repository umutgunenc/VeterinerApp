using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1, "4388daab-d4a6-433c-aa7d-7dfce39b9a8a", "ADMİN", "ADMİN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CalisiyorMu", "ConcurrencyStamp", "DiplomaNo", "Email", "EmailConfirmed", "ImgURL", "InsanAdi", "InsanSoyadi", "InsanTckn", "LockoutEnabled", "LockoutEnd", "Maas", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi", "TermOfUse", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, true, "afe719d9-cd17-4f42-96c6-d12dc3ed228f", null, "umutgunenc@gmail.com", false, null, "Umut", "Günenç", "33080423902", false, null, null, "UMUTGUNENC@GMAİL.COM", "ADMIN", "AQAAAAEAACcQAAAAEOqVVu0mIpOLjOOvxjGuCVXpbx6PZ2sNB2fB5JjYipakbCjaPTiRTrrgR4nOQqWTHg==", "05300000000", false, "a58e14b2-78c6-494b-8bff-1c6f7de0cb72", new DateTime(3023, 9, 9, 2, 36, 24, 679, DateTimeKind.Local).AddTicks(8719), new DateTime(2024, 9, 9, 2, 36, 24, 678, DateTimeKind.Local).AddTicks(1727), true, false, "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
