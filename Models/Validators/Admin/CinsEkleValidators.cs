using FluentValidation;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators.Admin
{
    public partial class CinsEkleValidators : AbstractValidator<CinsEkleViewModel>
    {
        public CinsEkleValidators()
        {
            RuleFor(x => x.cins)
                .NotNull().WithMessage("Lütfen bir cins giriniz.")
                .NotEmpty().WithMessage("Lütfen bir cins giriniz.")
                .MaximumLength(50).WithMessage("Maksimum 50 karakter kullanabilirsiniz.")
                .Must(FunctionsValidator.BeUniqueCins).WithMessage("Girdiğiniz cins zaten sisteme kayıtlı");
        }
    }
}
