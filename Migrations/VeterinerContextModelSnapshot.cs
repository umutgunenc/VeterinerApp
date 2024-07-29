﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VeterinerApp.Data;

namespace VeterinerApp.Migrations
{
    [DbContext(typeof(VeterinerContext))]
    partial class VeterinerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Cins", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("cins")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "cins" }, "UQ__Cins__AEC43224DE3A7177")
                        .IsUnique();

                    b.ToTable("Cins");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.FiyatListesi", b =>
                {
                    b.Property<string>("StokBarkod")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("FiyatSatisGecerlilikBaslangicTarihi")
                        .HasColumnType("date");

                    b.Property<DateTime?>("FiyatSatisGecerlilikBitisTarihi")
                        .HasColumnType("date");

                    b.Property<double>("SatisFiyati")
                        .HasColumnType("float");

                    b.HasKey("StokBarkod", "FiyatSatisGecerlilikBaslangicTarihi")
                        .HasName("PK__FiyatLis__3E0E630A24F35B80");

                    b.ToTable("FiyatListesi");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Hayvan", b =>
                {
                    b.Property<int>("HayvanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CinsId")
                        .HasColumnType("int");

                    b.Property<string>("HayvanAdi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("HayvanAnneId")
                        .HasColumnType("int");

                    b.Property<int?>("HayvanBabaId")
                        .HasColumnType("int");

                    b.Property<string>("HayvanCinsiyet")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .IsFixedLength(true);

                    b.Property<DateTime>("HayvanDogumTarihi")
                        .HasColumnType("date");

                    b.Property<double>("HayvanKilo")
                        .HasColumnType("float");

                    b.Property<DateTime?>("HayvanOlumTarihi")
                        .HasColumnType("date");

                    b.Property<int>("RenkId")
                        .HasColumnType("int");

                    b.Property<int>("TurId")
                        .HasColumnType("int");

                    b.HasKey("HayvanId");

                    b.HasIndex("HayvanAnneId");

                    b.HasIndex("HayvanBabaId");

                    b.HasIndex("RenkId");

                    b.HasIndex("TurId", "CinsId");

                    b.ToTable("Hayvan");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Ilac", b =>
                {
                    b.Property<string>("IlacBarkod")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("IlacAdi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IlacBarkod")
                        .HasName("PK__Ilac__D1970658054A94CA");

                    b.ToTable("Ilac");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.IlacMuayene", b =>
                {
                    b.Property<string>("IlacIlacBarkod")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Ilac_IlacBarkod");

                    b.Property<int>("MuayeneId")
                        .HasColumnType("int");

                    b.HasKey("IlacIlacBarkod", "MuayeneId")
                        .HasName("PK__Ilac_Mua__ED0D6E09ABA889E5");

                    b.HasIndex("MuayeneId");

                    b.ToTable("Ilac_Muayene");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Insan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("CalisiyorMu")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiplomaNo")
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ImgURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsanAdi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("InsanSoyadi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("InsanTckn")
                        .HasMaxLength(450)
                        .IsUnicode(false)
                        .HasColumnType("varchar(450)")
                        .HasColumnName("InsanTCKN");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<double?>("Maas")
                        .HasColumnType("float");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RolId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SifreGecerlilikTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SifreOlusturmaTarihi")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("RolId");

                    b.HasIndex(new[] { "DiplomaNo" }, "UQ__Insan__04869AE378B9FE0E")
                        .IsUnique()
                        .HasFilter("[DiplomaNo] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.MaasOdemeleri", b =>
                {
                    b.Property<string>("CalisanTckn")
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("varchar(11)")
                        .HasColumnName("CalisanTCKN");

                    b.Property<DateTime>("OdemeTarihi")
                        .HasColumnType("date");

                    b.Property<double>("OdenenTutar")
                        .HasColumnType("float");

                    b.HasKey("CalisanTckn", "OdemeTarihi")
                        .HasName("PK__MaasOdem__4B5856A3A25B87C9");

                    b.ToTable("MaasOdemeleri");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Muayene", b =>
                {
                    b.Property<int>("MuayeneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Aciklama")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("HayvanId")
                        .HasColumnType("int");

                    b.Property<string>("HekimkTckn")
                        .IsRequired()
                        .HasMaxLength(450)
                        .IsUnicode(false)
                        .HasColumnType("varchar(450)")
                        .HasColumnName("HekimkTCKN");

                    b.Property<string>("IlacBarkod")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("MuayeneNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("MuayeneTarihi")
                        .HasColumnType("date");

                    b.Property<DateTime?>("SonrakiMuayeneTarihi")
                        .HasColumnType("date");

                    b.Property<int>("TedaviId")
                        .HasColumnType("int");

                    b.HasKey("MuayeneId");

                    b.HasIndex("HayvanId");

                    b.HasIndex("HekimkTckn");

                    b.HasIndex(new[] { "MuayeneNo" }, "UQ__Muayene__FCCE33ED99F96EED")
                        .IsUnique();

                    b.ToTable("Muayene");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.MuayeneGelirleri", b =>
                {
                    b.Property<int>("MuayeneNo")
                        .HasColumnType("int");

                    b.Property<double>("Gelir")
                        .HasColumnType("float");

                    b.HasKey("MuayeneNo")
                        .HasName("PK__MuayeneG__FCCE33ECD0D81D69");

                    b.ToTable("MuayeneGelirleri");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Renk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("renk")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Renk");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "renk" }, "UQ__Renk__DC85F3ED9E71DFF7")
                        .IsUnique();

                    b.ToTable("Renk");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.SahipHayvan", b =>
                {
                    b.Property<string>("SahipTckn")
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("varchar(11)")
                        .HasColumnName("SahipTCKN");

                    b.Property<int>("HayvanId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SahiplikCikisTarihi")
                        .HasColumnType("date");

                    b.Property<DateTime>("SahiplikTarihi")
                        .HasColumnType("date");

                    b.HasKey("SahipTckn", "HayvanId")
                        .HasName("PK__SahipHay__5121BF0ED6BEC62E");

                    b.HasIndex("HayvanId");

                    b.ToTable("SahipHayvan");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Stok", b =>
                {
                    b.Property<string>("StokBarkod")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StokAdi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("StokSayisi")
                        .HasColumnType("int");

                    b.HasKey("StokBarkod")
                        .HasName("PK__Stok__5E87D66AAB303988");

                    b.ToTable("Stok");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.StokHareket", b =>
                {
                    b.Property<int>("StokHareketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double?>("AlisFiyati")
                        .HasColumnType("float");

                    b.Property<DateTime?>("AlisTarihi")
                        .HasColumnType("date");

                    b.Property<double?>("SatisFiyati")
                        .HasColumnType("float");

                    b.Property<DateTime?>("SatisTarihi")
                        .HasColumnType("date");

                    b.Property<string>("StokBarkod")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("StokHareketTarihi")
                        .HasColumnType("date");

                    b.HasKey("StokHareketId");

                    b.HasIndex("StokBarkod");

                    b.ToTable("StokHareket");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Tedavi", b =>
                {
                    b.Property<int>("TedaviId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("TedaviAdi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("TedaviUcreti")
                        .HasColumnType("float");

                    b.HasKey("TedaviId");

                    b.HasIndex(new[] { "TedaviAdi" }, "UQ__Tedavi__4A6347922ECA106F")
                        .IsUnique();

                    b.ToTable("Tedavi");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.TedaviMuayene", b =>
                {
                    b.Property<int>("TedaviTedaviId")
                        .HasColumnType("int")
                        .HasColumnName("Tedavi_TedaviId");

                    b.Property<int>("MuayeneId")
                        .HasColumnType("int");

                    b.HasKey("TedaviTedaviId", "MuayeneId")
                        .HasName("PK__Tedavi_M__6D36A071149549FC");

                    b.HasIndex("MuayeneId");

                    b.ToTable("Tedavi_Muayene");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Tur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("tur")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Tur");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "tur" }, "UQ__Tur__C45078B3ABBB8A4C")
                        .IsUnique();

                    b.ToTable("Tur");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.TurCins", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CinsId")
                        .HasColumnType("int");

                    b.Property<int>("TurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CinsId");

                    b.HasIndex(new[] { "TurId", "CinsId" }, "unique_tur_cins")
                        .IsUnique();

                    b.ToTable("Tur_Cins");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Rol", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.HasDiscriminator().HasValue("Rol");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Insan", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Insan", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.Insan", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Insan", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.FiyatListesi", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Stok", "StokBarkodNavigation")
                        .WithMany("FiyatListesis")
                        .HasForeignKey("StokBarkod")
                        .HasConstraintName("FK__FiyatList__StokB__66603565")
                        .IsRequired();

                    b.Navigation("StokBarkodNavigation");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Hayvan", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Hayvan", "HayvanAnne")
                        .WithMany("InverseHayvanAnne")
                        .HasForeignKey("HayvanAnneId")
                        .HasConstraintName("FK__Hayvan__HayvanAn__6A30C649");

                    b.HasOne("VeterinerApp.Models.Entity.Hayvan", "HayvanBaba")
                        .WithMany("InverseHayvanBaba")
                        .HasForeignKey("HayvanBabaId")
                        .HasConstraintName("FK__Hayvan__HayvanBa__6B24EA82");

                    b.HasOne("VeterinerApp.Models.Entity.Renk", "Renk")
                        .WithMany("Hayvans")
                        .HasForeignKey("RenkId")
                        .HasConstraintName("FK__Hayvan__RenkId__693CA210")
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.TurCins", "TurCin")
                        .WithMany("Hayvans")
                        .HasForeignKey("TurId", "CinsId")
                        .HasConstraintName("FK__Hayvan__68487DD7")
                        .HasPrincipalKey("TurId", "CinsId")
                        .IsRequired();

                    b.Navigation("HayvanAnne");

                    b.Navigation("HayvanBaba");

                    b.Navigation("Renk");

                    b.Navigation("TurCin");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Ilac", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Stok", "IlacBarkodNavigation")
                        .WithOne("Ilac")
                        .HasForeignKey("VeterinerApp.Models.Entity.Ilac", "IlacBarkod")
                        .HasConstraintName("FK__Ilac__IlacBarkod__656C112C")
                        .IsRequired();

                    b.Navigation("IlacBarkodNavigation");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.IlacMuayene", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Ilac", "IlacIlacBarkodNavigation")
                        .WithMany("IlacMuayenes")
                        .HasForeignKey("IlacIlacBarkod")
                        .HasConstraintName("FK__Ilac_Muay__Ilac___72C60C4A")
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.Muayene", "Muayene")
                        .WithMany("IlacMuayenes")
                        .HasForeignKey("MuayeneId")
                        .HasConstraintName("FK__Ilac_Muay__Muaye__73BA3083")
                        .IsRequired();

                    b.Navigation("IlacIlacBarkodNavigation");

                    b.Navigation("Muayene");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Insan", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Rol", "Rol")
                        .WithMany("Insans")
                        .HasForeignKey("RolId");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.MaasOdemeleri", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Insan", "CalisanTcknNavigation")
                        .WithMany("MaasOdemeleris")
                        .HasForeignKey("CalisanTckn")
                        .HasConstraintName("FK__MaasOdeme__Calis__6477ECF3")
                        .IsRequired();

                    b.Navigation("CalisanTcknNavigation");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Muayene", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Hayvan", "Hayvan")
                        .WithMany("Muayenes")
                        .HasForeignKey("HayvanId")
                        .HasConstraintName("FK__Muayene__HayvanI__6C190EBB")
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.Insan", "HekimkTcknNavigation")
                        .WithMany("Muayenes")
                        .HasForeignKey("HekimkTckn")
                        .HasConstraintName("FK__Muayene__HekimkT__6E01572D")
                        .IsRequired();

                    b.Navigation("Hayvan");

                    b.Navigation("HekimkTcknNavigation");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.MuayeneGelirleri", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Muayene", "MuayeneNoNavigation")
                        .WithOne("MuayeneGelirleri")
                        .HasForeignKey("VeterinerApp.Models.Entity.MuayeneGelirleri", "MuayeneNo")
                        .HasConstraintName("FK__MuayeneGe__Muaye__6383C8BA")
                        .HasPrincipalKey("VeterinerApp.Models.Entity.Muayene", "MuayeneNo")
                        .IsRequired();

                    b.Navigation("MuayeneNoNavigation");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.SahipHayvan", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Hayvan", "Hayvan")
                        .WithMany("SahipHayvans")
                        .HasForeignKey("HayvanId")
                        .HasConstraintName("FK__SahipHayv__Hayva__6EF57B66")
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.Insan", "SahipTcknNavigation")
                        .WithMany("SahipHayvans")
                        .HasForeignKey("SahipTckn")
                        .HasConstraintName("FK__SahipHayv__Sahip__6FE99F9F")
                        .IsRequired();

                    b.Navigation("Hayvan");

                    b.Navigation("SahipTcknNavigation");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.StokHareket", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Stok", "StokBarkodNavigation")
                        .WithMany("StokHarekets")
                        .HasForeignKey("StokBarkod")
                        .HasConstraintName("FK__StokHarek__StokB__6754599E");

                    b.Navigation("StokBarkodNavigation");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.TedaviMuayene", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Muayene", "Muayene")
                        .WithMany("TedaviMuayenes")
                        .HasForeignKey("MuayeneId")
                        .HasConstraintName("FK__Tedavi_Mu__Muaye__71D1E811")
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.Tedavi", "TedaviTedavi")
                        .WithMany("TedaviMuayenes")
                        .HasForeignKey("TedaviTedaviId")
                        .HasConstraintName("FK__Tedavi_Mu__Tedav__70DDC3D8")
                        .IsRequired();

                    b.Navigation("Muayene");

                    b.Navigation("TedaviTedavi");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.TurCins", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Cins", "Cins")
                        .WithMany("TurCins")
                        .HasForeignKey("CinsId")
                        .HasConstraintName("FK__Tur_Cins__CinsId__619B8048")
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.Tur", "Tur")
                        .WithMany("TurCins")
                        .HasForeignKey("TurId")
                        .HasConstraintName("FK__Tur_Cins__TurId__628FA481")
                        .IsRequired();

                    b.Navigation("Cins");

                    b.Navigation("Tur");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Cins", b =>
                {
                    b.Navigation("TurCins");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Hayvan", b =>
                {
                    b.Navigation("InverseHayvanAnne");

                    b.Navigation("InverseHayvanBaba");

                    b.Navigation("Muayenes");

                    b.Navigation("SahipHayvans");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Ilac", b =>
                {
                    b.Navigation("IlacMuayenes");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Insan", b =>
                {
                    b.Navigation("MaasOdemeleris");

                    b.Navigation("Muayenes");

                    b.Navigation("SahipHayvans");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Muayene", b =>
                {
                    b.Navigation("IlacMuayenes");

                    b.Navigation("MuayeneGelirleri");

                    b.Navigation("TedaviMuayenes");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Renk", b =>
                {
                    b.Navigation("Hayvans");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Stok", b =>
                {
                    b.Navigation("FiyatListesis");

                    b.Navigation("Ilac");

                    b.Navigation("StokHarekets");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Tedavi", b =>
                {
                    b.Navigation("TedaviMuayenes");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Tur", b =>
                {
                    b.Navigation("TurCins");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.TurCins", b =>
                {
                    b.Navigation("Hayvans");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Rol", b =>
                {
                    b.Navigation("Insans");
                });
#pragma warning restore 612, 618
        }
    }
}
