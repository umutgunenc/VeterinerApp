using FluentValidation;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class TurCinsEkleValidators : AbstractValidator<TurCinsViewModel>
    {
        private readonly VeterinerContext _context;
        public TurCinsEkleValidators(VeterinerContext context)
        {
            _context = context;


            RuleFor(x => x.CinsId)
                .NotNull().WithMessage("Cins boş olamaz.")
                .NotEmpty().WithMessage("Cins boş olamaz.")
                .Must(BeCins).WithMessage("Seçilen cins sistemde tanımlı olmalı.")
                .Must((model, cinsId) => TurCinsAlreadyExists(model.TurId, cinsId)).WithMessage("Seçilen cins ve tür daha önceden eşleştirilmiş.")
                .Must(notMatch).WithMessage("Seçilen cins daha önceden eşleştirilmiş.");


            RuleFor(x => x.TurId)
                .NotNull().WithMessage("Tür boş olamaz.")
                .NotEmpty().WithMessage("Tür boş olamaz.")
                .Must(BeTur).WithMessage("Seçilen tür sistemde tanımlı olmalı.");

        }

        private bool BeCins(int CinsId)
        {
            return _context.Cins.Any(x => x.Id == CinsId);
        }
        private bool notMatch(int CinsId)
        {
            return !_context.TurCins.Where(x => x.CinsId == CinsId).Any();
        }

        private bool BeTur(int TurId)
        {
            return _context.Turs.Any(x => x.Id == TurId);
        }

        private bool TurCinsAlreadyExists(int turId, int cinsId)
        {
            return !_context.TurCins.Any(x => x.TurId == turId && x.CinsId == cinsId);
        }
    }
}
