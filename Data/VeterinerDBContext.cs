using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Data
{
    public class VeterinerDBContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public VeterinerDBContext()
        {

        }
        public VeterinerDBContext(DbContextOptions<VeterinerDBContext> options) : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Birim> Birimler { get; set; }
        public virtual DbSet<Cins> Cinsler { get; set; }
        public virtual DbSet<FiyatListesi> FiyatListeleri { get; set; }
        public virtual DbSet<Hayvan> Hayvanlar { get; set; }
        public virtual DbSet<Kategori> Kategoriler { get; set; }
        public virtual DbSet<MaasOdemeleri> MaasOdemeleri { get; set; }
        public virtual DbSet<Muayene> Muayeneler { get; set; }
        public virtual DbSet<Renk> Renkler { get; set; }
        public virtual DbSet<Stok> Stoklar { get; set; }
        public virtual DbSet<StokHareket> StokHareketler { get; set; }
        public virtual DbSet<Tedavi> Tedaviler { get; set; }
        public virtual DbSet<Tur> Turler { get; set; }

        //Ara tablolar
        public virtual DbSet<CinsTur> CinsTur { get; set; }
        public virtual DbSet<MuayeneStok> MuayeneStoklar { get; set; }
        public virtual DbSet<SahipHayvan> SahipHayvanlar { get; set; }
        public virtual DbSet<TedaviMuayene> TedaviMuayeneler { get; set; }
        public virtual DbSet<StokMuayene> StokMuayeneler { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-5OQQMU6\\SQLEXPRESS;Database=Veteriner2;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hayvan>(entity =>
            {
                entity.HasOne(e => e.HayvanAnne)
                .WithMany(p => p.AnneCocuklari)
                .HasForeignKey(d => d.HayvanAnneId)
                .HasConstraintName("FK__Hayvan__Anne");

                entity.HasOne(e => e.HayvanBaba)
                .WithMany(p => p.BabaCocuklari)
                .HasForeignKey(d => d.HayvanBabaId)
                .HasConstraintName("FK__Hayvan__Baba");

            });


            modelBuilder.Entity<AppRole>()
                .HasData(new AppRole { Id = 1, Name = "ADMIN", NormalizedName = "ADMIN" });

            var admin = new AppUser
            {
                Id = 1,
                UserName = "ADMIN",
                Email = "umutgunenc@gmail.com",
                InsanAdi = "Umut",
                InsanSoyadi = "Günenç",
                InsanTckn = "33080423902",
                CalisiyorMu = true,
                SifreOlusturmaTarihi = System.DateTime.Now,
                SifreGecerlilikTarihi = System.DateTime.Now.AddYears(999),
                TermOfUse = true,
                PhoneNumber = "05300000000",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "UMUTGUNENC@GMAİL.COM",

            };
            var passwordHasher = new PasswordHasher<AppUser>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "123123123");
            admin.SecurityStamp = Guid.NewGuid().ToString();
            modelBuilder.Entity<AppUser>().HasData(admin);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 1, UserId = 1 }
            );

            base.OnModelCreating(modelBuilder);

        }

    }
}
