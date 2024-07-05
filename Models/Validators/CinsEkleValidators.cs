using FluentValidation;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class CinsEkleValidators : AbstractValidator<CinsEkleViewModel>
    {
        private readonly VeterinerContext _veterinerContext;
        public CinsEkleValidators(VeterinerContext veterinerContext)
        {
            _veterinerContext = veterinerContext;
            RuleFor(x => x.cins)
                .NotNull().WithMessage("Lütfen bir cins giriniz.")
                .NotEmpty().WithMessage("Lütfen bir cins giriniz.")
                .MaximumLength(50).WithMessage("Maksimum 50 karakter kullanabilirsiniz.")
                .Must(BeUnique).WithMessage("Girdiğiniz cins zaten sisteme kayıtlı");
        }
        private bool BeUnique(string girilenDeger)
        {
            return !_veterinerContext.Cins.Any(x => x.cins.ToUpper() == girilenDeger.ToUpper());
        }
    }
}
