using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsanTckn = table.Column<string>(type: "char(11)", nullable: false),
                    InsanAdi = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    InsanSoyadi = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ImgURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SifreOlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SifreGecerlilikTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiplomaNo = table.Column<string>(type: "nvarchar(11)", nullable: true),
                    CalisiyorMu = table.Column<bool>(type: "bit", nullable: false),
                    Maas = table.Column<double>(type: "float", nullable: true),
                    TermOfUse = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Birimler",
                columns: table => new
                {
                    BirimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirimAdi = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birimler", x => x.BirimId);
                });

            migrationBuilder.CreateTable(
                name: "Cinsler",
                columns: table => new
                {
                    CinsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CinsAdi = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinsler", x => x.CinsId);
                });

            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Renkler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenkAdi = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renkler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tedaviler",
                columns: table => new
                {
                    TedaviId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TedaviAdi = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TedaviUcreti = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tedaviler", x => x.TedaviId);
                });

            migrationBuilder.CreateTable(
                name: "Turler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurAdi = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaasOdemeleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalisanId = table.Column<int>(type: "int", nullable: false),
                    OdemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OdenenTutar = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaasOdemeleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaasOdemeleri_AppUsers_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stoklar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StokBarkod = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    StokAdi = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    StokSayisi = table.Column<int>(type: "int", nullable: false),
                    BirimId = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stoklar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stoklar_Birimler_BirimId",
                        column: x => x.BirimId,
                        principalTable: "Birimler",
                        principalColumn: "BirimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stoklar_Kategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CinsTur",
                columns: table => new
                {
                    CinsId = table.Column<int>(type: "int", nullable: false),
                    TurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinsTur", x => new { x.CinsId, x.TurId });
                    table.ForeignKey(
                        name: "FK_CinsTur_Cinsler_CinsId",
                        column: x => x.CinsId,
                        principalTable: "Cinsler",
                        principalColumn: "CinsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CinsTur_Turler_TurId",
                        column: x => x.TurId,
                        principalTable: "Turler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FiyatListeleri",
                columns: table => new
                {
                    FiyatListesiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StokId = table.Column<int>(type: "int", nullable: false),
                    FiyatSatisGecerlilikBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FiyatSatisGecerlilikBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SatisFiyati = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiyatListeleri", x => x.FiyatListesiId);
                    table.ForeignKey(
                        name: "FK_FiyatListeleri_Stoklar_StokId",
                        column: x => x.StokId,
                        principalTable: "Stoklar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StokHareketler",
                columns: table => new
                {
                    StokHareketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StokHareketTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StokId = table.Column<int>(type: "int", nullable: false),
                    SatisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SatisFiyati = table.Column<double>(type: "float", nullable: true),
                    AlisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlisFiyati = table.Column<double>(type: "float", nullable: true),
                    CalisanId = table.Column<int>(type: "int", nullable: false),
                    StokGirisAdet = table.Column<int>(type: "int", nullable: true),
                    StokCikisAdet = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokHareketler", x => x.StokHareketId);
                    table.ForeignKey(
                        name: "FK_StokHareketler_AppUsers_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StokHareketler_Stoklar_StokId",
                        column: x => x.StokId,
                        principalTable: "Stoklar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hayvanlar",
                columns: table => new
                {
                    HayvanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HayvanAdi = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    HayvanCinsiyet = table.Column<string>(type: "char(1)", nullable: false),
                    HayvanKilo = table.Column<double>(type: "float", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HayvanDogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HayvanOlumTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RenkId = table.Column<int>(type: "int", nullable: false),
                    TurId = table.Column<int>(type: "int", nullable: false),
                    CinsId = table.Column<int>(type: "int", nullable: false),
                    HayvanAnneId = table.Column<int>(type: "int", nullable: true),
                    HayvanBabaId = table.Column<int>(type: "int", nullable: true),
                    CinsTurCinsId = table.Column<int>(type: "int", nullable: true),
                    CinsTurTurId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hayvanlar", x => x.HayvanId);
                    table.ForeignKey(
                        name: "FK__Hayvan__Anne",
                        column: x => x.HayvanAnneId,
                        principalTable: "Hayvanlar",
                        principalColumn: "HayvanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Hayvan__Baba",
                        column: x => x.HayvanBabaId,
                        principalTable: "Hayvanlar",
                        principalColumn: "HayvanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hayvanlar_CinsTur_CinsTurCinsId_CinsTurTurId",
                        columns: x => new { x.CinsTurCinsId, x.CinsTurTurId },
                        principalTable: "CinsTur",
                        principalColumns: new[] { "CinsId", "TurId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hayvanlar_Renkler_RenkId",
                        column: x => x.RenkId,
                        principalTable: "Renkler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_AppUserHayvan_AppUsers_SahiplerId",
                        column: x => x.SahiplerId,
                        principalTable: "AppUsers",
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
                name: "Muayeneler",
                columns: table => new
                {
                    MuayeneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TedaviId = table.Column<int>(type: "int", nullable: false),
                    HayvanId = table.Column<int>(type: "int", nullable: false),
                    MuayeneTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SonrakiMuayeneTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HekimId = table.Column<int>(type: "int", nullable: false),
                    StokId = table.Column<int>(type: "int", nullable: true),
                    Gelir = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muayeneler", x => x.MuayeneId);
                    table.ForeignKey(
                        name: "FK_Muayeneler_AppUsers_HekimId",
                        column: x => x.HekimId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Muayeneler_Hayvanlar_HayvanId",
                        column: x => x.HayvanId,
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

            migrationBuilder.CreateIndex(
                name: "IX_AppUserHayvan_SahiplerId",
                table: "AppUserHayvan",
                column: "SahiplerId");

            migrationBuilder.CreateIndex(
                name: "IX_CinsTur_TurId",
                table: "CinsTur",
                column: "TurId");

            migrationBuilder.CreateIndex(
                name: "IX_FiyatListeleri_StokId",
                table: "FiyatListeleri",
                column: "StokId");

            migrationBuilder.CreateIndex(
                name: "IX_Hayvanlar_CinsTurCinsId_CinsTurTurId",
                table: "Hayvanlar",
                columns: new[] { "CinsTurCinsId", "CinsTurTurId" });

            migrationBuilder.CreateIndex(
                name: "IX_Hayvanlar_HayvanAnneId",
                table: "Hayvanlar",
                column: "HayvanAnneId");

            migrationBuilder.CreateIndex(
                name: "IX_Hayvanlar_HayvanBabaId",
                table: "Hayvanlar",
                column: "HayvanBabaId");

            migrationBuilder.CreateIndex(
                name: "IX_Hayvanlar_RenkId",
                table: "Hayvanlar",
                column: "RenkId");

            migrationBuilder.CreateIndex(
                name: "IX_MaasOdemeleri_CalisanId",
                table: "MaasOdemeleri",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Muayeneler_HayvanId",
                table: "Muayeneler",
                column: "HayvanId");

            migrationBuilder.CreateIndex(
                name: "IX_Muayeneler_HekimId",
                table: "Muayeneler",
                column: "HekimId");

            migrationBuilder.CreateIndex(
                name: "IX_MuayeneStok_StoklarId",
                table: "MuayeneStok",
                column: "StoklarId");

            migrationBuilder.CreateIndex(
                name: "IX_MuayeneTedavi_TedavilerTedaviId",
                table: "MuayeneTedavi",
                column: "TedavilerTedaviId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketler_CalisanId",
                table: "StokHareketler",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketler_StokId",
                table: "StokHareketler",
                column: "StokId");

            migrationBuilder.CreateIndex(
                name: "IX_Stoklar_BirimId",
                table: "Stoklar",
                column: "BirimId");

            migrationBuilder.CreateIndex(
                name: "IX_Stoklar_KategoriId",
                table: "Stoklar",
                column: "KategoriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserHayvan");

            migrationBuilder.DropTable(
                name: "FiyatListeleri");

            migrationBuilder.DropTable(
                name: "MaasOdemeleri");

            migrationBuilder.DropTable(
                name: "MuayeneStok");

            migrationBuilder.DropTable(
                name: "MuayeneTedavi");

            migrationBuilder.DropTable(
                name: "StokHareketler");

            migrationBuilder.DropTable(
                name: "Muayeneler");

            migrationBuilder.DropTable(
                name: "Tedaviler");

            migrationBuilder.DropTable(
                name: "Stoklar");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Hayvanlar");

            migrationBuilder.DropTable(
                name: "Birimler");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropTable(
                name: "CinsTur");

            migrationBuilder.DropTable(
                name: "Renkler");

            migrationBuilder.DropTable(
                name: "Cinsler");

            migrationBuilder.DropTable(
                name: "Turler");
        }
    }
}
