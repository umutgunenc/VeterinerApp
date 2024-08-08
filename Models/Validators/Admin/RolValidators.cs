using FluentValidation;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators.Admin
{
    public partial class RolValidators : AbstractValidator<RolEkleViewModel>
    {
        public RolValidators()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Lütfen rol tanımı yapınız.")
                .NotNull().WithMessage("Lütfen rol tanımı yapınız.")
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda rol tanımlaması yapılabilir.")
                .Must(FunctionsValidator.beUniqueRol).WithMessage("Girilen rol daha önceden sisteme tanımlanmış.")
                .Must(FunctionsValidator.roller).WithMessage("Sadece ADMİN, VETERİNER, ÇALIŞAN ve MÜŞTERİ tanımlanabilir.");
        }


    }
}
