#nullable disable

using FluentValidation;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.Validators.kullanılmayanlar
{
    public partial class MuayeneGelirleriValidators : AbstractValidator<MuayeneGelirleri>
    {
        private readonly VeterinerContext _context;
        public MuayeneGelirleriValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.MuayeneNo)
                .NotEmpty().WithMessage("Muayane numarası boş olamaz.")
                .NotNull().WithMessage("Muayane numarası boş olamaz.")
                .Must(x => x is int).WithMessage("Geçerli bir muayene numaası giriniz.")
                .Must(BeMuayene).WithMessage("Girilen muayene numarası sistemde bulunamadı.");

            RuleFor(x => x.Gelir)
                .NotEmpty().WithMessage("Muayane ücretini giriniz.")
                .NotNull().WithMessage("Muayane ücretini giriniz.")
                .Must(x => x is float).WithMessage("Geçerli bir muayene ücreti giriniz.");
        }

        private bool BeMuayene(int muayeneNo)
        {
            return _context.Muayenes.Any(x => x.MuayeneNo == muayeneNo);
        }
    }
}
