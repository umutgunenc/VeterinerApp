using FluentValidation;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators.Admin
{
    public partial class RolSilValidators : AbstractValidator<RolSilViewModel>
    {

        public RolSilValidators()
        {

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Silinecek rolü seçiniz.")
                .NotNull().WithMessage("Silinecek rolü seçiniz.")
                .Must(FunctionsValidator.beRol).WithMessage("Silinecek rol sistemde bulunamadı.")
                .Must(FunctionsValidator.notBeInsan).WithMessage("Silinecek role tanımlı kişiler olduğu için silme işlemi gerçekleştirilemedi.");
        }


    }
}
