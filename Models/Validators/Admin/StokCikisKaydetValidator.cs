using FluentValidation;
using System;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators.Admin
{
    public class StokCikisKaydetValidator : AbstractValidator<StokCikisKaydetViewModel>
    {
        public StokCikisKaydetValidator()
        {

            RuleFor(x => x.StokId)
                .NotEmpty().WithMessage("Lütfen bir stok seçiniz.")
                .NotNull().WithMessage("Lütfen bir stok seçiniz.");        

            RuleFor(x => x.SatisTarihi)
                .NotNull().WithMessage("Lütfen stok çıkış tarihini giriniz.")
                .NotEmpty().WithMessage("Lütfen stok çıkış tarihini giriniz.")
                .Must(x => x.HasValue && x.Value <= DateTime.Now).WithMessage("Satış tarihi, bugünden büyük olamaz.");

            RuleFor(x=>x.StokCikisAdet)
                .NotNull().WithMessage("Lütfen stok çıkış adedini giriniz.")
                .NotEmpty().WithMessage("Lütfen stok çıkış adedini giriniz.")
                .Must(x => x.HasValue && x.Value > 0).WithMessage("Stok çıkış miktarı 0'dan büyük olmalıdır.")
                .Must((model,adet)=>FunctionsValidator.BePositiveStock(model,adet)).WithMessage("Aktif stok sayısı negatif olacaktır. Stok miktarlarını kontrol ediniz.");
            
        }
    }
}
