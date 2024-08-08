using FluentValidation;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators.Admin
{
    public class RenkSilValidator : AbstractValidator<RenkSilViewModel>
    {

        public RenkSilValidator()
        {

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Lütfen bir renk seçiniz.")
                .NotEmpty().WithMessage("Lütfen bir renk seçiniz.")
                .Must(FunctionsValidator.beRenk).WithMessage("Listede olmayan bir rengi silemezsiniz.")
                .Must(FunctionsValidator.beNotUsedRenk).WithMessage("Silinmek istenen renk bir hayvana atandığı için silme işlemi başarısız oldu.");

        }


    }
}