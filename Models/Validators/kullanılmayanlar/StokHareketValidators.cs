using FluentValidation;
using System;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

#nullable disable

namespace VeterinerApp.Models.Validators.kullanılmayanlar
{
    public partial class StokHareketValidators : AbstractValidator<StokHareket>
    {
        private readonly VeterinerContext _context;
        public StokHareketValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.StokHareketId)
                .NotEmpty().WithMessage("Boş bırakılamaz.")
                .NotEmpty().WithMessage("Boş bırakılamaz.")
                .Must(x => x is int).WithMessage("Geçerlin bir ID giriniz.");

            RuleFor(x => x.StokBarkod)
                .NotNull().WithMessage("Lütfen bir barkod numarası giriniz.")
                .NotEmpty().WithMessage("Lütfen bir barkod numarası giriniz.")
                .MaximumLength(100).WithMessage("Barkod numarası maksimum 100 karakter uzunluğunda olmalıdır.")
                .Must(BeBarkod).WithMessage("Seçilen barkod sisteme kayıtlı olmalıdır.");

            RuleFor(x => x.StokHareketTarihi)
                .NotNull().WithMessage("Lütfen bir tarih giriniz.")
                .NotEmpty().WithMessage("Lütfen bir tarih giriniz.")
                .Must(x => x is DateTime).WithMessage("Lütfen geçerli bir tarih giriniz.")
                .Must(x => x is DateTime && x <= DateTime.Now).WithMessage("Gelecekten bir tarih girilemez.");

            RuleFor(x => x.SatisTarihi)
                .Must(x => !x.HasValue || x is DateTime).WithMessage("Geçerli bir tarih giriniz.")
                .Must(x => !x.HasValue || x.HasValue && x.Value <= DateTime.Now).WithMessage("Satış tarihi gelecekten bir tarih olamaz.");

            RuleFor(x => x.AlisTarihi)
                .NotEmpty().WithMessage("Alış tarihi boş olamaz.")
                .NotNull().WithMessage("Alış tarihi boş olamaz.")
                .Must(x => x is DateTime && x <= DateTime.Now).WithMessage("Gelecekten bir tarih girilemez.")
                .Must(x => x is DateTime).WithMessage("Lütfen geçerli bir tarih giriniz.");

            RuleFor(x => x.AlisFiyati)
                .NotNull().WithMessage("Lütfen alış fiyatını giriniz.")
                .NotEmpty().WithMessage("Lütfen alış fiyatını giriniz.")
                .Must(x => x is float).WithMessage("Lütfen geçerli bir fiyat giriniz.")
                .Must(x => x >= 0).WithMessage("Lütfen geçerli bir fiyat giriniz.");

            RuleFor(x => x.SatisFiyati)
                .Must(x => x is double?).WithMessage("Lütfen geçerli bir fiyat giriniz.")
                .Must(x => x >= 0).WithMessage("Lütfen geçerli bir fiyat giriniz.")
                .Must(BeFiyatListesi).WithMessage("Seçilen fiyat, Fiyat Listesinde bulunmamaktadır.");



        }

        private bool BeBarkod(string barkod)
        {
            return _context.Stoks.Any(x => x.StokBarkod.ToUpper() == barkod.ToUpper());
        }

        private bool BeFiyatListesi(double? fiyat)
        {
            return _context.FiyatListesis.Any(x => x.SatisFiyati == fiyat);
        }
    }
}
