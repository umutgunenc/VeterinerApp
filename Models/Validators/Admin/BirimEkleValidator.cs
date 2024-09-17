using FluentValidation;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators.Admin
{
    public class BirimEkleValidator : AbstractValidator<BirimEkleViewModel>
    {
        public BirimEkleValidator()
        {
            RuleFor(x=>x.BirimAdi)
                .NotNull().WithMessage("Birim adı boş olamaz.")
                .NotEmpty().WithMessage("Birim adı boş olamaz.")
                .MinimumLength(2).WithMessage("Birim adı en az 2 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Birim adı en fazla 50 karakter olmalıdır.")
                .Must(FunctionsValidator.BeUniqueBirim).WithMessage("Girilen kayıt sistemde mevcut.");
        }
    }
}
