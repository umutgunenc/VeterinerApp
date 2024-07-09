
using VeterinerApp.Data;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators
{
    public class RenkEkleValidators : AbstractValidator<RenkEkleViewModel>
    {
        private readonly VeterinerContext _dbContext;

        public RenkEkleValidators(VeterinerContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.renk)
                .NotNull().WithMessage("Lütfen bir renk giriniz.")
                .NotEmpty().WithMessage("Lütfen bir renk giriniz.")
                .MaximumLength(50).WithMessage("En fazla 50 karakter uzunluğunda değer girilebilir.")
                .Must(BeUnique).WithMessage("Girdiğiniz renk zaten sisteme kayıtlı.");
        }

        private bool BeUnique(string girilenDeger)
        {
            return !_dbContext.Renks.Any(x => x.renk.ToUpper() == girilenDeger.ToUpper());
        }
    }
}

