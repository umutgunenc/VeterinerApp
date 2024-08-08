using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators.Admin
{
    public class CinsSilValidator : AbstractValidator<CinsSilViewModel>
    {

        public CinsSilValidator()
        {

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Lütfen bir cins seçiniz.")
                .NotEmpty().WithMessage("Lütfen bir cins seçiniz.")
                .Must(FunctionsValidator.beCins).WithMessage("Listede olmayan bir cinsi silemezsiniz.")
                .Must(FunctionsValidator.notBeTur).WithMessage("Silinecek cinse ait tanımlı tür olduğu için silme işlemi gerçekleştirilemedi.");
        }

    }
}