using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.Account;
using VeterinerApp.Models.Validators.User;
using VeterinerApp.Models.ViewModel.Account;
using VeterinerApp.Models.ViewModel.Animal;
using VeterinerApp.Models.ViewModel.User;

namespace VeterinerApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly VeterinerContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(VeterinerContext context, UserManager<AppUser> userManager, IEmailSender emailSender, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Information()
        {
            var user = await _userManager.GetUserAsync(User);

            // Kullanıcının sahip olduğu hayvanların ID'lerini al
            var hayvanIdler = _context.SahipHayvans
                .Where(s => s.SahipTckn == user.InsanTckn)
                .Select(h => h.HayvanId)
                .ToList();

            // Hayvanların detaylarını al
            List<HayvanlarViewModel> hayvanlar = await _context.Hayvans
                .Where(h => hayvanIdler.Contains(h.HayvanId))
                .Select(h => new HayvanlarViewModel
                {
                    HayvanId = h.HayvanId,
                    HayvanAdi = h.HayvanAdi,
                    HayvanCinsiyet = h.HayvanCinsiyet,
                    HayvanKilo = h.HayvanKilo,
                    HayvanDogumTarihi = h.HayvanDogumTarihi,
                    HayvanOlumTarihi = h.HayvanOlumTarihi,
                    HayvanAnneAdi = _context.Hayvans.Where(ha => ha.HayvanId == h.HayvanAnneId).Select(ha => ha.HayvanAdi).FirstOrDefault(),
                    HayvanBabaAdi = _context.Hayvans.Where(hb => hb.HayvanId == h.HayvanBabaId).Select(hb => hb.HayvanAdi).FirstOrDefault(),
                    TurAdi = _context.Turs.Where(t => t.Id == h.TurId).Select(t => t.tur).FirstOrDefault(),
                    CinsAdi = _context.Cins.Where(c => c.Id == h.CinsId).Select(c => c.cins).FirstOrDefault(),
                    RenkAdi = _context.Renks.Where(r => r.Id == h.RenkId).Select(r => r.renk).FirstOrDefault(),

                })
                .ToListAsync();

            var tuplle = (user, hayvanlar);
            return View(tuplle);

        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePaswordViewModel model)
        {

            var user = _userManager.GetUserAsync(User).Result;

            ChangePaswordValidators Validator = new ChangePaswordValidators();
            var result = Validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }

            var resultChangePasword = _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!resultChangePasword.Result.Succeeded)
            {
                ModelState.AddModelError("OldPassword", "Eski şifreniz yanlış.");
                return View();
            }
            else
            {
                user.SifreOlusturmaTarihi = System.DateTime.Now;
                user.SifreGecerlilikTarihi = System.DateTime.Now.AddDays(120);
                await _userManager.UpdateAsync(user);
                TempData["PasswordChanged"] = $"Sayın {user.UserName}, Şifreniz başarıyla değiştirildi.";
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            EditUserViewModel model = new();
            model.InsanAdi = user.InsanAdi.ToUpper();
            model.InsanSoyadi = user.InsanSoyadi.ToUpper();
            model.Email = user.Email.ToLower();
            model.UserName = user.UserName.ToUpper();
            model.PhoneNumber = user.PhoneNumber;
            model.Id = user.Id;
            model.ImgURL = user.ImgURL;


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEdit(EditUserViewModel model)
        {
            UserEditValidator validator = new();
            ValidationResult result = validator.Validate(model);
            var user = await _context.Users.FindAsync(model.Id);

            EditUserViewModel returnModel = new()
            {
                InsanAdi = model.InsanAdi.ToUpper(),
                InsanSoyadi = model.InsanSoyadi.ToUpper(),
                Email = model.Email.ToLower(),
                UserName = model.UserName.ToUpper(),
                PhoneNumber = model.PhoneNumber,
                Id = model.Id,
                ImgURL = user.ImgURL

            };

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View("EditUser", returnModel);
            }

            user.InsanAdi = model.InsanAdi.ToUpper();
            user.InsanSoyadi = model.InsanSoyadi.ToUpper();
            user.Email = model.Email.ToLower();
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.UserName.ToUpper();

            if (model.PhotoOption == "changePhoto" && model.filePhoto != null)
            {


                var dosyaUzantısı = Path.GetExtension(model.filePhoto.FileName);

                var dosyaAdi = string.Format($"{Guid.NewGuid()}{dosyaUzantısı}");
                var userKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\user", user.Id.ToString());

                if (!Directory.Exists(userKlasoru))
                {
                    Directory.CreateDirectory(userKlasoru);
                }
                var eskiFotograflar = Directory.GetFiles(userKlasoru);

                if (eskiFotograflar.Length > 0)
                {
                    foreach (var eskiFotograf in eskiFotograflar)
                    {
                        System.IO.File.Delete(eskiFotograf);
                    }
                }

                var filePath = Path.Combine(userKlasoru, dosyaAdi);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.filePhoto.CopyToAsync(stream);
                }

                // Web URL'sini oluşturma
                var fileUrl = $"/img/user/{user.Id}/{dosyaAdi}";

                user.ImgURL = fileUrl;




            }
            else if (model.PhotoOption == "changePhoto" && model.filePhoto == null)
            {
                user.ImgURL = _context.Users.FindAsync(model.Id).Result.ImgURL;
            }
            else if (model.PhotoOption == "deletePhoto")
            {
                user.ImgURL = null;
            }
            else if (model.PhotoOption == "keepPhoto")
            {
                user.ImgURL = _context.Users.FindAsync(model.Id).Result.ImgURL;
            }

            _userManager.UpdateAsync(user).Wait();
            returnModel.ImgURL = user.ImgURL;

            TempData["EditUser"] = $"{user.InsanAdi} {user.InsanSoyadi} isimli kişinin kullanıcı bilgileri başarıyla güncellendi.";

            return View("EditUser", returnModel);
        }

        public async Task<IActionResult> SendDeletionEmail()
        {
            var user = await _userManager.GetUserAsync(User);

            var code = await _userManager.GenerateUserTokenAsync(user, "Default", "DeleteAccount");
            var callbackUrl = Url.Action("DeleteAccount", "User", new { userId = user.Id, code = code }, protocol: Request.Scheme);

            var message = $@"
                    <!DOCTYPE html>
                    <html lang='tr'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Hesap Silme İsteği</title>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                color: #333;
                                line-height: 1.6;
                            }}
                            .container {{
                                max-width: 600px;
                                margin: 20px auto;
                                background-color: #fff;
                                padding: 20px;
                                border-radius: 8px;
                                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                            }}
                            h1 {{
                                color: #444;
                                font-size: 24px;
                                text-align: center;
                                margin-bottom: 20px;
                            }}
                            p {{
                                font-size: 16px;
                                margin-bottom: 20px;
                            }}
                            a.button {{
                                display: inline-block;
                                background-color: #007bff;
                                color: #fff;
                                padding: 10px 20px;
                                text-decoration: none;
                                border-radius: 5px;
                                font-weight: bold;
                                text-align: center;
                            }}
                            a.button:hover {{
                                background-color: #0056b3;
                            }}
                            .footer {{
                                margin-top: 20px;
                                text-align: center;
                                font-size: 12px;
                                color: #777;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h1>Hesap Silme İsteği</h1>
                            <p>Sayın {User.Identity.Name}, hesabınızı silmek için aşağıdaki butona tıklayın. </p>
                            <p>Sistemde kayıtlı olan bir evcil hayvanınız var ise bilgileri silincektir. Bu işlem geri alınamaz.</p>
                            <p style='text-align:center;'>
                                <a href='{callbackUrl}' class='button'>Hesabımı Sil</a>
                            </p>
                            <p class='footer'>Bu e-posta otomatik olarak gönderilmiştir, lütfen yanıtlamayın.</p>
                        </div>
                    </body>
                    </html>";

            try
            {
                await _emailSender.SendEmailAsync(user.Email, "Hesap Silme Talebi", message);

                TempData["MailGonderildi"] = "Hesabınızı silmek için gerekli bağlantı mail adresinize gönderildi. Mail adresinizde bulunan linke tıklayarak hesabınızı silebilirsiniz.";
                return RedirectToAction("Information"); 
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Hesabınızı silmek için gerekli bağlantı mail adresinize gönderilemedi. Lütfen sistem yöneticisi ile iletişime geçiniz. " + ex.Message;

                return RedirectToAction("Information");
            }

        }

        public async Task<IActionResult> DeleteAccount(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("BadRequest");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.VerifyUserTokenAsync(user, "Default", "DeleteAccount", code);
            if (!result)
            {
                return View("BadRequest");
            }


            // Kullanıcının sahip olduğu hayvan kayıtlarını al
            var kullaniciHayvanKayitlari = _context.SahipHayvans
                .Where(s => s.SahipTckn == user.InsanTckn)
                .ToList();

            // Kullanıcının sahip olduğu hayvanlara ait ID'leri listele
            var kullaniciHayvanIds = kullaniciHayvanKayitlari
                .Select(k => k.HayvanId)
                .ToList();

            // SahipHayvans tablosundan sadece bu kullanıcının kayıtlarını sil
            _context.SahipHayvans.RemoveRange(kullaniciHayvanKayitlari);
            _context.SaveChanges();

            // Sahipsiz kalan hayvanları belirle
            var sahipsizHayvanlar = _context.Hayvans
                .Where(h => !(_context.SahipHayvans
                    .Any(sh => sh.HayvanId == h.HayvanId)))
                .ToList();

            // Hayvans tablosundan sahipsiz hayvanları sil
            _context.Hayvans.RemoveRange(sahipsizHayvanlar);
            _context.SaveChanges();
            

            await _signInManager.SignOutAsync();
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index", "Home");
        }
    }
}
