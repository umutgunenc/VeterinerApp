using FluentValidation;
using System;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Login;

namespace VeterinerApp.Models.Validators
{
    public class LoginValidators : AbstractValidator<LoginViewModel>
    {
        private readonly VeterinerContext _context;
        public LoginValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.KullaniciAdi)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.");

            //RuleFor(x => x.sifre)
            //    .NotEmpty().WithMessage("Şifre boş olamaz.") 
            //    .Must((model, sifre) => _context.Sifres.Any(x => x.KullaniciAdi == model.KullaniciAdi && x.sifre == sifre))
            //    .WithMessage("Kullanıcı adı veya şifre hatalı.");

            //RuleFor(x => x.sifre)
            //    .Must((user, sifre) => _context.Sifres.Any(y => y.KullaniciAdi == user.KullaniciAdi && y.SifreGecerlilikTarihi >= DateTime.Now))
            //    .WithMessage("Şifre geçerlilik süresi dolmuş. Lütfen şifrenizi değiştiriniz.")
            //    .When(user => _context.Sifres.Any(y => y.KullaniciAdi == user.KullaniciAdi && y.sifre == user.sifre));
        }

        //private bool beKullanici(string kullaniciAdi)
        //{
        //    return _context.Sifres.Any(x => x.KullaniciAdi == kullaniciAdi);
        //}
    }
}
