using FluentValidation;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.Validators
{
    public class CinsSilValidator : AbstractValidator<Cins>
    {   private readonly VeterinerContext _contex;

        public CinsSilValidator(VeterinerContext context)
        {
            _contex = context;

            RuleFor(x => x.cins)
                .NotNull().WithMessage("Lütfen bir cins seçiniz.")
                .NotEmpty().WithMessage("Lütfen bir cins seçiniz.")
                .Must(beCins).WithMessage("Listede olmayan bir cinsi silemezsiniz.");
        }
        private bool beCins(string cins)
        {
            return _contex.Cins.Any(x=>x.cins == cins.ToUpper());
        }
    }
}