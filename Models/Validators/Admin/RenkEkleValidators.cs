
using VeterinerApp.Data;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.ViewModel.Admin;
using VeterinerApp.Models.Validators.ValidateFunctions;

namespace VeterinerApp.Models.Validators.Admin
{
    public class RenkEkleValidators : AbstractValidator<RenkEkleViewModel>
    {

        public RenkEkleValidators()
        {

            RuleFor(x => x.renk)
                .NotNull().WithMessage("Lütfen bir renk giriniz.")
                .NotEmpty().WithMessage("Lütfen bir renk giriniz.")
                .MaximumLength(50).WithMessage("En fazla 50 karakter uzunluğunda değer girilebilir.")
                .Must(FunctionsValidator.BeUniqueRenk).WithMessage("Girdiğiniz renk zaten sisteme kayıtlı.");
        }


    }
}

