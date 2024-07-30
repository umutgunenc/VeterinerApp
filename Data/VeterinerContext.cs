﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VeterinerApp.Models.Entity;

#nullable disable

namespace VeterinerApp.Data
{
    public partial class VeterinerContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public VeterinerContext()
        {
        }

        public VeterinerContext(DbContextOptions<VeterinerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cins> Cins { get; set; }
        public virtual DbSet<FiyatListesi> FiyatListesis { get; set; }
        public virtual DbSet<Hayvan> Hayvans { get; set; }
        public virtual DbSet<Ilac> Ilacs { get; set; }
        public virtual DbSet<IlacMuayene> IlacMuayenes { get; set; }
        public virtual DbSet<AppUser> Insans { get; set; }
        public virtual DbSet<MaasOdemeleri> MaasOdemeleris { get; set; }
        public virtual DbSet<Muayene> Muayenes { get; set; }
        public virtual DbSet<MuayeneGelirleri> MuayeneGelirleris { get; set; }
        public virtual DbSet<Renk> Renks { get; set; }
        public virtual DbSet<SahipHayvan> SahipHayvans { get; set; }
        public virtual DbSet<Stok> Stoks { get; set; }
        public virtual DbSet<StokHareket> StokHarekets { get; set; }
        public virtual DbSet<Tedavi> Tedavis { get; set; }
        public virtual DbSet<TedaviMuayene> TedaviMuayenes { get; set; }
        public virtual DbSet<Tur> Turs { get; set; }
        public virtual DbSet<TurCins> TurCins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-5OQQMU6\\SQLEXPRESS;Database=Veteriner2;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cins>(entity =>
            {
                entity.HasIndex(e => e.cins, "UQ__Cins__AEC43224DE3A7177")
                    .IsUnique();

                entity.Property(e => e.cins)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FiyatListesi>(entity =>
            {
                entity.HasKey(e => new { e.StokBarkod, e.FiyatSatisGecerlilikBaslangicTarihi })
                    .HasName("PK__FiyatLis__3E0E630A24F35B80");

                entity.ToTable("FiyatListesi");

                entity.Property(e => e.StokBarkod).HasMaxLength(100);

                entity.Property(e => e.FiyatSatisGecerlilikBaslangicTarihi).HasColumnType("date");

                entity.Property(e => e.FiyatSatisGecerlilikBitisTarihi).HasColumnType("date");

                entity.HasOne(d => d.StokBarkodNavigation)
                    .WithMany(p => p.FiyatListesis)
                    .HasForeignKey(d => d.StokBarkod)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FiyatList__StokB__66603565");
            });

            modelBuilder.Entity<Hayvan>(entity =>
            {
                entity.ToTable("Hayvan");

                entity.Property(e => e.HayvanAdi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HayvanCinsiyet)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.HayvanDogumTarihi).HasColumnType("date");

                entity.Property(e => e.HayvanOlumTarihi).HasColumnType("date");

                entity.HasOne(d => d.HayvanAnne)
                    .WithMany(p => p.InverseHayvanAnne)
                    .HasForeignKey(d => d.HayvanAnneId)
                    .HasConstraintName("FK__Hayvan__HayvanAn__6A30C649");

                entity.HasOne(d => d.HayvanBaba)
                    .WithMany(p => p.InverseHayvanBaba)
                    .HasForeignKey(d => d.HayvanBabaId)
                    .HasConstraintName("FK__Hayvan__HayvanBa__6B24EA82");

                entity.HasOne(d => d.Renk)
                    .WithMany(p => p.Hayvans)
                    .HasForeignKey(d => d.RenkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Hayvan__RenkId__693CA210");

                entity.HasOne(d => d.TurCin)
                    .WithMany(p => p.Hayvans)
                    .HasPrincipalKey(p => new { p.TurId, p.CinsId })
                    .HasForeignKey(d => new { d.TurId, d.CinsId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Hayvan__68487DD7");
            });

            modelBuilder.Entity<Ilac>(entity =>
            {
                entity.HasKey(e => e.IlacBarkod)
                    .HasName("PK__Ilac__D1970658054A94CA");

                entity.ToTable("Ilac");

                entity.Property(e => e.IlacBarkod).HasMaxLength(100);

                entity.Property(e => e.IlacAdi)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IlacBarkodNavigation)
                    .WithOne(p => p.Ilac)
                    .HasForeignKey<Ilac>(d => d.IlacBarkod)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ilac__IlacBarkod__656C112C");
            });

            modelBuilder.Entity<IlacMuayene>(entity =>
            {
                entity.HasKey(e => new { e.IlacIlacBarkod, e.MuayeneId })
                    .HasName("PK__Ilac_Mua__ED0D6E09ABA889E5");

                entity.ToTable("Ilac_Muayene");

                entity.Property(e => e.IlacIlacBarkod)
                    .HasMaxLength(100)
                    .HasColumnName("Ilac_IlacBarkod");

                entity.HasOne(d => d.IlacIlacBarkodNavigation)
                    .WithMany(p => p.IlacMuayenes)
                    .HasForeignKey(d => d.IlacIlacBarkod)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ilac_Muay__Ilac___72C60C4A");

                entity.HasOne(d => d.Muayene)
                    .WithMany(p => p.IlacMuayenes)
                    .HasForeignKey(d => d.MuayeneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ilac_Muay__Muaye__73BA3083");
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.InsanTckn)
                    .HasName("PK__Insan__1D10AE4008848862");

                entity.ToTable("Insan");

                entity.HasIndex(e => e.DiplomaNo, "UQ__Insan__04869AE378B9FE0E")
                    .IsUnique();

                entity.Property(e => e.InsanTckn)
                    .HasMaxLength(450)
                    .IsUnicode(false)
                    .HasColumnName("InsanTCKN");

                entity.Property(e => e.DiplomaNo)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.InsanAdi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsanSoyadi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<MaasOdemeleri>(entity =>
            {
                entity.HasKey(e => new { e.CalisanTckn, e.OdemeTarihi })
                    .HasName("PK__MaasOdem__4B5856A3A25B87C9");

                entity.ToTable("MaasOdemeleri");

                entity.Property(e => e.CalisanTckn)
                    .HasMaxLength(450)
                    .IsUnicode(false)
                    .HasColumnName("CalisanTCKN");

                entity.Property(e => e.OdemeTarihi).HasColumnType("date");

                entity.HasOne(d => d.CalisanTcknNavigation)
                    .WithMany(p => p.MaasOdemeleris)
                    .HasForeignKey(d => d.CalisanTckn)
                    .HasPrincipalKey(p => p.InsanTckn)  // Principal key olarak InsanTckn kullanılıyor
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MaasOdeme__Calis__6477ECF3");
            });

            modelBuilder.Entity<Muayene>(entity =>
            {
                entity.ToTable("Muayene");

                entity.HasIndex(e => e.MuayeneNo, "UQ__Muayene__FCCE33ED99F96EED")
                    .IsUnique();

                entity.Property(e => e.Aciklama)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.HekimkTckn)
                    .IsRequired()
                    .HasMaxLength(450)
                    .IsUnicode(false)
                    .HasColumnName("HekimkTCKN");

                entity.Property(e => e.IlacBarkod).HasMaxLength(100);

                entity.Property(e => e.MuayeneTarihi).HasColumnType("date");

                entity.Property(e => e.SonrakiMuayeneTarihi).HasColumnType("date");

                entity.HasOne(d => d.Hayvan)
                    .WithMany(p => p.Muayenes)
                    .HasForeignKey(d => d.HayvanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Muayene__HayvanI__6C190EBB");

                entity.HasOne(d => d.HekimkTcknNavigation)
                    .WithMany(p => p.Muayenes)
                    .HasForeignKey(d => d.HekimkTckn)
                    .HasPrincipalKey(p => p.InsanTckn) // Principal key olarak InsanTckn kullanılıyor
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Muayene__HekimkT__6E01572D");
            });

            modelBuilder.Entity<MuayeneGelirleri>(entity =>
            {
                entity.HasKey(e => e.MuayeneNo)
                    .HasName("PK__MuayeneG__FCCE33ECD0D81D69");

                entity.ToTable("MuayeneGelirleri");

                entity.Property(e => e.MuayeneNo).ValueGeneratedNever();

                entity.HasOne(d => d.MuayeneNoNavigation)
                    .WithOne(p => p.MuayeneGelirleri)
                    .HasPrincipalKey<Muayene>(p => p.MuayeneNo)
                    .HasForeignKey<MuayeneGelirleri>(d => d.MuayeneNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MuayeneGe__Muaye__6383C8BA");
            });

            modelBuilder.Entity<Renk>(entity =>
            {
                entity.ToTable("Renk");

                entity.HasIndex(e => e.renk, "UQ__Renk__DC85F3ED9E71DFF7")
                    .IsUnique();

                entity.Property(e => e.renk)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Renk");
            });

            modelBuilder.Entity<SahipHayvan>(entity =>
            {
                entity.HasKey(e => new { e.SahipTckn, e.HayvanId })
                    .HasName("PK__SahipHay__5121BF0ED6BEC62E");

                entity.ToTable("SahipHayvan");

                entity.Property(e => e.SahipTckn)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("SahipTCKN");

                entity.Property(e => e.SahiplikCikisTarihi).HasColumnType("date");

                entity.Property(e => e.SahiplikTarihi).HasColumnType("date");

                entity.HasOne(d => d.Hayvan)
                    .WithMany(p => p.SahipHayvans)
                    .HasForeignKey(d => d.HayvanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SahipHayv__Hayva__6EF57B66");

                entity.HasOne(d => d.SahipTcknNavigation)
                    .WithMany(p => p.SahipHayvans)
                    .HasForeignKey(d => d.SahipTckn)
                    .HasPrincipalKey(p => p.InsanTckn) // Principal key olarak InsanTckn kullanılıyor
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SahipHayv__Sahip__6FE99F9F");
            });

            modelBuilder.Entity<Stok>(entity =>
            {
                entity.HasKey(e => e.StokBarkod)
                    .HasName("PK__Stok__5E87D66AAB303988");

                entity.ToTable("Stok");

                entity.Property(e => e.StokBarkod).HasMaxLength(100);

                entity.Property(e => e.StokAdi)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<StokHareket>(entity =>
            {
                entity.ToTable("StokHareket");

                entity.Property(e => e.AlisTarihi).HasColumnType("date");

                entity.Property(e => e.SatisTarihi).HasColumnType("date");

                entity.Property(e => e.StokBarkod).HasMaxLength(100);

                entity.Property(e => e.StokHareketTarihi).HasColumnType("date");

                entity.HasOne(d => d.StokBarkodNavigation)
                    .WithMany(p => p.StokHarekets)
                    .HasForeignKey(d => d.StokBarkod)
                    .HasConstraintName("FK__StokHarek__StokB__6754599E");
            });

            modelBuilder.Entity<Tedavi>(entity =>
            {
                entity.ToTable("Tedavi");

                entity.HasIndex(e => e.TedaviAdi, "UQ__Tedavi__4A6347922ECA106F")
                    .IsUnique();

                entity.Property(e => e.TedaviAdi)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TedaviMuayene>(entity =>
            {
                entity.HasKey(e => new { e.TedaviTedaviId, e.MuayeneId })
                    .HasName("PK__Tedavi_M__6D36A071149549FC");

                entity.ToTable("Tedavi_Muayene");

                entity.Property(e => e.TedaviTedaviId).HasColumnName("Tedavi_TedaviId");

                entity.HasOne(d => d.Muayene)
                    .WithMany(p => p.TedaviMuayenes)
                    .HasForeignKey(d => d.MuayeneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tedavi_Mu__Muaye__71D1E811");

                entity.HasOne(d => d.TedaviTedavi)
                    .WithMany(p => p.TedaviMuayenes)
                    .HasForeignKey(d => d.TedaviTedaviId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tedavi_Mu__Tedav__70DDC3D8");
            });



            modelBuilder.Entity<Tur>(entity =>
            {
                entity.ToTable("Tur");

                entity.HasIndex(e => e.tur, "UQ__Tur__C45078B3ABBB8A4C")
                    .IsUnique();

                entity.Property(e => e.tur)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Tur");
            });

            modelBuilder.Entity<TurCins>(entity =>
            {
                entity.ToTable("Tur_Cins");

                entity.HasIndex(e => new { e.TurId, e.CinsId }, "unique_tur_cins")
                    .IsUnique();

                entity.HasOne(d => d.Cins)
                    .WithMany(p => p.TurCins)
                    .HasForeignKey(d => d.CinsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tur_Cins__CinsId__619B8048");

                entity.HasOne(d => d.Tur)
                    .WithMany(p => p.TurCins)
                    .HasForeignKey(d => d.TurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tur_Cins__TurId__628FA481");
            });

            base.OnModelCreating(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
