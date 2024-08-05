using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class TurCinsSilValidators : AbstractValidator<TurCinsSilViewModel>
    {
        private readonly VeterinerContext _context;
        public TurCinsSilValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Bir kayıt seçiniz.")
                .NotEmpty().WithMessage("Bir kayıt seçiniz.")
                .Must(beTurCins).WithMessage("Seçilen Kayıt sisteme kayıtlı olmalı.")
                .Must(notUsed).WithMessage("Seçilen tür ve cinse ait bir hayvan kaydı olduğu için silme işlemi başarısız oldu");         
        }

        private bool beTurCins(int id)
        {
            return _context.TurCins.Any(x => x.Id == id);
        }

        private bool notUsed(int id)
        {
            var cinsId = _context.TurCins.Where(x => x.Id == id).Select(x => x.CinsId).FirstOrDefault();
            var turId = _context.TurCins.Where(x => x.Id == id).Select(x => x.TurId).FirstOrDefault();
            return !_context.Hayvans.Where(x => x.CinsId == cinsId && x.TurId == turId).Any();
        }
    }
}
