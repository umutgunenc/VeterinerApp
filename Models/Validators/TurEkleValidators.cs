using FluentValidation;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class TurEkleValidators :AbstractValidator<TurEKleViewModel>
    {
        private readonly VeterinerContext _context;

        public TurEkleValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.tur)
                .NotEmpty().WithMessage("Lütfen bir tür giriniz")
                .NotNull().WithMessage("Lütfen bir tür giriniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter uzunluğunda değer girilebilir.")
                .Must(BeUnique).WithMessage("Girdiğiniz tür zaten sistemde kayıtlı.");
        }

        private bool BeUnique(string girilenTur)
        {
            return !_context.Turs.Any(t => t.tur.ToUpper() == girilenTur.ToUpper());
        }
    }
}
