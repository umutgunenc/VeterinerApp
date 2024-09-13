using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using VeterinerApp.Fonksiyonlar;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinerApp.Models.ViewModel.Admin;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using VeterinerApp.Models.Validators.Admin;
using Microsoft.AspNetCore.Identity.UI.Services;



namespace VeterinerApp.Controllers
{
    [Authorize(Roles = "ADMIN,ADMİN,admin,admın")]
    public class AdminController : Controller
    {
        private readonly VeterinerDBContext _veterinerDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AdminController(VeterinerDBContext veterinerDbContext, UserManager<AppUser> userManager, IEmailSender emailSender)
        {
            _veterinerDbContext = veterinerDbContext;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult AdminIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RenkEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RenkEkle(RenkEkleViewModel model)
        {
            model.RenkAdi = model.RenkAdi.ToUpper();

            RenkEkleValidators renkvalidator = new();
            ValidationResult result = renkvalidator.Validate(model);


            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }
            await _veterinerDbContext.Renkler.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["Success"] = $"{model.RenkAdi} rengi eklendi";

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RenkSil()
        {
            RenkSilViewModel model = new();

            return View(await model.RenklerListesiniGetirAsync(_veterinerDbContext));
        }
        [HttpPost]
        public async Task<IActionResult> RenkSil(RenkSilViewModel model)
        {
            RenkSilValidator validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);
                }


                return View(await model.RenklerListesiniGetirAsync(_veterinerDbContext));
            }
            var silinecekRenk = await model.SilinecekRengiGetirAsync(model, _veterinerDbContext);
            _veterinerDbContext.Renkler.Remove(silinecekRenk);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["RenkSilindi"] = $"{silinecekRenk.RenkAdi} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult CinsEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CinsEkle(CinsEkleViewModel model)
        {

            model.CinsAdi = model.CinsAdi.ToUpper();

            CinsEkleValidators validator = new();
            ValidationResult result = validator.Validate(model);


            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }

