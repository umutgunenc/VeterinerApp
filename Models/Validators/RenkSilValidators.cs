using FluentValidation;
using System;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators
{
    public class RenkSilValidator : AbstractValidator<RenkViewModel>
    {
        private readonly VeterinerContext _contex;

        public RenkSilValidator(VeterinerContext context)
        {
            _contex = context;

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Lütfen bir renk seçiniz.")
                .NotEmpty().WithMessage("Lütfen bir renk seçiniz.")
                .Must(beRenk).WithMessage("Listede olmayan bir rengi silemezsiniz.");

        }
        private bool beRenk(int Id)
        {
            return _contex.Renks.Any(x => x.Id == Id);
        }

    }
}