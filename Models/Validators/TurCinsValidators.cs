using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class TurCinsValidators : AbstractValidator<TurCins>
    {
        private readonly VeterinerContext _context;
        public TurCinsValidators(VeterinerContext context)
        {
            _context = context;


            RuleFor(x => x.CinsId)
                .NotNull().WithMessage("Cins boş olamaz.")
                .NotEmpty().WithMessage("Cins boş olamaz.")
                .Must(BeCins).WithMessage("Seçilen cins sistemde tanımlı olmalı.");

            RuleFor(x => x.TurId)
                .NotNull().WithMessage("Tür boş olamaz.")
                .NotEmpty().WithMessage("Tür boş olamaz.")
                .Must(BeTur).WithMessage("Seçilen tür sistemde tanımlı olmalı.");

            RuleFor(x => x)
            .Must(BeUniqueTurCins).WithMessage("Bu cins ve tür eşleşmesi zaten mevcut.");
        }

        private bool BeCins(int CinsId)
        {
            return _context.Cins.Any(x => x.Id == CinsId);
        }

        private bool BeTur(int TurId)
        {
            return _context.Turs.Any(x => x.Id == TurId);
        }

        private bool BeUniqueTurCins(TurCins model)
        {
            var cinsId = model.CinsId;
            var turId = model.TurId;

            return !_context.TurCins.Any(x => x.CinsId == cinsId && x.TurId == turId);
        }
    }
}
