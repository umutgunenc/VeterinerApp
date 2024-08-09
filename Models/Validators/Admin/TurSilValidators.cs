using FluentValidation;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators.Admin
{
    public class TurSilValidator : AbstractValidator<TurSilViewModel>
    {
        public TurSilValidator()
        {

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Lütfen bir tür seçiniz.")
                .NotEmpty().WithMessage("Lütfen bir tür seçiniz.")
                .Must(FunctionsValidator.BeTur).WithMessage("Listede olmayan bir türü silemezsiniz.")
                .Must(FunctionsValidator.BeNotMatchedTur).WithMessage("Silinecek türe ait tanımlı cins olduğu için silme işlemi gerçekleştirilemedi.");
        }

    }
}