using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerApp.Migrations
{
    public partial class migration_identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cins = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Renk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Renk = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stok",
                columns: table => new
                {
                    StokBarkod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StokAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StokSayisi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stok__5E87D66AAB303988", x => x.StokBarkod);
                });

            migrationBuilder.CreateTable(
                name: "Tedavi",
                columns: table => new
                {
                    TedaviId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TedaviAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TedaviUcreti = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tedavi", x => x.TedaviId);
                });

            migrationBuilder.CreateTable(
                name: "Tur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tur = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InsanTCKN = table.Column<string>(type: "varchar(450)", unicode: false, maxLength: 450, nullable: true),
                    InsanAdi = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    InsanSoyadi = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ImgURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SifreOlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SifreGecerlilikTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiplomaNo = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    CalisiyorMu = table.Column<bool>(type: "bit", nullable: false),
                    Maas = table.Column<double>(type: "float", nullable: true),
                    RolId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetRoles_RolId",
                        column: x => x.RolId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FiyatListesi",
                columns: table => new
                {
                    StokBarkod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FiyatSatisGecerlilikBaslangicTarihi = table.Column<DateTime>(type: "date", nullable: false),
                    FiyatSatisGecerlilikBitisTarihi = table.Column<DateTime>(type: "date", nullable: true),
                    SatisFiyati = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FiyatLis__3E0E630A24F35B80", x => new { x.StokBarkod, x.FiyatSatisGecerlilikBaslangicTarihi });
                    table.ForeignKey(
                        name: "FK__FiyatList__StokB__66603565",
                        column: x => x.StokBarkod,
                        principalTable: "Stok",
                        principalColumn: "StokBarkod",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ilac",
                columns: table => new
                {
                    IlacBarkod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IlacAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ilac__D1970658054A94CA", x => x.IlacBarkod);
                    table.ForeignKey(
                        name: "FK__Ilac__IlacBarkod__656C112C",
                        column: x => x.IlacBarkod,
                        principalTable: "Stok",
                        principalColumn: "StokBarkod",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StokHareket",
                columns: table => new
                {
                    StokHareketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StokHareketTarihi = table.Column<DateTime>(type: "date", nullable: true),
                    StokBarkod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SatisTarihi = table.Column<DateTime>(type: "date", nullable: true),
                    SatisFiyati = table.Column<double>(type: "float", nullable: true),
                    AlisTarihi = table.Column<DateTime>(type: "date", nullable: true),
                    AlisFiyati = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokHareket", x => x.StokHareketId);
                    table.ForeignKey(
                        name: "FK__StokHarek__StokB__6754599E",
                        column: x => x.StokBarkod,
                        principalTable: "Stok",
                        principalColumn: "StokBarkod",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tur_Cins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurId = table.Column<int>(type: "int", nullable: false),
                    CinsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tur_Cins", x => x.Id);
                    table.UniqueConstraint("AK_Tur_Cins_TurId_CinsId", x => new { x.TurId, x.CinsId });
                    table.ForeignKey(
                        name: "FK__Tur_Cins__CinsId__619B8048",
                        column: x => x.CinsId,
                        principalTable: "Cins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Tur_Cins__TurId__628FA481",
                        column: x => x.TurId,
                        principalTable: "Tur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaasOdemeleri",
                columns: table => new
                {
                    CalisanTCKN = table.Column<string>(type: "nvarchar(450)", unicode: false, maxLength: 11, nullable: false),
                    OdemeTarihi = table.Column<DateTime>(type: "date", nullable: false),
                    OdenenTutar = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MaasOdem__4B5856A3A25B87C9", x => new { x.CalisanTCKN, x.OdemeTarihi });
                    table.ForeignKey(
                        name: "FK__MaasOdeme__Calis__6477ECF3",
                        column: x => x.CalisanTCKN,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hayvan",
                columns: table => new
                {
                    HayvanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HayvanAdi = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    HayvanCinsiyet = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    HayvanKilo = table.Column<double>(type: "float", nullable: false),
                    HayvanDogumTarihi = table.Column<DateTime>(type: "date", nullable: false),
                    HayvanOlumTarihi = table.Column<DateTime>(type: "date", nullable: true),
                    RenkId = table.Column<int>(type: "int", nullable: false),
                    TurId = table.Column<int>(type: "int", nullable: false),
                    CinsId = table.Column<int>(type: "int", nullable: false),
                    HayvanAnneId = table.Column<int>(type: "int", nullable: true),
                    HayvanBabaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hayvan", x => x.HayvanId);
                    table.ForeignKey(
                        name: "FK__Hayvan__68487DD7",
                        columns: x => new { x.TurId, x.CinsId },
                        principalTable: "Tur_Cins",
                        principalColumns: new[] { "TurId", "CinsId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Hayvan__HayvanAn__6A30C649",
                        column: x => x.HayvanAnneId,
                        principalTable: "Hayvan",
                        principalColumn: "HayvanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Hayvan__HayvanBa__6B24EA82",
                        column: x => x.HayvanBabaId,
                        principalTable: "Hayvan",
                        principalColumn: "HayvanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Hayvan__RenkId__693CA210",
                        column: x => x.RenkId,
                        principalTable: "Renk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Muayene",
                columns: table => new
                {
                    MuayeneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MuayeneNo = table.Column<int>(type: "int", nullable: false),
                    TedaviId = table.Column<int>(type: "int", nullable: false),
                    HayvanId = table.Column<int>(type: "int", nullable: false),
                    MuayeneTarihi = table.Column<DateTime>(type: "date", nullable: false),
                    SonrakiMuayeneTarihi = table.Column<DateTime>(type: "date", nullable: true),
                    Aciklama = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    HekimkTCKN = table.Column<string>(type: "nvarchar(450)", unicode: false, maxLength: 450, nullable: false),
                    IlacBarkod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muayene", x => x.MuayeneId);
                    table.UniqueConstraint("AK_Muayene_MuayeneNo", x => x.MuayeneNo);
                    table.ForeignKey(
                        name: "FK__Muayene__HayvanI__6C190EBB",
                        column: x => x.HayvanId,
                        principalTable: "Hayvan",
                        principalColumn: "HayvanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Muayene__HekimkT__6E01572D",
                        column: x => x.HekimkTCKN,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SahipHayvan",
                columns: table => new
                {
                    SahipTCKN = table.Column<string>(type: "nvarchar(450)", unicode: false, maxLength: 11, nullable: false),
                    HayvanId = table.Column<int>(type: "int", nullable: false),
                    SahiplikTarihi = table.Column<DateTime>(type: "date", nullable: false),
                    SahiplikCikisTarihi = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SahipHay__5121BF0ED6BEC62E", x => new { x.SahipTCKN, x.HayvanId });
                    table.ForeignKey(
                        name: "FK__SahipHayv__Hayva__6EF57B66",
                        column: x => x.HayvanId,
                        principalTable: "Hayvan",
                        principalColumn: "HayvanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__SahipHayv__Sahip__6FE99F9F",
                        column: x => x.SahipTCKN,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ilac_Muayene",
                columns: table => new
                {
                    Ilac_IlacBarkod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MuayeneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ilac_Mua__ED0D6E09ABA889E5", x => new { x.Ilac_IlacBarkod, x.MuayeneId });
                    table.ForeignKey(
                        name: "FK__Ilac_Muay__Ilac___72C60C4A",
                        column: x => x.Ilac_IlacBarkod,
                        principalTable: "Ilac",
                        principalColumn: "IlacBarkod",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Ilac_Muay__Muaye__73BA3083",
                        column: x => x.MuayeneId,
                        principalTable: "Muayene",
                        principalColumn: "MuayeneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MuayeneGelirleri",
                columns: table => new
                {
                    MuayeneNo = table.Column<int>(type: "int", nullable: false),
                    Gelir = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MuayeneG__FCCE33ECD0D81D69", x => x.MuayeneNo);
                    table.ForeignKey(
                        name: "FK__MuayeneGe__Muaye__6383C8BA",
                        column: x => x.MuayeneNo,
                        principalTable: "Muayene",
                        principalColumn: "MuayeneNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tedavi_Muayene",
                columns: table => new
                {
                    Tedavi_TedaviId = table.Column<int>(type: "int", nullable: false),
                    MuayeneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tedavi_M__6D36A071149549FC", x => new { x.Tedavi_TedaviId, x.MuayeneId });
                    table.ForeignKey(
                        name: "FK__Tedavi_Mu__Muaye__71D1E811",
                        column: x => x.MuayeneId,
                        principalTable: "Muayene",
                        principalColumn: "MuayeneId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Tedavi_Mu__Tedav__70DDC3D8",
                        column: x => x.Tedavi_TedaviId,
                        principalTable: "Tedavi",
                        principalColumn: "TedaviId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RolId",
                table: "AspNetUsers",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "UQ__Insan__04869AE378B9FE0E",
                table: "AspNetUsers",
                column: "DiplomaNo",
                unique: true,
                filter: "[DiplomaNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Cins__AEC43224DE3A7177",
                table: "Cins",
                column: "cins",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hayvan_HayvanAnneId",
                table: "Hayvan",
                column: "HayvanAnneId");

            migrationBuilder.CreateIndex(
                name: "IX_Hayvan_HayvanBabaId",
                table: "Hayvan",
                column: "HayvanBabaId");

            migrationBuilder.CreateIndex(
                name: "IX_Hayvan_RenkId",
                table: "Hayvan",
                column: "RenkId");

            migrationBuilder.CreateIndex(
                name: "IX_Hayvan_TurId_CinsId",
                table: "Hayvan",
                columns: new[] { "TurId", "CinsId" });

            migrationBuilder.CreateIndex(
                name: "IX_Ilac_Muayene_MuayeneId",
                table: "Ilac_Muayene",
                column: "MuayeneId");

            migrationBuilder.CreateIndex(
                name: "IX_Muayene_HayvanId",
                table: "Muayene",
                column: "HayvanId");

            migrationBuilder.CreateIndex(
                name: "IX_Muayene_HekimkTCKN",
                table: "Muayene",
                column: "HekimkTCKN");

            migrationBuilder.CreateIndex(
                name: "UQ__Muayene__FCCE33ED99F96EED",
                table: "Muayene",
                column: "MuayeneNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Renk__DC85F3ED9E71DFF7",
                table: "Renk",
                column: "Renk",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SahipHayvan_HayvanId",
                table: "SahipHayvan",
                column: "HayvanId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareket_StokBarkod",
                table: "StokHareket",
                column: "StokBarkod");

            migrationBuilder.CreateIndex(
                name: "UQ__Tedavi__4A6347922ECA106F",
                table: "Tedavi",
                column: "TedaviAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tedavi_Muayene_MuayeneId",
                table: "Tedavi_Muayene",
                column: "MuayeneId");

            migrationBuilder.CreateIndex(
                name: "UQ__Tur__C45078B3ABBB8A4C",
                table: "Tur",
                column: "Tur",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tur_Cins_CinsId",
                table: "Tur_Cins",
                column: "CinsId");

            migrationBuilder.CreateIndex(
                name: "unique_tur_cins",
                table: "Tur_Cins",
                columns: new[] { "TurId", "CinsId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FiyatListesi");

            migrationBuilder.DropTable(
                name: "Ilac_Muayene");

            migrationBuilder.DropTable(
                name: "MaasOdemeleri");

            migrationBuilder.DropTable(
                name: "MuayeneGelirleri");

            migrationBuilder.DropTable(
                name: "SahipHayvan");

            migrationBuilder.DropTable(
                name: "StokHareket");

            migrationBuilder.DropTable(
                name: "Tedavi_Muayene");

            migrationBuilder.DropTable(
                name: "Ilac");

            migrationBuilder.DropTable(
                name: "Muayene");

            migrationBuilder.DropTable(
                name: "Tedavi");

            migrationBuilder.DropTable(
                name: "Stok");

            migrationBuilder.DropTable(
                name: "Hayvan");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tur_Cins");

            migrationBuilder.DropTable(
                name: "Renk");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Cins");

            migrationBuilder.DropTable(
                name: "Tur");
        }
    }
}
