using FluentValidation;
using System;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators.Admin
{
    public class StokGirisKaydetValidator : AbstractValidator<StokGirisKaydetViewModel>
    {
        public StokGirisKaydetValidator()
        {

            RuleFor(x => x.StokId)
                .NotEmpty().WithMessage("Lütfen bir stok seçiniz.")
                .NotNull().WithMessage("Lütfen bir stok seçiniz.");


            RuleFor(x => x.AlisFiyati)
                .NotEmpty().WithMessage("Lütfen birim alış fiyatı giriniz.")
                .NotNull().WithMessage("Lütfen birim alış fiyatı giriniz.")
                .Must(x=>x.HasValue && x.Value >= 0).WithMessage("Birim alış fiyatı 0 veya daha büyük olmaladır.");

            RuleFor(x => x.AlisTarihi)
                .NotNull().WithMessage("Lütfen alış tarihini giriniz.")
                .NotEmpty().WithMessage("Lütfen alış tarihini giriniz.")
                .Must(x => x.HasValue && x.Value <= DateTime.Now).WithMessage("Alış tarihi, bugünden büyük olamaz.");

            RuleFor(x=>x.StokGirisAdet)
                .NotNull().WithMessage("Lütfen stok giriş adedini giriniz.")
                .NotEmpty().WithMessage("Lütfen stok giriş adedini giriniz.")
                .Must(x => x.HasValue && x.Value > 0).WithMessage("Giriş adedi 0'dan büyük olmalıdır.");
        }
    }
}
