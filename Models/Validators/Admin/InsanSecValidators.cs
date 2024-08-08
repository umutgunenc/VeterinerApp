using FluentValidation;
using System.Collections.Generic;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using System;
using System.Linq;
using VeterinerApp.Models.ViewModel.Admin;
using VeterinerApp.Models.Validators.ValidateFunctions;

#nullable disable

namespace VeterinerApp.Models.Validators.Admin
{
    public partial class InsanSecValidators : AbstractValidator<InsanSecViewModel>
    {
        public InsanSecValidators()
        {

            RuleFor(x => x.InsanTckn)
                .NotEmpty().WithMessage("Lütfen TCKN giriniz.")
                .NotNull().WithMessage("Lütfen TCKN giriniz.")
                .Length(11).WithMessage("TCKN 11 karakter uzunluğunda olmalıdır.")
                .Matches("^[0-9]*$").WithMessage("TCKN numarası sadece rakamlardan oluşmalıdır.")
                .Must(FunctionsValidator.notUniqueTCKN).WithMessage("Girilen TCKN sistemde bulunamadı.")
                .Must(FunctionsValidator.TcDogrula).WithMessage("Geçerli bir TCKN giriniz.");
        }



    }
}