            await _veterinerDbContext.Cinsler.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["CinsEklendi"] = $"{model.CinsAdi} cinsi eklendi";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> CinsSil()
        {
            CinsSilViewModel model = new();

            return View(await model.CinslerListesiGetirAsync(_veterinerDbContext));
        }
        [HttpPost]
        public async Task<IActionResult> CinsSil(CinsSilViewModel model)
        {

            CinsSilValidator validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);
                }


                return View(await model.CinslerListesiGetirAsync(_veterinerDbContext));
            }

            var silinecekCins = await model.SilinecekCinsiGetir(model, _veterinerDbContext);

            _veterinerDbContext.Cinsler.Remove(silinecekCins);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["CinsSilindi"] = $"{silinecekCins.CinsAdi} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult TurEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TurEkle(TurEKleViewModel model)
        {
            model.TurAdi = model.TurAdi.ToUpper();

            TurEkleValidators validator = new TurEkleValidators();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }
            await _veterinerDbContext.Turler.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["TurEklendi"] = $"{model.TurAdi} türü eklendi";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> TurSil()
        {

            TurSilViewModel model = new();

            return View(await model.TurListesiniGetirASync(_veterinerDbContext));

        }
        [HttpPost]
        public async Task<IActionResult> TurSil(TurSilViewModel model)
        {
            TurSilValidator validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);
                }

                return View(await model.TurListesiniGetirASync(_veterinerDbContext));

            }
            var silinecekTur = await model.SilinecekTuruGetirAsync(model, _veterinerDbContext);

            _veterinerDbContext.Turler.Remove(silinecekTur);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["TurSilindi"] = $"{silinecekTur.TurAdi} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> CinsTurEslestir()
        {
            var model = new CinsTurEslestirViewModel();
            return View(await model.CinsTurListesiGetirAsync(_veterinerDbContext));
        }
        [HttpPost]
        public async Task<IActionResult> CinsTurEslestir(CinsTurEslestirViewModel model)
        {
            TurCinsEkleValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View(await model.CinsTurListesiGetirAsync(_veterinerDbContext));
            }


            await _veterinerDbContext.CinsTur.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            model.Cins = await _veterinerDbContext.Cinsler.FirstOrDefaultAsync(x => x.CinsId == model.CinsId);
            model.Tur = await _veterinerDbContext.Turler.FirstOrDefaultAsync(x => x.TurId == model.TurId);

            TempData["CinsTurEklendi"] = $"{model.Cins.CinsAdi} cinsi ve {model.Tur.TurAdi} türü eşleştirildi";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> CinsTurEslesmeKaldir()
        {
            var model = new CinsTurEslesmeKaldirViewModel();

            return View(await model.CinslerTurlerListesiniGetirAsync(_veterinerDbContext));

        }
        [HttpPost]
        public async Task<IActionResult> CinsTurEslesmeKaldir(CinsTurEslesmeKaldirViewModel model)
        {

            TurCinsSilValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View(await model.CinslerTurlerListesiniGetirAsync(_veterinerDbContext));
            }

            var eslesmesiKaldirilacakCinsTur = await model.EslesmesiKaldirilacakCinsTuruGetirAsync(model, _veterinerDbContext);

            var cinsAdi = await _veterinerDbContext.Cinsler
                .Where(c => c.CinsId ==
                            _veterinerDbContext.CinsTur
                                .Where(ct => ct.Id == eslesmesiKaldirilacakCinsTur.Id)
                                .Select(ct => ct.CinsId)
                                .FirstOrDefault())
                .Select(c => c.CinsAdi)
                .FirstOrDefaultAsync();

            var turAdi = await _veterinerDbContext.Turler
                .Where(c => c.TurId ==
                            _veterinerDbContext.CinsTur
                                .Where(ct => ct.Id == eslesmesiKaldirilacakCinsTur.Id)
                                .Select(ct => ct.TurId)
                                .FirstOrDefault())
                .Select(t => t.TurAdi)
                .FirstOrDefaultAsync();


            _veterinerDbContext.CinsTur.Remove(eslesmesiKaldirilacakCinsTur);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["EslemeKaldiridi"] = $"{cinsAdi} cinsi ve {turAdi} türü arasındaki eşleştirme kaldırıldı.";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult RolEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RolEkle(RolEkleViewModel model)
        {
            model.Name = model.Name.ToUpper();

            RolValidators rolValidator = new RolValidators();
            ValidationResult result = rolValidator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }

            await _veterinerDbContext.Roles.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["RolEklendi"] = $"{model.Name.ToUpper()} türünde bir rol başarı ile eklendi eklendi";
            return RedirectToAction();

        }

        [HttpGet]
        public async Task<IActionResult> RolSil()
        {
            var model = new RolSilViewModel();

            return View(await model.RollerListesiniGetir(_veterinerDbContext));
        }
        [HttpPost]
        public async Task<IActionResult> RolSil(RolSilViewModel model)
        {

            RolSilValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);
                }

                return View(model.RollerListesiniGetir(_veterinerDbContext));

            }
            var silinecekRol = await model.SilinecekRoluGetir(_veterinerDbContext,model);
            _veterinerDbContext.Roles.Remove(await model.SilinecekRoluGetir(_veterinerDbContext, model));
            await _veterinerDbContext.SaveChangesAsync();

            TempData["RolSilindi"] = $"{silinecekRol.Name} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> KisiEkle()
        {
            var model = new KisiEkleViewModel();

            return View(await model.RollerListesiniGetirAsync(_veterinerDbContext));

        }
        [HttpPost]
        public async Task<IActionResult> KisiEkle(KisiEkleViewModel model)
        {
            var kisi = await model.KisiOlusturAsync(_veterinerDbContext, model);

            KisiEkleValidators validator = new KisiEkleValidators();
            ValidationResult result = validator.Validate(kisi);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View(await kisi.RollerListesiniGetirAsync(_veterinerDbContext));
            }

            var createResult = await _userManager.CreateAsync(kisi, kisi.KullaniciSifresi);
            await _veterinerDbContext.SaveChangesAsync(); // Kullaniciya rol atayabilmek için kullanıcıyı veritabanına kaydetmemiz gerekiyor.

            if (!createResult.Succeeded)
            {
                foreach (var error in createResult.Errors)
                {
                    ModelState.AddModelError("Error", error.Description);
                }
                return View(await kisi.RollerListesiniGetirAsync(_veterinerDbContext));
            }


            await _veterinerDbContext.UserRoles.AddAsync(await kisi.KisininRolunuGetirAsync(_veterinerDbContext, kisi));

            if (await _veterinerDbContext.SaveChangesAsync() > 0)
            {
                var loginUrl = Url.Action("Login", "Account", null, Request.Scheme);
                var kullaniciAdSoyad = model.InsanAdi.ToUpper() + " " + model.InsanSoyadi.ToUpper();
                var tarih = DateTime.Now.ToString("HH:mm dd/MM/yyyy");
                var rolAdi = await _veterinerDbContext.Roles
                    .Where(x => x.Id == model.RolId)
                    .Select(x => x.Name)
                    .FirstOrDefaultAsync();

                string mailMessage = $@"
                <!DOCTYPE html>
                <html lang='tr'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Hoş Geldiniz!</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            color: #333;
                            margin: 0;
                            padding: 0;
                            font-size: 1.2rem;
                            background-color: #f8f9fa;
                        }}
                        h1 {{
                            font-size: 1.8rem;
                            text-align: center;
                            margin-bottom: 20px;
                            color: #fff;
                        }}
                        .container {{
                            padding: 20px;
                            max-width: 600px;
                            margin: auto;
                            background-color: #ffffff;
                            border-radius: 8px;
                            box-shadow: 0px 0px 10px rgba(0,0,0,0.1);
                        }}
                        .header {{
                            background-color: #343a40;
                            color: white;
                            padding: 10px;
                            border-radius: 8px 8px 0 0;
                            text-align: center;
                        }}
                        .content {{
                            padding: 20px;
                        }}
                        .credentials {{
                            background-color: #f9f9f9;
                            border-left: 4px solid #007bff;
                            padding: 10px;
                            margin-bottom: 20px;
                            font-size: 1.2rem;
                        }}
                        a.button {{
                            display: inline-block;
                            background-color: #007bff;
                            color: white !important;
                            padding: 10px 20px;
                            text-decoration: none;
                            border-radius: 5px;
                            font-weight: bold;
                            text-align: center;
                            margin-top: 10px;
                            background-color: #6c757d;
                        }}
                        a.button:hover {{
                            background-color: #5a6268;
                        }}
                        .footer {{
                            margin-top: 20px;
                            text-align: center;
                            font-size: 12px;
                            color: #777;
                            border-top: 1px solid #e9ecef;
                            padding-top: 20px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Hoş Geldiniz!</h1>
                        </div>
                        <div class='content'>
                            <p>Sayın {kullaniciAdSoyad}, {tarih} tarihinde sisteme {rolAdi} görevinde başarıyla üye oldunuz. Aşağıda giriş bilgileriniz yer almaktadır:</p>
                            <div class='credentials'>
                                <p><strong>Kullanıcı Adı:</strong> {model.UserName}</p>
                                <p><strong>Şifre:</strong> {model.KullaniciSifresi}</p>
                            </div>
                            <p style='text-align:center;'>
                                <a href='{loginUrl}' class='button'>Giriş Yap</a>
                            </p>
                        </div>
                        <div class='footer'>
                            <p>Bu e-posta otomatik olarak gönderilmiştir, lütfen yanıtlamayın.</p>
                        </div>
                    </div>
                </body>
                </html>";

                try
                {
                    await _emailSender.SendEmailAsync(model.Email, "Veteriner Bilgi Sistemi'ne Hoş Geldiniz!", mailMessage);
                    TempData["CalısanEklendi"] = $"{model.InsanAdi.ToUpper()} {model.InsanSoyadi.ToUpper()} isimli calışan {rolAdi.ToUpper()} görevi ile sisteme kaydedildi. Kullanıcı adı ve şifresi {model.Email.ToUpper()} adresine gönderildi.";
                    return RedirectToAction();

                }
                catch (Exception ex)
                {

                    ViewBag.Hata = "Mail Gönderme işlemi başarısız oldu. Kayıt işlemi tamamlanamadı." + " " + ex.Message;
                    _veterinerDbContext.Users.Remove(kisi);
                    await _veterinerDbContext.SaveChangesAsync();

                    return View(await kisi.RollerListesiniGetirAsync(_veterinerDbContext));
                }

            }
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult KisiSec()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KisiSec(KisiSecViewModel model)
        {

            KisiSecValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }

            var secilenKisi = new KisiDuzenleViewModel(_veterinerDbContext, model);

            //View Component'ta kullanmak secilen kişinin bilgilerini, Viewbag'e atıyoruz.
            //View Component'ta ViewBag'den alıp kullanacağız.
            ViewBag.SecilenKisi = secilenKisi;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> KisiDuzenle(KisiDuzenleViewModel model)
        {


            KisiDuzenleValidators validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                KisiSecViewModel kisiSecViewModel = new();
                kisiSecViewModel.InsanTckn = model.InsanTckn;

                model = new(_veterinerDbContext, kisiSecViewModel);

                ViewBag.SecilenKisi = model;
                return View("KisiSec");
            }
            var kisiRolu = await model.UserRoleGetirAsync(_veterinerDbContext, model);
            var kisi = await model.UpdateOlacakKullaniciyiGetirAsync(_veterinerDbContext, model);
            _veterinerDbContext.UserRoles.Update(kisiRolu);
            _veterinerDbContext.Users.Update(kisi);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["KisiGuncellendi"] = $"{model.InsanAdi} {model.InsanSoyadi} adlı kişinin bilgileri başarı ile güncellendi.";
            return View("KisiSec");
        }

        [HttpGet]
        public IActionResult KisileriListele(int sayfaNumarasi = 1)
        {
            int sayfadaGosterilecekKayitSayisi = 4;

            var kisiler = new KisileriListeleViewModel(_veterinerDbContext);

            var viewModel = SayfalamaListesi<KisileriListeleViewModel>.Olustur(kisiler.KisiListesiniGetir(), sayfaNumarasi, sayfadaGosterilecekKayitSayisi);
            ViewBag.ToplamKayit = kisiler;
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> KisileriListele(string secilenKisiTckn, int sayfaNumarasi = 1)
        {
            int sayfadaGosterilecekKayitSayisi = 4;
            var kisiler = new KisileriListeleViewModel(_veterinerDbContext);


            var secilenKisi = kisiler.SecilenKisi(secilenKisiTckn);

            if (secilenKisi == null)
            {
                return View("BadRequest");
            }

            ViewBag.SecilenKisi = secilenKisi;


            var viewModel = SayfalamaListesi<KisileriListeleViewModel>.Olustur(kisiler.KisiListesiniGetir(), sayfaNumarasi, sayfadaGosterilecekKayitSayisi);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult KategoriEkle(KategoriViewModel model)
        {
            KategoriEkleValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(model);
            }
            model.KategoriAdi = model.KategoriAdi.ToUpper();
            _veterinerDbContext.Kategoriler.Add(model);
            if (_veterinerDbContext.SaveChanges() > 0)
            {
                TempData["KategoriEklendi"] = $"{model.KategoriAdi.ToUpper()} kategorisi başarı ile eklendi.";
            }

            return View();
        }

    }
}

