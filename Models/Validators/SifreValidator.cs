using FluentValidation;
using System.Collections.Generic;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using System;
using System.Linq;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class SifreValidators : AbstractValidator<Sifre>
    {
        private readonly VeterinerContext _context;
        public SifreValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.sifre)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .NotNull().WithMessage("Şifre boş olamaz.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
                .MaximumLength(60).WithMessage("Şifre en fazla 60 karakter olabilir.")
                .Matches(@"[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
                .Matches(@"[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches(@"[\W_]").WithMessage("Şifre en az bir noktalama işareti içermelidir.");

            RuleFor(x => x.SifreOlusturmaTarihi)
                .NotEmpty().WithMessage("Şifre oluşturma tarihi boş olamaz.")
                .NotNull().WithMessage("Şifre oluşturma tarihi boş olamaz.")
                .Must(x => x is DateTime).WithMessage("Geçerli bir tarih giriniz.");

            RuleFor(x => x.SifreGecerlilikTarihi)
                .NotEmpty().WithMessage("Şifre geçerlilik tarihi boş olamaz.")
                .NotNull().WithMessage("Şifre geçerlilik tarihi boş olamaz.")
                .Must(x => x is DateTime).WithMessage("Geçerli bir tarih giriniz.")
                .Must((x, date) => date >= x.SifreOlusturmaTarihi).WithMessage("Şifre geçerlilik tarihi şifre oluşturma tarihinden büyük olmalı.")
;
        }


    }
}
