using FluentValidation;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class TurCinsEkleValidators : AbstractValidator<TurCinsEkleViewModel>
    {
        private readonly VeterinerContext _context;
        public TurCinsEkleValidators(VeterinerContext context)
        {
            _context = context;


            RuleFor(x => x.CinsId)
                .NotNull().WithMessage("Cins boş olamaz.")
                .NotEmpty().WithMessage("Cins boş olamaz.")
                .Must(BeCins).WithMessage("Seçilen cins sistemde tanımlı olmalı.");

            RuleFor(x => x.TurId)
                .NotNull().WithMessage("Tür boş olamaz.")
                .NotEmpty().WithMessage("Tür boş olamaz.")
                .Must(BeTur).WithMessage("Seçilen tür sistemde tanımlı olmalı.")
                .Must(notMatch).WithMessage("Seçilen tür daha önceden eşleştirilmiş.");

        }

        private bool BeCins(int CinsId)
        {
            return _context.Cins.Any(x => x.Id == CinsId);
        }
        private bool notMatch(int turId)
        {
            return !_context.TurCins.Where(x => x.TurId == turId).Any();
        }
        private bool BeTur(int TurId)
        {
            return _context.Turs.Any(x => x.Id == TurId);
        }
    }
}
