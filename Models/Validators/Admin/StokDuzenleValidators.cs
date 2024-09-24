using FluentValidation;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators.Admin
{
    public class StokDuzenleValidators : AbstractValidator<StokDuzenleViewModel>
    {
        public StokDuzenleValidators()
        {
            RuleFor(x => x.StokBarkod)
                .NotEmpty().WithMessage("Barkod numarası boş olamaz.")
                .NotNull().WithMessage("Barkod numarası boş olamaz.")
                .Must(FunctionsValidator.BeInStock).WithMessage("Girilen barkod numarası sistemde bulunamadı");
        }
    }
}
