using FluentValidation;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.ValidateFunctions;

namespace VeterinerApp.Models.Validators.Admin
{
    public class KategoriEkleValidators : AbstractValidator<Kategori>
    {
        public KategoriEkleValidators()
        {
            RuleFor(x => x.KategoriAdi)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .NotNull().WithMessage("Kategori adı boş olamaz.")
                .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter uzunluğunda olabilir.")
                .Must(FunctionsValidator.BeUniqueKategori).WithMessage("Bu kategori daha önceden sisteme eklenmiş.");
        }
    }
}
