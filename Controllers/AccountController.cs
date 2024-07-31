using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Fonksiyonlar.MailGonderme;
using VeterinerApp.Fonksiyonlar;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.Account;
using VeterinerApp.Models.ViewModel.Account;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly VeterinerContext _context;

        public AccountController(UserManager<AppUser> userManager, VeterinerContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            model.rolId = _context.Roles
                .Where(x => x.Name == "MÜŞTERİ")
                .Select(r => r.Id)
                .FirstOrDefault();

            model.CalisiyorMu = false;
            model.SifreOlusturmaTarihi = System.DateTime.Now;
            model.SifreGecerlilikTarihi = System.DateTime.Now.AddMonths(3);
            model.UserName = model.UserName.ToUpper();

            RegisterValidators validator = new(_context);
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(model);
            }

            _context.Users.Add(model);
            _context.SaveChanges();

            IdentityUserRole<int> userRole = new()
            {
                RoleId = model.rolId,
                UserId = model.Id
            };

            _context.UserRoles.Add(userRole);

            if (_context.SaveChanges() > 0)
            {
                MailGonder mail = new MailGonder(model.Email, model.UserName, model.PasswordHash);

                if (!mail.MailGonderHotmail(mail))
                {
                    ViewBag.Hata = "Mail Gönderme işlemi başarısız oldu. Kayıt işlemi tamamlanamadı.";
                    _context.Users.Remove(model);
                    _context.UserRoles.Remove(userRole);
                    _context.SaveChanges();

                    return View(model);
                }

                TempData["KisiEklendi"] = $"{model.InsanAdi.ToUpper()} {model.InsanSoyadi.ToUpper()} isimli kişi sisteme kaydedildi. Kullanıcı adı ve şifresi {model.Email.ToUpper()} adresine gönderildi.";
            }
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            return View();

        }
    }
}
