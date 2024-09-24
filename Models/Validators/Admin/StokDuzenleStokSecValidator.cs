using FluentValidation;
using System;
using System.Linq;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators.Admin
{
    public class StokDuzenleStokSecValidator : AbstractValidator<StokDuzenleStokSecViewModel>
    {
        public StokDuzenleStokSecValidator()
        {
            RuleFor(x => x.GirilenBarkodNo)
                .NotEmpty().WithMessage("Barkod numarası boş olamaz.")
                .NotNull().WithMessage("Barkod numarası boş olamaz.")
                .MaximumLength(50).WithMessage("Barkod Numarası maksimum 50 karakter uzunluğunda olabilir.")
                .Must(FunctionsValidator.BeInStock).WithMessage("Girilen barkod numarası sistemde bulunamadı.");
        }
    }
}
