using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class TurCinsSilValidators : AbstractValidator<TurCins>
    {
        private readonly VeterinerContext _context;
        public TurCinsSilValidators(VeterinerContext context)
        {
            _context = context;

                      

                RuleFor(x => x.Id)
                    .NotNull().WithMessage("ID boş olamaz.")
                    .NotEmpty().WithMessage("ID boş olamaz.");

                RuleFor(x => x)
                    .NotEmpty().WithMessage("Bir kayıt seçiniz.")
                    .NotNull().WithMessage("Bir kayıt seçiniz.")
                    .Must(BeUniqueTurCins).WithMessage("Seçilen kayıt sistemde bulunamadı.");

            
        }

        private bool BeUniqueTurCins(TurCins model)
        {
            var cinsId = model.CinsId;
            var turId = model.TurId;

            return _context.TurCins.Any(x => x.CinsId == cinsId && x.TurId == turId);
        }
    }
}
