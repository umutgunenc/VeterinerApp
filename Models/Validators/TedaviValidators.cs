using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class TedaviValidators : AbstractValidator<Tedavi>
    {
        private readonly VeterinerContext _context;
        public TedaviValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.TedaviId)
                .NotEmpty().WithMessage("Tedavi ID boş olamaz.")
                .NotNull().WithMessage("Tedavi ID boş olamaz")
                .Must(x => x is int).WithMessage("Lütfen geçerli bir ID giriniz.");

            RuleFor(x => x.TedaviAdi)
                .NotNull().WithMessage("Tedavi için bir isim giriniz.")
                .NotEmpty().WithMessage("Tedavi için bir isim giriniz.")
                .MaximumLength(100).WithMessage("Tedavi adı maksimum 100 karakter uzunluğunda olmalıdır.")
                .Must(beUnique).WithMessage("Girilen tedavi zaten sistemde bulunmaktadır.");

            RuleFor(x => x.TedaviUcreti)
                .NotNull().WithMessage("Tedavi ücretini giriniz.")
                .NotEmpty().WithMessage("Tedavi ücretini giriniz")
                .Must(x => x is double).WithMessage("Geçerli bir ücret giriniz.")
                .Must(x => x is double && x >= 0).WithMessage("Geçerli bir ücret giriniz.");

        }

        private bool beUnique(string tedaviAdi)
        {
            return _context.Tedavis.Any(x=>x.TedaviAdi.ToUpper()==tedaviAdi.ToUpper());
        }
    }
}
