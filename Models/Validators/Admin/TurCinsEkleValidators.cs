using FluentValidation;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators.Admin
{
    public partial class TurCinsEkleValidators : AbstractValidator<CinsTurEslestirViewModel>
    {
        public TurCinsEkleValidators()
        {


            RuleFor(x => x.CinsId)
                .NotNull().WithMessage("Cins boş olamaz.")
                .NotEmpty().WithMessage("Cins boş olamaz.")
                .Must(FunctionsValidator.BeCins).WithMessage("Seçilen cins sistemde tanımlı olmalı.")
                .Must(FunctionsValidator.BeNotMatchTurCins).WithMessage("Seçilen cins daha önceden eşleştirilmiş.");

            RuleFor(x => x.TurId)
                .NotNull().WithMessage("Tür boş olamaz.")
                .NotEmpty().WithMessage("Tür boş olamaz.")
                .Must(FunctionsValidator.BeTur).WithMessage("Seçilen tür sistemde tanımlı olmalı.");

        }


    }
}
