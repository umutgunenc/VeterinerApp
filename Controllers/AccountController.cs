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
using VeterinerApp.Fonksiyonlar;
using System.IO;

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
            var user = _context.Users.FirstOrDefault(x => x.UserName == model.UserName);
            if (model.filePhoto != null)
            {
                var dosyaUzantısı = Path.GetExtension(model.filePhoto.FileName);
                var dosyaAdi = string.Format($"{Guid.NewGuid()}{dosyaUzantısı}");
                var userKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\user", user.Id.ToString());

                if (!Directory.Exists(userKlasoru))
                {
                    Directory.CreateDirectory(userKlasoru);
                }

                var filePath = Path.Combine(userKlasoru, dosyaAdi);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.filePhoto.CopyToAsync(stream);
                }

                // Web URL'sini oluşturma
                var fileUrl = $"/img/user/{user.Id}/{dosyaAdi}";

                // Veritabanına URL'yi kaydetme
                user.ImgURL = fileUrl;
                await _context.SaveChangesAsync();
            }

            IdentityUserRole<int> userRole = new()
            {
                RoleId = model.rolId,
                UserId = model.Id
            };

            _context.UserRoles.Add(userRole);


            if (_context.SaveChanges() > 0)
            {
                string mailBody = $"Veteriner bilgi sistemine kaydınız {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year} tarihinde başarılı bir şekilde oluşturulmuştur.\nKullanıcı Adınız : {model.UserName} \nŞifreniz : {model.PasswordHash}";

                string baslik = "Veteriner Bilgi Sistemi Kullanıcı Kaydı";

                MailGonder mail = new MailGonder(model.Email, mailBody, baslik);

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

                var roles = await _userManager.GetRolesAsync(user);

                if (user.CalisiyorMu == false && (roles.Contains("ÇALIŞAN") || roles.Contains("VETERİNER") || roles.Contains("ADMİN")))
                {
                    ModelState.AddModelError("PasswordHash", "Kullanıcı aktif değil. Lütfen yöneticinizle iletişime geçiniz.");
                    return View(model);
                }

                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.PasswordHash, isPersistent: false, lockoutOnFailure: true);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (signInResult.IsLockedOut)
                {
                    if (user.LockoutEnd.HasValue && user.LockoutEnd.Value.ToLocalTime() > DateTime.Now)
                    {
                        ModelState.AddModelError("LockoutEnd", $"Hesabınız kilitlenmiştir. Hesabınızın kilidi {user.LockoutEnd.Value.ToLocalTime().ToString("g")} 'de açılacaktır.");
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

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            ForgotPasswordValidators validator = new(_context);
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(x => x.InsanTckn == model.InsanTckn && x.Email == model.Email && x.PhoneNumber == model.PhoneNumber);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Girdiğiniz bilgiler birbirleri ile uyuşmuyor. Girdiğiniz TCKN, email adresi veya telefon numarası sistemde kayıtlı değil.");
                return View(model);
            }
            else
            {
                sifre sifre = new sifre();
                var yeniSifre = sifre.GeneratePassword();

                // Yeni şifreyi hashle
                var hashedNewPassword = _userManager.PasswordHasher.HashPassword(user, yeniSifre);

                // Eski şifreyi sakla
                var eskiSifreHash = user.PasswordHash;

                // Hashlenmiş şifreyi kullanıcıya ata
                user.PasswordHash = hashedNewPassword;

                // Kullanıcıyı güncelle
                var EskiSifreOlusturmaTarihi = user.SifreOlusturmaTarihi;
                var EskiSifreGecerlilikTarihi = user.SifreGecerlilikTarihi;
                user.SifreOlusturmaTarihi = DateTime.Now;
                user.SifreGecerlilikTarihi = DateTime.Now.AddDays(120);
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError("Email", error.Description);
                    }
                    return View(model);
                }

                string mailBody = $"Merhaba {user.InsanAdi.ToUpper()} {user.InsanSoyadi.ToUpper()}! \nŞifreniz {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year} tarihinde yenilenmişsitr.\nKullanıcı Adınız: {user.UserName}\nŞifreniz: {yeniSifre}";
                string baslik = "Veteriner Bilgi Sistemi Şifre Yenileme";
                MailGonder mail = new MailGonder(user.Email, mailBody, baslik);
                if (!mail.MailGonderHotmail(mail))
                {
                    // Mail gönderme başarısız olursa eski şifreyi geri yükle
                    user.PasswordHash = eskiSifreHash;
                    user.SifreOlusturmaTarihi = EskiSifreOlusturmaTarihi;
                    user.SifreGecerlilikTarihi = EskiSifreGecerlilikTarihi;
                    await _userManager.UpdateAsync(user);

                    ViewBag.Hata = "Mail Gönderme işlemi başarısız oldu. Şifre gönderme işlemi tamamlanamadı.";
                    return View(model);
                }

                TempData["SifreGonderildi"] = $"{user.InsanAdi.ToUpper()} {user.InsanSoyadi.ToUpper()} isimli kişinin şifresi {user.Email.ToUpper()} adresine gönderildi.";

                return View();
            }
        }
    }
}
