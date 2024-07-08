using FluentValidation;
using System.Collections.Generic;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using System;
using System.Linq;
using VeterinerApp.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class InsanEkleValidators : AbstractValidator<InsanEkleViewModel>
    {
        private readonly VeterinerContext _context;
        public InsanEkleValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.InsanTckn)
                .NotEmpty().WithMessage("Lütfen TCKN giriniz.")
                .NotNull().WithMessage("Lütfen TCKN giriniz.")
                .Length(11).WithMessage("TCKN 11 karakter uzunluğundadır.")
                .MaximumLength(11).WithMessage("TCKN 11 karakter uzunluğundadır.")
                .Matches("^[0-9]*$").WithMessage("TCKN numarası sadece rakamlardan oluşmalıdır.")
                .Must(UniqueTCKN).WithMessage("Girilen TCKN zaten sistemde kayıtlı.")
                .Must(TcDogrula).WithMessage("Geçerli bir TCKN giriniz.");

            RuleFor(x => x.InsanMail)
                .EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.")
                .NotNull().WithMessage("Lütfen e-mail adresi giriniz.")
                .NotEmpty().WithMessage("Lütfen e-mail adresi giriniz.")
                .MaximumLength(100).WithMessage("e-mail adresi maksimum 100 karakter uzunluğunda olabilir.");

            RuleFor(x => x.InsanTel)
                .MaximumLength(11).WithMessage("Telefon numarası maksimum 11 karakter olabilir.")
                .NotEmpty().WithMessage("Lütfen telfon numarasını giriniz.")
                .NotNull().WithMessage("Lütfen telfon numarasını giriniz.")
                .Matches(@"^0\d{10}$").WithMessage("Telefon numarası geçersiz.")
                .Must(UniqueTel).WithMessage("Girilen telefon numarası zaten sisteme kayıtlı");

            RuleFor(x => x.DiplomaNo)
                .MaximumLength(11).WithMessage("Diploma numarası en fazla 11 karakter olabilir.")
                .Must(BeUniqueOrNullDiplomaNo).WithMessage("Girilen diploma numarası zaten sistemde kayıtlı.");

            RuleFor(x => x.InsanAdi)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda isim girilebilir.")
                .NotEmpty().WithMessage("Çalışanın ismini giriniz.")
                .NotNull().WithMessage("Çalışanın ismini giriniz.");

            RuleFor(x => x.InsanSoyadi)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda soyisim girilebilir.")
                .NotEmpty().WithMessage("Çalışanın soyismini giriniz.")
                .NotNull().WithMessage("Çalışanın soyismini giriniz.");

            RuleFor(x => x.KullaniciAdi)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda kullanıcı adı girilebilir")
                .NotEmpty().WithMessage("Çalışanın kullanıcı adını giriniz.")
                .NotNull().WithMessage("Çalışanın kullanıcı adını giriniz.")
                .Must(BeUniqueKullaniciAdi).WithMessage("Sistemde bu isimde bir kullanici adi mevcut. Farkli bir kullanıcı adı seçiniz.");

            RuleFor(x => x.CalisiyorMu)
                .NotNull().WithMessage("Çalışan için çalışma durumu seçiniz.")
                .NotEmpty().WithMessage("Çalışan için çalışma durumu seçiniz.")
                .Must(x => x == true || x == false).WithMessage("Çalışan için çalışma durumu seçiniz.");

            RuleFor(x => x.Maas)
                .NotEmpty().WithMessage("Maaş bilgisi boş olamaz.")
                .NotNull().WithMessage("Maaş bilgisi boş olamaz.")
                .Must(maas => maas is double?).WithMessage("Maaş bilgisini yanlış giriniz.")
                .When(x => IsRoleMatching(x.RolId, new List<string> { "ADMIN", "ÇALIŞAN", "VETERINER" }));

            RuleFor(x => x.Maas)
                .Null().WithMessage("Müşteriler için maaş bilgisi girilemez")
                .When(x => IsRoleMatching(x.RolId, new List<string> { "MÜŞTERİ" }));


            RuleFor(x => x.RolId)
                .NotNull().WithMessage("Çalışan için bir görev seçiniz.")
                .NotNull().WithMessage("Çalışan için bir görev seçiniz.")
                .When(x => IsRoleMatching(x.RolId, new List<string> { "ADMIN", "ÇALIŞAN", "VETERINER" }));


            RuleFor(x => x.RolId)
                .NotNull().WithMessage("Müşteri tanımlaması yapınız.")
                .NotNull().WithMessage("Müşteri tanımlaması yapınız.")
                .When(x => IsRoleMatching(x.RolId, new List<string> { "MÜŞTERİ" }));

            RuleFor(x => x.RolId)
               .NotNull().WithMessage("Çalışan için bir görev seçiniz.")
               .NotNull().WithMessage("Çalışan için bir görev seçiniz.");

        }
        private bool BeUniqueKullaniciAdi(string kullaniciAdi)
        {
            return !_context.Insans.Any(x => x.KullaniciAdi.ToUpper() == kullaniciAdi.ToUpper());
        }
        private bool BeUniqueOrNullDiplomaNo(string diplomaNumarasi)
        {
            if (string.IsNullOrEmpty(diplomaNumarasi))
            {
                return true;
            }
            return !_context.Insans.Any(x => x.DiplomaNo.ToUpper() == diplomaNumarasi.ToUpper());
        }
        private bool UniqueTel(string TelNo)
        {
            return !_context.Insans.Any(x => x.InsanTel.ToUpper() == TelNo.ToUpper());
        }
        private bool UniqueTCKN(string girilenTCKN)
        {
            return !_context.Insans.Any(x => x.InsanTckn.ToUpper() == girilenTCKN.ToUpper());
        }
        private bool TcDogrula(string tcKimlikNo)
        {
            bool returnvalue = false;
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
            var role = _context.Rols.Find(rolId);
            return role != null && validRoles.Contains(role.RolAdi.ToUpper());
        }
    }
}
