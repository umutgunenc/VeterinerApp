using FluentValidation;
using System;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

#nullable disable

namespace VeterinerApp.Models.Validators.kullanılmayanlar
{
    public partial class SahipHayvanValidators : AbstractValidator<SahipHayvan>
    {
        private readonly VeterinerContext _context;

        public SahipHayvanValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.SahipTckn)
                .NotEmpty().WithMessage("Evcil hayvan için bir sahip seçiniz.")
                .NotNull().WithMessage("Evcil hayvan için bir sahip seçiniz.")
                .Must(BeInsan).WithMessage("Seçilen TCKN sisteme kayıtlı olmalı.");

            RuleFor(x => x.HayvanId)
                .NotEmpty().WithMessage("Bir evcil hayvan seçiniz.")
                .NotNull().WithMessage("Bir evcil hayvan seçiniz.")
                .Must(BeHayvan).WithMessage("Seçilen hayvan sisteme kayıtlı olmalı.");

            RuleFor(x => x.SahiplikTarihi)
                .NotNull().WithMessage("Sahiplik tarihi giriniz.")
                .NotEmpty().WithMessage("Sahiplik tarihi giriniz.")
                .Must(x => x is DateTime).WithMessage("Lütfen geçerli bir tarih giriniz.")
                .Must(x => x <= DateTime.Now).WithMessage("İleri bir tarih girilemez");


            RuleFor(x => x.SahiplikCikisTarihi)
                .NotNull().WithMessage("Sahiplikten çıkış tarihini giriniz.")
                .NotEmpty().WithMessage("Sahiplikten çıkış tarihini giriniz.")
                .Must(x => x is DateTime).WithMessage("Lütfen geçerli bir tarih giriniz.")
                .Must(x => x >= DateTime.Now).WithMessage("Geçmiş bir tarih girilemez");

        }
        private bool BeInsan(string TCKN)
        {
            return _context.Insans.Any(x => x.InsanTckn.ToUpper() == TCKN.ToUpper());
        }
        private bool BeHayvan(int hayvanId)
        {
            return _context.Hayvans.Any(x => x.HayvanId == hayvanId);
        }
    }
}
