using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators.Admin
{
    public partial class TurCinsSilValidators : AbstractValidator<TurCinsSilViewModel>
    {
        public TurCinsSilValidators()
        {

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Bir kayıt seçiniz.")
                .NotEmpty().WithMessage("Bir kayıt seçiniz.")
                .Must(FunctionsValidator.BeTurCins).WithMessage("Seçilen Kayıt sisteme kayıtlı olmalı.")
                .Must(FunctionsValidator.BeNotUsedTurCins).WithMessage("Seçilen tür ve cinse ait bir hayvan kaydı olduğu için silme işlemi başarısız oldu");
        }


    }
}
