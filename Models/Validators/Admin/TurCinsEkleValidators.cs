using FluentValidation;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators.Admin
{
    public partial class TurCinsEkleValidators : AbstractValidator<TurCinsEkleViewModel>
    {
        public TurCinsEkleValidators()
        {


            RuleFor(x => x.CinsId)
                .NotNull().WithMessage("Cins boş olamaz.")
                .NotEmpty().WithMessage("Cins boş olamaz.")
                .Must(FunctionsValidator.beCins).WithMessage("Seçilen cins sistemde tanımlı olmalı.");

            RuleFor(x => x.TurId)
                .NotNull().WithMessage("Tür boş olamaz.")
                .NotEmpty().WithMessage("Tür boş olamaz.")
                .Must(FunctionsValidator.beTur).WithMessage("Seçilen tür sistemde tanımlı olmalı.")
                .Must(FunctionsValidator.notMatch).WithMessage("Seçilen tür daha önceden eşleştirilmiş.");

        }


    }
}
