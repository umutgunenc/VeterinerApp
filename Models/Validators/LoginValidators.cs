using FluentValidation;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Login;

namespace VeterinerApp.Models.Validators
{
    public class LoginValidators : AbstractValidator<LoginViewModel>
    {
        private readonly VeterinerContext _context;
        public LoginValidators(VeterinerContext context)
        {
            RuleFor(x => x.KullaniciAdi)
                .NotEmpty()
                .WithMessage("Kullanıcı adı boş olamaz.");

            RuleFor(x => x.sifre)
                .NotEmpty()
                .WithMessage("Şifre boş olamaz.");

            RuleFor(x=>x.SifreGecerlilikTarihi)
                .Must(x => x >= System.DateTime.Now)
                .WithMessage("Şifre geçerlilik süresi dolmuş. Lütfen şifrenizi değiştiriniz.");
        }
    }
}
