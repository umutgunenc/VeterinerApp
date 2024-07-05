using FluentValidation;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.Validators
{
    public class TurSilValidator : AbstractValidator<Tur>
    {   private readonly VeterinerContext _contex;

        public TurSilValidator(VeterinerContext context)
        {
            _contex = context;

            RuleFor(x => x.tur)
                .NotNull().WithMessage("Lütfen bir tür seçiniz.")
                .NotEmpty().WithMessage("Lütfen bir tür seçiniz.")
                .Must(beTur).WithMessage("Listede olmayan bir türü silemezsiniz.");
        }
        private bool beTur(string tur)
        {
            return _contex.Turs.Any(x=>x.tur == tur.ToUpper());
        }
    }
}