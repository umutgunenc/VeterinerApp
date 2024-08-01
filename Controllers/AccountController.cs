using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Fonksiyonlar.MailGonderme;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.Account;
using VeterinerApp.Models.ViewModel.Account;
using System;

namespace VeterinerApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly VeterinerContext _context;

        public AccountController(UserManager<AppUser> userManager, VeterinerContext context, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
            model.SifreOlusturmaTarihi = DateTime.Now;
            model.SifreGecerlilikTarihi = DateTime.Now.AddDays(120);
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

            var createResult = await _userManager.CreateAsync(model, model.PasswordHash);

            if (!createResult.Succeeded)
            {
                foreach (var error in createResult.Errors)
                {
                    ModelState.AddModelError("PasswordHash", error.Description);
                }
                return View(model);
            }

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
            LoginValidators validator = new(_context);
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    ModelState.AddModelError("PasswordHash", "Kullanıcı adı veya şifre hatalı.");
                    return View(model);
                }

                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.PasswordHash, isPersistent: true, lockoutOnFailure: true);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (signInResult.IsLockedOut)
                {
                    if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.Now)
                    {
                        ModelState.AddModelError("LockoutEnd", $"Hesabınız kilitlenmiştir. Hesabınızın kilidi {user.LockoutEnd.Value.ToString("g")} tarihinde açılacaktır.");
                    }
                    else
                    {
                        ModelState.AddModelError("LockoutEnd", "Hesabınız kilitlenmiştir. Lütfen daha sonra tekrar deneyiniz.");
                    }
                }
                else if (user.AccessFailedCount>=0)
                {
                    ModelState.AddModelError("AccessFailedCount", $"Hesabınıza 3 defa yanlış giriş yaptığınızda kitlenecektir. {user.AccessFailedCount} kere yanlış giriş yapıldı.");
                    ModelState.AddModelError("PasswordHash", "Kullanıcı adı veya şifre hatalı.");
                }
                else
                {
                    ModelState.AddModelError("PasswordHash", "Kullanıcı adı veya şifre hatalı.");
                }
            }

            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
