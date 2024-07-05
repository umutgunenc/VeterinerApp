using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class TurCinsSilValidators : AbstractValidator<TurCinsViewModel>
    {
        private readonly VeterinerContext _context;
        public TurCinsSilValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Bir kayıt seçiniz.")
                .NotEmpty().WithMessage("Bir kayıt seçiniz.")
                .Must(beTurCins).WithMessage("Seçilen Kayıt sisteme kayıtlı olmalı.");         
        }

        private bool beTurCins(int id)
        {
            return _context.TurCins.Any(x => x.Id == id);
        }
    }
}
