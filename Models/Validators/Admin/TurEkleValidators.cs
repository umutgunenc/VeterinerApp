using FluentValidation;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators.Admin
{
    public partial class TurEkleValidators : AbstractValidator<TurEKleViewModel>
    {

        public TurEkleValidators()
        {

            RuleFor(x => x.tur)
                .NotEmpty().WithMessage("Lütfen bir tür giriniz")
                .NotNull().WithMessage("Lütfen bir tür giriniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter uzunluğunda değer girilebilir.")
                .Must(FunctionsValidator.beUniqueTur).WithMessage("Girdiğiniz tür zaten sistemde kayıtlı.");
        }


    }
}
