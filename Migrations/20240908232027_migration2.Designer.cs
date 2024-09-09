﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VeterinerApp.Data;

namespace VeterinerApp.Migrations
{
    [DbContext(typeof(VeterinerDBContext))]
    [Migration("20240908232027_migration2")]
    partial class migration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("AppUserHayvan", b =>
                {
                    b.Property<int>("HayvanlarHayvanId")
                        .HasColumnType("int");

                    b.Property<int>("SahiplerId")
                        .HasColumnType("int");

                    b.HasKey("HayvanlarHayvanId", "SahiplerId");

                    b.HasIndex("SahiplerId");

                    b.ToTable("AppUserHayvan");
                });

            modelBuilder.Entity("MuayeneStok", b =>
                {
                    b.Property<int>("MuayenelerMuayeneId")
                        .HasColumnType("int");

                    b.Property<int>("StoklarId")
                        .HasColumnType("int");

                    b.HasKey("MuayenelerMuayeneId", "StoklarId");

                    b.HasIndex("StoklarId");

                    b.ToTable("MuayeneStok");
                });

            modelBuilder.Entity("MuayeneTedavi", b =>
                {
                    b.Property<int>("MuayenelerMuayeneId")
                        .HasColumnType("int");

                    b.Property<int>("TedavilerTedaviId")
                        .HasColumnType("int");

                    b.HasKey("MuayenelerMuayeneId", "TedavilerTedaviId");

                    b.HasIndex("TedavilerTedaviId");

                    b.ToTable("MuayeneTedavi");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("CalisiyorMu")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiplomaNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ImgURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsanAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsanSoyadi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsanTckn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<double?>("Maas")
                        .HasColumnType("float");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SifreGecerlilikTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SifreOlusturmaTarihi")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TermOfUse")
                        .HasColumnType("bit");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Birim", b =>
                {
                    b.Property<int>("BirimId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BirimAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BirimId");

                    b.ToTable("Birimler");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Cins", b =>
                {
                    b.Property<int>("CinsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CinsAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CinsId");

                    b.ToTable("Cinsler");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.CinsTur", b =>
                {
                    b.Property<int>("CinsId")
                        .HasColumnType("int");

                    b.Property<int>("TurId")
                        .HasColumnType("int");

                    b.HasKey("CinsId", "TurId");

                    b.HasIndex("TurId");

                    b.ToTable("CinsTur");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.FiyatListesi", b =>
                {
                    b.Property<int>("FiyatListesiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("FiyatSatisGecerlilikBaslangicTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FiyatSatisGecerlilikBitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<double>("SatisFiyati")
                        .HasColumnType("float");

                    b.Property<int>("StokId")
                        .HasColumnType("int");

                    b.HasKey("FiyatListesiId");

                    b.HasIndex("StokId");

                    b.ToTable("FiyatListeleri");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Hayvan", b =>
                {
                    b.Property<int>("HayvanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CinsTurCinsId")
                        .HasColumnType("int");

                    b.Property<int>("CinsTurTurId")
                        .HasColumnType("int");

                    b.Property<string>("HayvanAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HayvanAnneId")
                        .HasColumnType("int");

                    b.Property<int?>("HayvanBabaId")
                        .HasColumnType("int");

                    b.Property<string>("HayvanCinsiyet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HayvanDogumTarihi")
                        .HasColumnType("datetime2");

                    b.Property<double>("HayvanKilo")
                        .HasColumnType("float");

                    b.Property<DateTime?>("HayvanOlumTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RenkId")
                        .HasColumnType("int");

                    b.HasKey("HayvanId");

                    b.HasIndex("HayvanAnneId");

                    b.HasIndex("HayvanBabaId");

                    b.HasIndex("RenkId");

                    b.HasIndex("CinsTurCinsId", "CinsTurTurId");

                    b.ToTable("Hayvanlar");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Kategori", b =>
                {
                    b.Property<int>("KategoriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("KategoriAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KategoriId");

                    b.ToTable("Kategoriler");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.MaasOdemeleri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CalisanId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OdemeTarihi")
                        .HasColumnType("datetime2");

                    b.Property<double>("OdenenTutar")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CalisanId");

                    b.ToTable("MaasOdemeleri");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Muayene", b =>
                {
                    b.Property<int>("MuayeneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Aciklama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Gelir")
                        .HasColumnType("float");

                    b.Property<int>("HayvanId")
                        .HasColumnType("int");

                    b.Property<int>("HekimId")
                        .HasColumnType("int");

                    b.Property<DateTime>("MuayeneTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SonrakiMuayeneTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("StokId")
                        .HasColumnType("int");

                    b.Property<int>("TedaviId")
                        .HasColumnType("int");

                    b.HasKey("MuayeneId");

                    b.HasIndex("HayvanId");

                    b.HasIndex("HekimId");

                    b.ToTable("Muayeneler");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Renk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("RenkAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Renkler");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Stok", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Aciklama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("AktifMi")
                        .HasColumnType("bit");

                    b.Property<int>("BirimId")
                        .HasColumnType("int");

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.Property<string>("StokAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StokBarkod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StokSayisi")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BirimId");

                    b.HasIndex("KategoriId");

                    b.ToTable("Stoklar");
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
                        .HasColumnType("datetime2");

                    b.Property<int>("CalisanId")
                        .HasColumnType("int");

                    b.Property<double?>("SatisFiyati")
                        .HasColumnType("float");

                    b.Property<DateTime?>("SatisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StokCikisAdet")
                        .HasColumnType("int");

                    b.Property<int?>("StokGirisAdet")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StokHareketTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("StokId")
                        .HasColumnType("int");

                    b.HasKey("StokHareketId");

                    b.HasIndex("CalisanId");

                    b.HasIndex("StokId");

                    b.ToTable("StokHareketler");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Tedavi", b =>
                {
                    b.Property<int>("TedaviId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("TedaviAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TedaviUcreti")
                        .HasColumnType("float");

                    b.HasKey("TedaviId");

                    b.ToTable("Tedaviler");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Tur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("TurAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Turler");
                });

            modelBuilder.Entity("AppUserHayvan", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Hayvan", null)
                        .WithMany()
                        .HasForeignKey("HayvanlarHayvanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("SahiplerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MuayeneStok", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Muayene", null)
                        .WithMany()
                        .HasForeignKey("MuayenelerMuayeneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.Stok", null)
                        .WithMany()
                        .HasForeignKey("StoklarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MuayeneTedavi", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Muayene", null)
                        .WithMany()
                        .HasForeignKey("MuayenelerMuayeneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.Tedavi", null)
                        .WithMany()
                        .HasForeignKey("TedavilerTedaviId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.CinsTur", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Cins", "Cins")
                        .WithMany("CinsTur")
                        .HasForeignKey("CinsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.Tur", "Tur")
                        .WithMany("CinsTur")
                        .HasForeignKey("TurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cins");

                    b.Navigation("Tur");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.FiyatListesi", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Stok", "Stok")
                        .WithMany("FiyatListeleri")
                        .HasForeignKey("StokId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stok");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Hayvan", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Hayvan", "HayvanAnne")
                        .WithMany("AnneCocuklari")
                        .HasForeignKey("HayvanAnneId")
                        .HasConstraintName("FK__Hayvan__Anne");

                    b.HasOne("VeterinerApp.Models.Entity.Hayvan", "HayvanBaba")
                        .WithMany("BabaCocuklari")
                        .HasForeignKey("HayvanBabaId")
                        .HasConstraintName("FK__Hayvan__Baba");

                    b.HasOne("VeterinerApp.Models.Entity.Renk", "Renk")
                        .WithMany("Hayvanlar")
                        .HasForeignKey("RenkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.CinsTur", "CinsTur")
                        .WithMany("Hayvanlar")
                        .HasForeignKey("CinsTurCinsId", "CinsTurTurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CinsTur");

                    b.Navigation("HayvanAnne");

                    b.Navigation("HayvanBaba");

                    b.Navigation("Renk");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.MaasOdemeleri", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.AppUser", "Calisan")
                        .WithMany("MaasOdemeleri")
                        .HasForeignKey("CalisanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calisan");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Muayene", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Hayvan", "Hayvan")
                        .WithMany("Muayeneler")
                        .HasForeignKey("HayvanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.AppUser", "Hekim")
                        .WithMany("Muayeneler")
                        .HasForeignKey("HekimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hayvan");

                    b.Navigation("Hekim");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Stok", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.Birim", "Birim")
                        .WithMany("Stoklar")
                        .HasForeignKey("BirimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.Kategori", "Kategori")
                        .WithMany("Stoklar")
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Birim");

                    b.Navigation("Kategori");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.StokHareket", b =>
                {
                    b.HasOne("VeterinerApp.Models.Entity.AppUser", "Calisan")
                        .WithMany()
                        .HasForeignKey("CalisanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerApp.Models.Entity.Stok", "Stok")
                        .WithMany("StokHareketleri")
                        .HasForeignKey("StokId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calisan");

                    b.Navigation("Stok");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.AppUser", b =>
                {
                    b.Navigation("MaasOdemeleri");

                    b.Navigation("Muayeneler");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Birim", b =>
                {
                    b.Navigation("Stoklar");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Cins", b =>
                {
                    b.Navigation("CinsTur");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.CinsTur", b =>
                {
                    b.Navigation("Hayvanlar");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Hayvan", b =>
                {
                    b.Navigation("AnneCocuklari");

                    b.Navigation("BabaCocuklari");

                    b.Navigation("Muayeneler");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Kategori", b =>
                {
                    b.Navigation("Stoklar");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Renk", b =>
                {
                    b.Navigation("Hayvanlar");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Stok", b =>
                {
                    b.Navigation("FiyatListeleri");

                    b.Navigation("StokHareketleri");
                });

            modelBuilder.Entity("VeterinerApp.Models.Entity.Tur", b =>
                {
                    b.Navigation("CinsTur");
                });
#pragma warning restore 612, 618
        }
    }
}
