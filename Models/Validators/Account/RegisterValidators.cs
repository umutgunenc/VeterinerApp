using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Account;

namespace VeterinerApp.Models.Validators.Account
{
    public class RegisterValidators : AbstractValidator<RegisterViewModel>
    {

        public RegisterValidators()
        {

            RuleFor(x => x.UserName)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda kullanıcı adı girilebilir")
                .NotEmpty().WithMessage("Kullanıcı adını giriniz.")
                .NotNull().WithMessage("Kullanıcı adını giriniz.")
                .Must(FunctionsValidator.BeUniqueKullaniciAdi).WithMessage("Sistemde bu isimde bir kullanici adi mevcut. Farkli bir kullanıcı adı seçiniz.");

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
                .Must(FunctionsValidator.BeUniqueTel).WithMessage("Girilen telefon numarası zaten sisteme kayıtlı");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.")
                .NotNull().WithMessage("Lütfen e-mail adresi giriniz.")
                .NotEmpty().WithMessage("Lütfen e-mail adresi giriniz.")
                .MaximumLength(100).WithMessage("e-mail adresi maksimum 100 karakter uzunluğunda olabilir.")
                .Must(FunctionsValidator.BeUniqueEmail).WithMessage("Girilen e-posta adresi zaten sisteme kayıtlı.");

            RuleFor(x => x.InsanTckn)
                .NotEmpty().WithMessage("Lütfen TCKN giriniz.")
                .NotNull().WithMessage("Lütfen TCKN giriniz.")
                .Length(11).WithMessage("TCKN 11 karakter uzunluğunda olmalıdır.")
                .Matches("^[0-9]*$").WithMessage("TCKN numarası sadece rakamlardan oluşmalıdır.")
                .Must(FunctionsValidator.BeUniqueTCKN).WithMessage("Girilen TCKN zaten sistemde kayıtlı.")
                .Must(FunctionsValidator.BeValidTCKN).WithMessage("Geçerli bir TCKN giriniz.");

            RuleFor(x => x.TermOfUse)
                .Equal(true).WithMessage("Kullanım şartlarını kabul etmelisiniz.");

            RuleFor(x => x.filePhoto)
                .Must(FunctionsValidator.BeValidExtensionForPhoto)
                .WithMessage("Yalnızca jpg, jpeg, png ve bmp uzantılı dosyalar yüklenebilir.")
                .When(x => x.filePhoto != null)
                .WithName("filePhoto");

            RuleFor(x => x.filePhoto)
                .Must(x => x.Length < 5242880)
                .When(x => x.filePhoto != null)
                .WithMessage("Fotoğraf boyutu 5MB'dan küçük olmalıdır.");

        }



    }
}
