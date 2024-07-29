using FluentValidation;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class RolValidators : AbstractValidator<RolEkleViewModel>
    {
        private readonly VeterinerContext _context;
        public RolValidators(VeterinerContext context)
        {
            _context = context;

            //RuleFor(x => x.RolAdi)
            //    .NotEmpty().WithMessage("Lütfen rol tanımı yapınız.")
            //    .NotNull().WithMessage("Lütfen rol tanımı yapınız.")
            //    .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda rol tanımlaması yapılabilir.")
            //    .Must(beUnique).WithMessage("Girilen rol daha önceden sisteme tanımlanmış.")
            //    .Must(roller).WithMessage("Sadece ADMİN, VETERİNER, ÇALIŞAN ve MÜŞTERİ tanımlanabilir.");
        }

        private bool roller(string roller)
        {
            roller=roller.ToUpper();
            if (roller == "admin".ToUpper() || roller == "veteriner".ToUpper() || roller == "çalışan".ToUpper() || roller == "müşteri".ToUpper())
                return true;
            return false;
        }
        //private bool beUnique(string rol)
        //{
        //    return !_context.Rols.Any(x => x.RolAdi.ToUpper() == rol.ToUpper());
        //}
    }
}
