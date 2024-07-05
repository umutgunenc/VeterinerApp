using FluentValidation;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators
{
    public class TurSilValidator : AbstractValidator<TurViewModel>
    {   private readonly VeterinerContext _contex;

        public TurSilValidator(VeterinerContext context)
        {
            _contex = context;

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Lütfen bir tür seçiniz.")
                .NotEmpty().WithMessage("Lütfen bir tür seçiniz.")
                .Must(beTur).WithMessage("Listede olmayan bir türü silemezsiniz.")
                .Must(beCins).WithMessage("Silinecek türe ait tanımlı cins olduğu için silme işlemi gerçekleştirilemedi.");
        }
        private bool beTur(int id)
        {
            return _contex.Turs.Any(x=>x.Id == id);
        }

        private bool beCins(int id)
        {
            return !_contex.TurCins.Any(x=>x.Id == id);
        }
    }
}