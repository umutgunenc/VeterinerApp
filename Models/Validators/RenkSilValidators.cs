using FluentValidation;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators
{
    public class RenkSilValidator : AbstractValidator<RenkSilViewModel>
    {
        private readonly VeterinerContext _contex;

        public RenkSilValidator(VeterinerContext context)
        {
            _contex = context;

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Lütfen bir renk seçiniz.")
                .NotEmpty().WithMessage("Lütfen bir renk seçiniz.")
                .Must(beRenk).WithMessage("Listede olmayan bir rengi silemezsiniz.")
                .Must(notUsed).WithMessage("Silinmek istenen renk bir hayvana atandığı için silme işlemi başarısız oldu.");

        }
        private bool beRenk(int Id)
        {
            return _contex.Renks.Any(x => x.Id == Id);
        }

        private bool notUsed(int Id)
        {
            return !_contex.Hayvans.Any(x => x.RenkId == Id);
        }

    }
}