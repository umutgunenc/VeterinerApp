using FluentValidation;
using System;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Account;


namespace VeterinerApp.Models.Validators.Account
{
    public class LoginValidators : AbstractValidator<LoginViewModel>
    {
        private readonly VeterinerContext _context;
        public LoginValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .NotNull().WithMessage("Kullanıcı adı boş olamaz.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .Must((model, sifre) => _context.Users.Any(x => x.UserName == model.UserName && x.PasswordHash == sifre))
                .WithMessage("Kullanıcı adı veya şifre hatalı.");

            RuleFor(x => x.PasswordHash)
                .Must((user, sifre) => _context.Users.Any(y => y.UserName == user.UserName && y.SifreGecerlilikTarihi >= DateTime.Now))
                .WithMessage("Şifre geçerlilik süresi dolmuş. Lütfen şifrenizi değiştiriniz.")
                .When(user => _context.Users.Any(y => y.UserName == user.UserName && y.PasswordHash == user.PasswordHash));
        }

        private bool beKullanici(string kullaniciAdi)
        {
            return _context.Users.Any(x => x.UserName == kullaniciAdi);
        }
    }
}
