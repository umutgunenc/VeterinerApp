using FluentValidation;
using System.Collections.Generic;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using System;
using System.Linq;
using VeterinerApp.Models.ViewModel.Admin;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class InsanDuzenleValidators : AbstractValidator<InsanDuzenleViewModel>
    {
        private readonly VeterinerContext _context;
        public InsanDuzenleValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.InsanTckn)
                .NotEmpty().WithMessage("Lütfen TCKN giriniz.")
                .NotNull().WithMessage("Lütfen TCKN giriniz.")
                .Length(11).WithMessage("TCKN 11 karakter uzunluğunda olmalıdır.")
                .Matches("^[0-9]*$").WithMessage("TCKN numarası sadece rakamlardan oluşmalıdır.")
                .Must(TcDogrula).WithMessage("Geçerli bir TCKN giriniz.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(11).WithMessage("Telefon numarası maksimum 11 karakter olabilir.")
                .NotEmpty().WithMessage("Lütfen telefon numarasını giriniz.")
                .NotNull().WithMessage("Lütfen telefon numarasını giriniz.")
                .Matches(@"^0\d{10}$").WithMessage("Telefon numarası geçersiz.")
                .Must((model, insanTel) => UniqueTel(model.InsanTckn, insanTel))
                .WithMessage("Girilen telefon numarası zaten sisteme kayıtlı.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.")
                .NotNull().WithMessage("Lütfen e-mail adresi giriniz.")
                .NotEmpty().WithMessage("Lütfen e-mail adresi giriniz.")
                .MaximumLength(100).WithMessage("e-mail adresi maksimum 100 karakter uzunluğunda olabilir.")
                .Must((model, insanMail) => UniqueEmailSelf(model.InsanTckn, insanMail))
                .WithMessage("Girilen e-posta adresi zaten sisteme kayıtlı.");

            RuleFor(x => x.DiplomaNo)
                .MaximumLength(11).WithMessage("Diploma numarası en fazla 11 karakter olabilir.")
                .Must((model, diplomaNo) => BeUniqueOrNullDiplomaNo(model.InsanTckn, diplomaNo))
                .WithMessage("Girilen diploma numarası zaten sistemde kayıtlı.");

            RuleFor(x => x.DiplomaNo)
                .NotNull().WithMessage("Diploma numarası giriniz.")
                .When(x => IsRoleMatching(x.rolId, new List<string> { "VETERİNER" }));

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
                .Must((model, kullaniciAdi) => BeUniqueKullaniciAdi(model.InsanTckn, kullaniciAdi))
                .WithMessage("Sistemde bu isimde bir kullanici adi mevcut. Farkli bir kullanıcı adı seçiniz.");

            RuleFor(x => x.CalisiyorMu)
                .Must(x => x == true || x == false).WithMessage("Çalışan için çalışma durumu seçiniz.");

            RuleFor(x => x.CalisiyorMu)
                   .Must((model, CalisiyorMu) => !(IsRoleMatching(model.rolId, new List<string> { "MÜŞTERİ" }) && CalisiyorMu))
                   .WithMessage("Müşteriler çalışmıyor olarak işaretlenmelidir.");

            RuleFor(x => x.Maas)
                .NotNull().WithMessage("Maaş bilgisi boş olamaz.")
                .When(x => IsRoleMatching(x.rolId,new List<string> { "ADMİN", "ÇALIŞAN", "VETERİNER" }) && x.CalisiyorMu);

            RuleFor(x => x.Maas)
                .Must(x => x.HasValue && x.Value >= 0).WithMessage("Maaş bilgisi pozitif bir sayı olmalıdır.")
                .When(x => IsRoleMatching(x.rolId, new List<string> { "ADMİN", "ÇALIŞAN", "VETERİNER" }) && x.CalisiyorMu && x.Maas.HasValue);

            RuleFor(x => x.Maas)
                .Null().WithMessage("Müşteriler için maaş bilgisi girilemez")
                .When(x => IsRoleMatching(x.rolId, new List<string> { "MÜŞTERİ" }));

            RuleFor(x => x.Maas)
                .Null().WithMessage("Çalışmayan kişiler için maaş bilgisi girilemez.")
                .When(x => IsRoleMatching(x.rolId, new List<string> { "ADMİN", "ÇALIŞAN", "VETERİNER" }) && !x.CalisiyorMu);

            RuleFor(x => x.rolId)
                .NotNull().WithMessage("Çalışan için bir görev seçiniz.")
                .NotNull().WithMessage("Çalışan için bir görev seçiniz.")
                .When(x => IsRoleMatching(x.rolId, new List<string> { "ADMIN", "ÇALIŞAN", "VETERINER" }));

            RuleFor(x => x.rolId)
                .NotNull().WithMessage("Müşteri tanımlaması yapınız.")
                .NotNull().WithMessage("Müşteri tanımlaması yapınız.")
                .When(x => IsRoleMatching(x.rolId, new List<string> { "MÜŞTERİ" }));

            RuleFor(x => x.rolId)
               .NotNull().WithMessage("Çalışan için bir görev seçiniz.")
               .NotNull().WithMessage("Çalışan için bir görev seçiniz.")
               .Must(beRol).WithMessage("Seçilen rol geçerli değil.");

        }
        private bool beRol(int rolId)
        {
            return _context.Roles.Any(x => x.Id == rolId);
        }
        private bool BeUniqueKullaniciAdi(string InsanTckn, string kullaniciAdi)
        {
            if (string.IsNullOrEmpty(kullaniciAdi))
                return true;

            return !_context.Users.Any(x => x.UserName.ToUpper() == kullaniciAdi.ToUpper() && x.InsanTckn != InsanTckn);
        }
        private bool BeUniqueOrNullDiplomaNo(string InsanTckn, string diplomaNo)
        {
            if (string.IsNullOrEmpty(diplomaNo))
                return true;

            return !_context.Users.Any(x => x.DiplomaNo == diplomaNo && x.InsanTckn.ToUpper() != InsanTckn.ToUpper());
        }
        private bool UniqueTel(string InsanTckn, string insanTel)
        {
            if (string.IsNullOrEmpty(insanTel))
                return true;

            return !_context.Users.Any(x => x.PhoneNumber == insanTel && x.InsanTckn != InsanTckn);
        }
        private bool TcDogrula(string tcKimlikNo)
        {
            bool returnvalue = false;
            if (string.IsNullOrEmpty(tcKimlikNo))
            {
                return false;
            }
            if (tcKimlikNo.Length == 11)
            {
                Int64 ATCNO, BTCNO, TcNo;
                long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;

                TcNo = Int64.Parse(tcKimlikNo);

                ATCNO = TcNo / 100;
                BTCNO = TcNo / 100;

                C1 = ATCNO % 10; ATCNO = ATCNO / 10;
                C2 = ATCNO % 10; ATCNO = ATCNO / 10;
                C3 = ATCNO % 10; ATCNO = ATCNO / 10;
                C4 = ATCNO % 10; ATCNO = ATCNO / 10;
                C5 = ATCNO % 10; ATCNO = ATCNO / 10;
                C6 = ATCNO % 10; ATCNO = ATCNO / 10;
                C7 = ATCNO % 10; ATCNO = ATCNO / 10;
                C8 = ATCNO % 10; ATCNO = ATCNO / 10;
                C9 = ATCNO % 10; ATCNO = ATCNO / 10;
                Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
                Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);

                returnvalue = ((BTCNO * 100) + (Q1 * 10) + Q2 == TcNo);
            }
            return returnvalue;
        }
        private bool IsRoleMatching(int rolId, List<string> validRoles)
        {
            var role = _context.Roles.Find(rolId);
            return role != null && validRoles.Contains(role.Name.ToUpper());
        }
        private bool UniqueEmailSelf(string InsanTckn, string insanMail)
        {
            if (string.IsNullOrEmpty(insanMail))
                return true;

            return !_context.Users
                .Any(x => x.Email.ToUpper() == insanMail.ToUpper()
                || x.Email.ToLower() == insanMail.ToLower()
                && x.InsanTckn != InsanTckn);
        }
    }
}
