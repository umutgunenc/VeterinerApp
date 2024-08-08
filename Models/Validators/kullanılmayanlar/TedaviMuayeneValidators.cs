#nullable disable

using FluentValidation;
using System.Linq;
using System.Runtime.InteropServices;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.Validators.kullanılmayanlar
{
    public partial class TedaviMuayeneValidators : AbstractValidator<TedaviMuayene>
    {
        private readonly VeterinerContext _context;

        public TedaviMuayeneValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.TedaviTedaviId)
                .NotNull().WithMessage("Tedavi numarası boş bırakılamaz.")
                .NotEmpty().WithMessage("Tedavi numarası boş bırakılamaz.")
                .Must(x => x is int).WithMessage("Geçerli bir tedavi numarası giriniz.")
                .Must(beTedavi).WithMessage("Seçilen Tedavi sisteme kayıtlı olmalı.");

            RuleFor(x => x.MuayeneId)
                .NotNull().WithMessage("Muayene numarası boş bırakılamaz.")
                .NotEmpty().WithMessage("Muayene numarası boş bırakılamaz.")
                .Must(x => x is int).WithMessage("Geçerli bir muayene numarası giriniz.")
                .Must(beMuayene).WithMessage("Seçilen muayane sisteme kayıtlı olmalı.");
        }

        private bool beTedavi(int tedaviId)
        {
            return _context.Tedavis.Any(x => x.TedaviId == tedaviId);
        }

        private bool beMuayene(int muayeneId)
        {
            return _context.Muayenes.Any(x => x.MuayeneId == muayeneId);
        }
    }
}
