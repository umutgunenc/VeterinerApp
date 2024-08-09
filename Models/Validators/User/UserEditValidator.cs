using FluentValidation;
using FluentValidation.AspNetCore;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.User;

namespace VeterinerApp.Models.Validators.User
{
    public class UserEditValidator :AbstractValidator<EditUserViewModel>
    {
        public UserEditValidator()
        {

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.")
                .NotNull().WithMessage("Lütfen e-mail adresi giriniz.")
                .NotEmpty().WithMessage("Lütfen e-mail adresi giriniz.")
                .MaximumLength(100).WithMessage("e-mail adresi maksimum 100 karakter uzunluğunda olabilir.")
                .Must((model,email)=> FunctionsValidator.BeUniqueEmail(model.Id, email))
                .WithMessage("Girilen e-posta adresi zaten sisteme kayıtlı.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(11).WithMessage("Telefon numarası maksimum 11 karakter olabilir.")
                .NotEmpty().WithMessage("Lütfen telefon numarasını giriniz.")
                .NotNull().WithMessage("Lütfen telefon numarasını giriniz.")
                .Matches(@"^0\d{10}$").WithMessage("Telefon numarası geçersiz.")
                .Must((model,phoneNumber)=>FunctionsValidator.BeUniqueTel(model.Id,phoneNumber)).WithMessage("Girilen telefon numarası zaten sisteme kayıtlı");

            RuleFor(x => x.InsanAdi)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda isim girilebilir.")
                .NotEmpty().WithMessage("İsminizi giriniz.")
                .NotNull().WithMessage("İsminizi giriniz.");

            RuleFor(x => x.InsanSoyadi)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda soyisim girilebilir.")
                .NotEmpty().WithMessage("Soyisminizi giriniz.")
                .NotNull().WithMessage("Soyisminizi giriniz.");

            RuleFor(x => x.UserName)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda kullanıcı adı girilebilir")
                .NotEmpty().WithMessage("Kullanıcı adınızı giriniz.")
                .NotNull().WithMessage("Kullanıcı adınızı giriniz.")
                .Must((model, kullaniciAdi) => FunctionsValidator.BeUniqueKullaniciAdi(model.Id, kullaniciAdi))
                .WithMessage("Sistemde bu isimde bir kullanici adi mevcut. Farkli bir kullanıcı adı seçiniz.");

            RuleFor(x => x.PhotoOption)
                .NotEmpty().WithMessage("Lütfen bir seçenek seçiniz.")
                .NotNull().WithMessage("Lütfen bir seçenek seçiniz.")
                .Must(FunctionsValidator.BeValidRadioPhotoAdd).WithMessage("Geçersiz seçenek.");

            RuleFor(x => x.filePhoto)
                .Must(FunctionsValidator.BeValidExtensionForPhoto)
                .WithMessage("Yalnızca jpg, jpeg, png ve gif uzantılı dosyalar yüklenebilir.")
                .When(x => x.PhotoOption == "changePhoto" && x.filePhoto != null)
                .WithName("filePhoto");

            RuleFor(x => x.filePhoto)
                .NotEmpty()
                .When(x => x.PhotoOption == "changePhoto")
                .WithMessage("Fotoğraf yüklemek doğru seçeneği seçiniz.");

            RuleFor(x => x.filePhoto)
                .Empty()
                .When(x => x.PhotoOption == "keepPhoto" || x.PhotoOption == "useDefault")
                .WithMessage("Fotoğraf yüklemek doğru seçeneği seçiniz.");

            RuleFor(x => x.filePhoto)
                .Must(x => x.Length < 10485760)
                .When(x => x.filePhoto != null)
                .WithMessage("Fotoğraf boyutu 10MB'dan küçük olmalıdır.");

        }

        
    }
}
