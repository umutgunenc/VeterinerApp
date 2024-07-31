using FluentValidation;
using System;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Account;

namespace VeterinerApp.Models.Validators.Account
{
    public class RegisterValidators : AbstractValidator<RegisterViewModel>
    {
        private readonly VeterinerContext _context;

        public RegisterValidators(VeterinerContext contex)
        {
            _context = contex;

            RuleFor(x => x.UserName)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda kullanıcı adı girilebilir")
                .NotEmpty().WithMessage("Kullanıcı adını giriniz.")
                .NotNull().WithMessage("Kullanıcı adını giriniz.")
                .Must(BeUniqueKullaniciAdi).WithMessage("Sistemde bu isimde bir kullanici adi mevcut. Farkli bir kullanıcı adı seçiniz.");

            //RuleFor(x => x.PasswordHash)
            //    .NotNull().WithMessage("Lütfen şifrenizi giriniz.")
            //    .NotEmpty().WithMessage("Lütfen şifrenizi giriniz.")
            //    .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
            //    .MaximumLength(50).WithMessage("Şifre maksimum 50 karakter olabilir.");

            RuleFor(x => x.PasswordHash)
                .NotNull().WithMessage("Lütfen şifrenizi giriniz.")
                .NotEmpty().WithMessage("Lütfen şifrenizi giriniz.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Şifre maksimum 50 karakter olabilir.");
            //TODO : Şifre için kontroller eklenecek
            //.Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.") 
            //.Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
            //.Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
            //.Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel işaret içermelidir.");

            RuleFor(x => x.ConfirmPassword)
                .NotNull().WithMessage("Lütfen şifrenizi tekrar giriniz.")
                .NotEmpty().WithMessage("Lütfen şifrenizi tekrar giriniz.")
                .Equal(x => x.PasswordHash).WithMessage("Şifreler uyuşmuyor");

            RuleFor(x => x.InsanAdi)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda isim girilebilir.")
                .NotEmpty().WithMessage("İsminizi giriniz.")
                .NotNull().WithMessage("İsminizi giriniz.");

            RuleFor(x => x.InsanSoyadi)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda soyisim girilebilir.")
                .NotEmpty().WithMessage("Soyisminizi giriniz.")
                .NotNull().WithMessage("Soyisminizi giriniz.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(11).WithMessage("Telefon numarası maksimum 11 karakter olabilir.")
                .NotEmpty().WithMessage("Lütfen telefon numarasını giriniz.")
                .NotNull().WithMessage("Lütfen telefon numarasını giriniz.")
                .Matches(@"^0\d{10}$").WithMessage("Telefon numarası geçersiz.")
                .Must(UniqueTel).WithMessage("Girilen telefon numarası zaten sisteme kayıtlı");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.")
                .NotNull().WithMessage("Lütfen e-mail adresi giriniz.")
                .NotEmpty().WithMessage("Lütfen e-mail adresi giriniz.")
                .MaximumLength(100).WithMessage("e-mail adresi maksimum 100 karakter uzunluğunda olabilir.")
                .Must(UniqueEmail).WithMessage("Girilen e-posta adresi zaten sisteme kayıtlı.");

            RuleFor(x => x.InsanTckn)
                .NotEmpty().WithMessage("Lütfen TCKN giriniz.")
                .NotNull().WithMessage("Lütfen TCKN giriniz.")
                .Length(11).WithMessage("TCKN 11 karakter uzunluğunda olmalıdır.")
                .Matches("^[0-9]*$").WithMessage("TCKN numarası sadece rakamlardan oluşmalıdır.")
                .Must(UniqueTCKN).WithMessage("Girilen TCKN zaten sistemde kayıtlı.")
                .Must(TcDogrula).WithMessage("Geçerli bir TCKN giriniz.");

            RuleFor(x => x.TermOfUse)
                .Equal(true).WithMessage("Kullanım şartlarını kabul etmelisiniz.");

        }
        private bool BeUniqueKullaniciAdi(string kullaniciAdi)
        {
            if (string.IsNullOrEmpty(kullaniciAdi))
                return true;
            return !_context.Users.Any(x => x.UserName.ToUpper() == kullaniciAdi.ToUpper());
        }
        private bool UniqueTel(string TelNo)
        {
            return !_context.Users.Any(x => x.PhoneNumber.ToUpper() == TelNo.ToUpper());
        }
        private bool UniqueTCKN(string girilenTCKN)
        {
            return !_context.Users.Any(x => x.InsanTckn.ToUpper() == girilenTCKN.ToUpper());
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
        private bool UniqueEmail(string insanMail)
        {
            if (string.IsNullOrEmpty(insanMail))
                return true;

            return !_context.Insans.Any(x => x.Email.ToUpper() == insanMail.ToUpper() || x.Email.ToLower() == insanMail.ToLower());
        }

    }
}
