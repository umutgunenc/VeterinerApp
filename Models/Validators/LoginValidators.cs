using FluentValidation;
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
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .Must(beKullanici).WithMessage("Kullanıcı adı sistemde kayıtlı değil.");

            RuleFor(x => x.sifre)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .Must((user, sifre) => _context.Sifres.Any(x => x.KullaniciAdi == user.KullaniciAdi && x.sifre == sifre)).WithMessage("Şifre hatalı.");

            RuleFor(x=>x.SifreGecerlilikTarihi)
                .Must((user, sifre) => _context.Sifres.Any(x => x.KullaniciAdi == user.KullaniciAdi && x.SifreGecerlilikTarihi >= System.DateTime.Now)).WithMessage("Şifre geçerlilik süresi dolmuş. Lütfen şifrenizi değiştiriniz.");
        }

        private bool beKullanici(string kullaniciAdi)
        {
            return _context.Sifres.Any(x => x.KullaniciAdi == kullaniciAdi);
        }
    }
}
