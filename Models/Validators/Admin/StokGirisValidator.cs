using FluentValidation;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators.Admin
{
    public class StokGirisValidator :AbstractValidator<StokGirisViewModel>
    {
        public StokGirisValidator()
        {
            RuleFor(x=>x.ArananMetin)
                .NotEmpty().WithMessage("Aranan kelime boş olamaz")
                .NotNull().WithMessage("Aranan kelime boş olamaz")
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda bir arama yapılabilir.")
                .Must(FunctionsValidator.SeacrhInStock).WithMessage("Aranan stok bulunamadı.");
        }
    }
}
