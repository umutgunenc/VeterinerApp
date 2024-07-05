using FluentValidation;
using System;
using System.Linq;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using VeterinerApp.Data;



#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class FiyatListesiValidators : AbstractValidator<FiyatListesi>
    {
        private readonly VeterinerContext _context;
        public FiyatListesiValidators(VeterinerContext context)
        {
            _context = context;
            RuleFor(x => x.StokBarkod)
                .NotNull().WithMessage("Lütfen ürünün barkod numarasını seçiniz.")
                .NotEmpty().WithMessage("Lütfen ürünün barkod numarasını seçiniz.")
                .MaximumLength(100).WithMessage("Barkod numarası maksimum 100 karakter uzunluğunda olabilir.")
                .Must(MustBeBarkod).WithMessage("Seçilen barkod numarası sistemde bulunmamaktadır.");

            RuleFor(x => x.FiyatSatisGecerlilikBaslangicTarihi)
                .NotNull().WithMessage("Fiyatın geçerli olduğu tarihi giriniz.")
                .NotEmpty().WithMessage("Fiyatın geçerli olduğu tarihi giriniz.")
                .Must(x => x is DateTime).WithMessage("Lütfen gereçli tarih giriniz.");


            RuleFor(x => x.FiyatSatisGecerlilikBitisTarihi)
                .Must(x => x is DateTime).WithMessage("Lütfen tarih giriniz.")
                .Must((x, date) => date >= x.FiyatSatisGecerlilikBaslangicTarihi)
                .WithMessage("Fiyat Satış Geçerlilik Bitiş Tarihi, Başlangıç Tarihinden önce olamaz.");

            RuleFor(x => x.SatisFiyati)
                .NotNull().WithMessage("Ürün satış fiyatını giriniz.")
                .NotEmpty().WithMessage("Ürün satış fiyatını giriniz.")
                .Must(x => x is double).WithMessage("Lütfen geçerli bir fiyat giriniz.")
                .Must(x => x >= 0).WithMessage("Fiyat değeri negatif olamaz.Lütfen geçerli bir fiyat giriniz.");
        }

        private bool MustBeBarkod(string girilenBarkod)
        {
            return _context.Stoks.Any(x=>x.StokBarkod.ToUpper() == girilenBarkod.ToUpper());
        }
    }
}
