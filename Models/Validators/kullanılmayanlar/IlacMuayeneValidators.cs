#nullable disable

using FluentValidation;
using System.Linq;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;



namespace VeterinerApp.Models.Validators.kullanılmayanlar
{
    public partial class IlacMuayeneValidators : AbstractValidator<IlacMuayene>
    {
        private readonly VeterinerContext _context;
        public IlacMuayeneValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.IlacIlacBarkod)
                .NotNull().WithMessage("Lütfen ilaç seçiniz.")
                .NotEmpty().WithMessage("Lütfen ilaç seçiniz.")
                .MaximumLength(100).WithMessage("Barkod numarası maksimum 100 karakter uzunluğunda olmalı.")
                .Must(MustBeBarkod).WithMessage("Seçtiğiniz ilaç sistemde bulunmamaktadır.");

            RuleFor(x => x.MuayeneId)
                .NotNull().WithMessage("Lütfen ilgili muayene numarasını seçiniz.")
                .NotEmpty().WithMessage("Lütfen ilgili muayene numarasını seçiniz.")
                .Must(MustBeMuayene).WithMessage("Seçtiğiniz ilaç sistemde bulunmamaktadır.")
                .Must(x => x is int).WithMessage("Lütfen muayeneye ait bir numara giriniz.");

        }

        private bool MustBeBarkod(string Barkod)
        {
            return _context.Ilacs.Any(x => x.IlacBarkod.ToUpper() == Barkod.ToUpper());
        }

        private bool MustBeMuayene(int Muayene)
        {
            return _context.Muayenes.Any(x => x.MuayeneId == Muayene);
        }
    }
}
