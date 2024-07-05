using FluentValidation;
using FluentValidation.AspNetCore;
using System.Collections.Generic;
using System.Linq;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;


#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class IlacValidators : AbstractValidator<Ilac>
    {
        private readonly VeterinerContext _context;
        public IlacValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.IlacAdi)
                .NotNull().WithMessage("Lütfen bir ilaç adı giriniz.")
                .NotEmpty().WithMessage("Lütfen bir ilaç adı giriniz.")
                .Must(BeUniqueIlac).WithMessage("Sistemde bu isimde bir ilaç zaten bulunmakta")
                .MaximumLength(100).WithMessage("İlaç adı maksimum 100 karakter uzunluğunda olmalı");

            RuleFor(x => x.IlacBarkod)
                .NotNull().WithMessage("Lütfen ilaca ait barkod numarasını seçiniz")
                .NotEmpty().WithMessage("Lütfen ilaca ait barkod numarasını seçiniz")
                .MaximumLength(100).WithMessage("Barkod numarası makismum 100 karakter uzunluğunda olmalı")
                .Must(BeUniqueBarkod).WithMessage("Sistemde bu barkod numarasına ait bir ilaç zaten bulunmakta")
                .Must(MustBeBarkod).WithMessage("Barkod numarası sistemde kayıtlı değil.");


        }

        private bool BeUniqueIlac(string ilacAdi)
        {
            return !_context.Ilacs.Any(x => x.IlacAdi.ToUpper() == ilacAdi.ToUpper());
        }

        private bool BeUniqueBarkod(string ilacBarkod)
        {
            return !_context.Ilacs.Any(x => x.IlacBarkod.ToUpper() == ilacBarkod.ToUpper());
        }

        private bool MustBeBarkod(string ilacBarkod)
        {
            return _context.Stoks.Any(x => x.StokBarkod.ToUpper() == ilacBarkod.ToUpper());
        }
    }
}
