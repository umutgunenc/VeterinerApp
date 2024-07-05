using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class StokValidators :AbstractValidator<Stok>
    {

        private readonly VeterinerContext _context;
        public StokValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.StokBarkod)
                .NotNull().WithMessage("Barkod numarası girilmeli")
                .NotEmpty().WithMessage("Barkod numarası girilmeli")
                .MaximumLength(100).WithMessage("Barkod numarası maksimum 100 karakter uzunluğunda olabilir")
                .Must(BeUnique).WithMessage("Girilen barkod numarası sisteme kayıtlı.");

            RuleFor(x => x.StokAdi)
                .NotEmpty().WithMessage("Girilen stok için bir isim giriniz.")
                .NotEmpty().WithMessage("Girilen stok için bir isim giriniz.")
                .MaximumLength(100).WithMessage("Stok ismi maksimum 100 karakter uzunluğunda olmalıdır.")
                .Must(BeUniqueName).WithMessage("Girilen stok adı sisteme kayıtlı.");

            RuleFor(x => x.StokSayisi)
                .NotEmpty().WithMessage("Boş bırakılamaz.")
                .NotNull().WithMessage("Boş bırakılamaz.")
                .Must(x => x is int).WithMessage("Lütfen geçerli bir sayı giriniz.")
                .Must(x => x is int && x >= 0).WithMessage("Sıfırdan büyük bir sayı giriniz");            

        }

        private bool BeUnique(string barkod)
        {
            return !_context.Stoks.Any(x=>x.StokBarkod.ToUpper()==barkod.ToUpper());
        }
        private bool BeUniqueName(string name)
        {
            return !_context.Stoks.Any(x=>x.StokAdi.ToUpper()==name.ToUpper());
        }
    }
}
