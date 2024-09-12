using FluentValidation;
using System.Collections.Generic;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using System;
using System.Linq;
using VeterinerApp.Models.ViewModel.Admin;
using Microsoft.AspNetCore.Identity;
using VeterinerApp.Models.Validators.ValidateFunctions;

#nullable disable

namespace VeterinerApp.Models.Validators.Admin
{
    public partial class KisiDuzenleValidators : AbstractValidator<KisiDuzenleViewModel>
    {
        public KisiDuzenleValidators()
        {
            RuleFor(x => x.InsanTckn)
                .NotEmpty().WithMessage("Lütfen TCKN giriniz.")
                .NotNull().WithMessage("Lütfen TCKN giriniz.")
                .Length(11).WithMessage("TCKN 11 karakter uzunluğunda olmalıdır.")
                .Matches("^[0-9]*$").WithMessage("TCKN numarası sadece rakamlardan oluşmalıdır.")
                .Must(FunctionsValidator.BeValidTCKN).WithMessage("Geçerli bir TCKN giriniz.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(11).WithMessage("Telefon numarası maksimum 11 karakter olabilir.")
                .NotEmpty().WithMessage("Lütfen telefon numarasını giriniz.")
                .NotNull().WithMessage("Lütfen telefon numarasını giriniz.")
                .Matches(@"^0\d{10}$").WithMessage("Telefon numarası geçersiz.")
                .Must((model, insanTel) => FunctionsValidator.BeUniqueTel(model.Id, insanTel))
                .WithMessage("Girilen telefon numarası zaten sisteme kayıtlı.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.")
                .NotNull().WithMessage("Lütfen e-mail adresi giriniz.")
                .NotEmpty().WithMessage("Lütfen e-mail adresi giriniz.")
                .MaximumLength(100).WithMessage("e-mail adresi maksimum 100 karakter uzunluğunda olabilir.")
                .Must((model, insanMail) => FunctionsValidator.BeUniqueEmail(model.Id, insanMail))
                .WithMessage("Girilen e-posta adresi zaten sisteme kayıtlı.");

            RuleFor(x => x.DiplomaNo)
                .MaximumLength(11).WithMessage("Diploma numarası en fazla 11 karakter olabilir.")
                .Must((model, diplomaNo) => FunctionsValidator.BeUniqueOrNullDiplomaNo(model.Id, diplomaNo))
                .WithMessage("Girilen diploma numarası zaten sistemde kayıtlı.");

            RuleFor(x => x.DiplomaNo)
                .NotNull().WithMessage("Diploma numarası giriniz.")
                .When(x => FunctionsValidator.IsRoleMatching(x.RolId, new List<string> { "veteriner" }));

            RuleFor(x => x.InsanAdi)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda isim girilebilir.")
                .NotEmpty().WithMessage("Çalışanın ismini giriniz.")
                .NotNull().WithMessage("Çalışanın ismini giriniz.");

            RuleFor(x => x.InsanSoyadi)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda soyisim girilebilir.")
                .NotEmpty().WithMessage("Çalışanın soyismini giriniz.")
                .NotNull().WithMessage("Çalışanın soyismini giriniz.");

            RuleFor(x => x.UserName)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda kullanıcı adı girilebilir")
                .NotEmpty().WithMessage("Çalışanın kullanıcı adını giriniz.")
                .NotNull().WithMessage("Çalışanın kullanıcı adını giriniz.")
                .Must((model, kullaniciAdi) => FunctionsValidator.BeUniqueKullaniciAdi(model.Id, kullaniciAdi))
                .WithMessage("Sistemde bu isimde bir kullanici adi mevcut. Farkli bir kullanıcı adı seçiniz.");

            RuleFor(x => x.CalisiyorMu)
                .Must(x => x == true || x == false).WithMessage("Çalışan için çalışma durumu seçiniz.");

            RuleFor(x => x.CalisiyorMu)
                   .Must((model, CalisiyorMu) => !(FunctionsValidator.IsRoleMatching(model.RolId, new List<string> { "müşteri" }) && CalisiyorMu))
                   .WithMessage("Müşteriler çalışmıyor olarak işaretlenmelidir.");

            RuleFor(x => x.Maas)
                .NotNull().WithMessage("Maaş bilgisi boş olamaz.")
                .When(x => FunctionsValidator.IsRoleMatching(x.RolId, new List<string> { "admin", "çalışan", "veteriner" }) && x.CalisiyorMu);

            RuleFor(x => x.Maas)
                .Must(x => x.HasValue && x.Value >= 0).WithMessage("Maaş bilgisi pozitif bir sayı olmalıdır.")
                .When(x => FunctionsValidator.IsRoleMatching(x.RolId, new List<string> { "admin", "çalışan", "veteriner" }) && x.CalisiyorMu && x.Maas.HasValue);

            RuleFor(x => x.Maas)
                .Null().WithMessage("Müşteriler için maaş bilgisi girilemez")
                .When(x => FunctionsValidator.IsRoleMatching(x.RolId, new List<string> { "müşteri" }));

            RuleFor(x => x.Maas)
                .Null().WithMessage("Çalışmayan kişiler için maaş bilgisi girilemez.")
                .When(x => FunctionsValidator.IsRoleMatching(x.RolId, new List<string> { "admin", "çalışan", "veteriner" }) && !x.CalisiyorMu);

            RuleFor(x => x.RolId)
                .NotNull().WithMessage("Çalışan için bir görev seçiniz.")
                .NotNull().WithMessage("Çalışan için bir görev seçiniz.")
                .When(x => FunctionsValidator.IsRoleMatching(x.RolId, new List<string> { "admin", "çalışan", "veteriner" }));

            RuleFor(x => x.RolId)
                .NotNull().WithMessage("Müşteri tanımlaması yapınız.")
                .NotNull().WithMessage("Müşteri tanımlaması yapınız.")
                .When(x => FunctionsValidator.IsRoleMatching(x.RolId, new List<string> { "müşteri" }));

            RuleFor(x => x.RolId)
               .NotNull().WithMessage("Çalışan için bir görev seçiniz.")
               .NotNull().WithMessage("Çalışan için bir görev seçiniz.")
               .Must(FunctionsValidator.BeRol).WithMessage("Seçilen rol geçerli değil.");

        }

    }
}
