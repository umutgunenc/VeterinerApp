using FluentValidation;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators
{
    public partial class RolSilValidators : AbstractValidator<RolSilViewModel>
    {
        private readonly VeterinerContext _context;

        public RolSilValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Silinecek rolü seçiniz.")
                .NotNull().WithMessage("Silinecek rolü seçiniz.")
                .Must(beRol).WithMessage("Silinecek rol sistemde bulunamadı.")
                .Must(notBeInsan).WithMessage("Silinecek role tanımlı kişiler olduğu için silme işlemi gerçekleştirilemedi.");
        }

        private bool beRol(string rolId)
        {
            return _context.Roles.Any(x => x.Id == rolId);
        }

        private bool notBeInsan(string rolId)
        {
            return !_context.Users.Any(x => x.Id == rolId);
        }
    }
}
