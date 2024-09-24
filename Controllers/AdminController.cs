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
using VeterinerApp.Models.Validators;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;



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
            model.RenkAdi = model.RenkAdi.ToUpper();

            await _veterinerDbContext.Renkler.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["Success"] = $"{model.RenkAdi} rengi eklendi";

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RenkSil()
        {
            RenkSilViewModel model = new();
            model.RenklerListesi = await model.RenklerListesiniGetirAsync(_veterinerDbContext);

            return View(model);
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
                model.RenklerListesi = await model.RenklerListesiniGetirAsync(_veterinerDbContext);

                return View(model);
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
            model.CinsAdi = model.CinsAdi.ToUpper();

            await _veterinerDbContext.Cinsler.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["CinsEklendi"] = $"{model.CinsAdi} cinsi eklendi";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> CinsSil()
        {
            CinsSilViewModel model = new();
            model.CinslerListesi = await model.CinslerListesiGetirAsync(_veterinerDbContext);

            return View(model);
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

                model.CinslerListesi = await model.CinslerListesiGetirAsync(_veterinerDbContext);
                return View(model);
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
            model.TurAdi = model.TurAdi.ToUpper();

            await _veterinerDbContext.Turler.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["TurEklendi"] = $"{model.TurAdi} türü eklendi";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> TurSil()
        {

            TurSilViewModel model = new();
            model.TurListesi = await model.TurListesiniGetirASync(_veterinerDbContext);

            return View(model);

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
                model.TurListesi = await model.TurListesiniGetirASync(_veterinerDbContext);
                return View(model);

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

            model.TurlerListesi = await model.TurlerListesiGetirAsync(_veterinerDbContext);
            model.CinslerListesi = await model.CinslerListesiGetirAsync(_veterinerDbContext);

            return View(model);
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

                model.TurlerListesi = await model.TurlerListesiGetirAsync(_veterinerDbContext);
                model.CinslerListesi = await model.CinslerListesiGetirAsync(_veterinerDbContext);

                return View(model);
            }


            await _veterinerDbContext.CinsTur.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            model.Cins = await model.SecilenCinsiGetirAsync(_veterinerDbContext, model);
            model.Tur = await model.SecilenTuruGetirAsync(_veterinerDbContext, model);

            TempData["CinsTurEklendi"] = $"{model.Cins.CinsAdi} cinsi ve {model.Tur.TurAdi} türü eşleştirildi";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> CinsTurEslesmeKaldir()
        {
            var model = new CinsTurEslesmeKaldirViewModel();
            model.CinslerTurlerListesi = await model.CinslerTurlerListesiniGetirAsync(_veterinerDbContext);

            return View(model);

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

                model.CinslerTurlerListesi = await model.CinslerTurlerListesiniGetirAsync(_veterinerDbContext);

                return View(model);
            }

            model.EslemesiKaldiralacakCinstur = await model.EslesmesiKaldirilacakCinsTuruGetirAsync(model, _veterinerDbContext);

            var cinsAdi = await model.EslesmesiKaldirilacakCinsAdiniGetirAsync(model, _veterinerDbContext);
            var turAdi = await model.EslesmesiKaldirilacakTurAdiniGetirAsync(model, _veterinerDbContext);


            _veterinerDbContext.CinsTur.Remove(model.EslemesiKaldiralacakCinstur);
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
            model.Name = model.Name.ToUpper();

            await _veterinerDbContext.Roles.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["RolEklendi"] = $"{model.Name.ToUpper()} türünde bir rol başarı ile eklendi eklendi";
            return RedirectToAction();

        }

        [HttpGet]
        public async Task<IActionResult> RolSil()
        {
            var model = new RolSilViewModel();

            model.RollerListesi = await model.RollerListesiniGetir(_veterinerDbContext);

            return View(model);
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

                model.RollerListesi = await model.RollerListesiniGetir(_veterinerDbContext);

                return View(model);
            }
            model.SilinecekRol = await model.SilinecekRoluGetir(_veterinerDbContext, model);
            _veterinerDbContext.Roles.Remove(model.SilinecekRol);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["RolSilindi"] = $"{model.SilinecekRol.Name} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> KisiEkle()
        {
            var model = new KisiEkleViewModel();
            model.RollerListesi = await model.RollerListesiniGetirAsync(_veterinerDbContext);

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> KisiEkle(KisiEkleViewModel model)
        {
            model.Kullanici = await model.KisiOlusturAsync(_veterinerDbContext, model);

            KisiEkleValidators validator = new KisiEkleValidators();
            ValidationResult result = validator.Validate(model.Kullanici);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model.RollerListesi = await model.RollerListesiniGetirAsync(_veterinerDbContext);

                return View(model);
            }

            var createResult = await _userManager.CreateAsync(model.Kullanici, model.KullaniciSifresi);
            await _veterinerDbContext.SaveChangesAsync(); // Kullaniciya rol atayabilmek için kullanıcıyı veritabanına kaydetmemiz gerekiyor.

            if (!createResult.Succeeded)
            {
                foreach (var error in createResult.Errors)
                {
                    ModelState.AddModelError("Error", error.Description);
                }
                model.RollerListesi = await model.RollerListesiniGetirAsync(_veterinerDbContext);

                return View(model);
            }
            model.KullaniciRolu = await model.KisininRolunuGetirAsync(_veterinerDbContext, model);

            await _veterinerDbContext.UserRoles.AddAsync(model.KullaniciRolu);

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

                    _veterinerDbContext.Users.Remove(model.Kullanici);
                    await _veterinerDbContext.SaveChangesAsync();
                    model.RollerListesi = await model.RollerListesiniGetirAsync(_veterinerDbContext);
                    return View(model);
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

            KisiDuzenleViewModel kisiDuzenleViewModel = new();
            kisiDuzenleViewModel.SecilenKisi = await kisiDuzenleViewModel.SecilenKisiyiGetirAsync(_veterinerDbContext, model);
            kisiDuzenleViewModel.Signature = Signature.CreateSignature(kisiDuzenleViewModel.Id, kisiDuzenleViewModel.InsanTckn);

            ViewBag.model = kisiDuzenleViewModel;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> KisiDuzenle(KisiDuzenleViewModel model)
        {
            if (!Signature.VerifySignature(model.Id, model.InsanTckn, model.Signature))
                return View("BadRequest");

            KisiDuzenleValidators validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model.RollerListesi = await model.RollerListesiniGetirAsync(_veterinerDbContext);
                ViewBag.model = model;
                return View("KisiSec");
            }

            model.EskiRol = await model.KullanicininEskiRolunuGetirAsync(_veterinerDbContext, model);
            model.YeniRol = model.KullanicininYeniRolunuGetir(model);


            if (model.EskiRol.RoleId != model.YeniRol.RoleId)
            {
                _veterinerDbContext.UserRoles.Remove(model.EskiRol);
                await _veterinerDbContext.UserRoles.AddAsync(model.YeniRol);
            }

            model.UpdateOlacakKullanici = await model.UpdateOlacakKullaniciyiGetirAsync(_veterinerDbContext, model);
            _veterinerDbContext.Users.Update(model.UpdateOlacakKullanici);

            await _veterinerDbContext.SaveChangesAsync();

            TempData["KisiGuncellendi"] = $"{model.InsanAdi} {model.InsanSoyadi} adlı kişinin bilgileri başarı ile güncellendi.";
            return View("KisiSec");
        }

        [HttpGet]
        public IActionResult KisileriListele(int sayfaNumarasi = 1)
        {
            int sayfadaGosterilecekKayitSayisi = 4;

            var kisiler = new KisileriListeleViewModel();

            var viewModel = SayfalamaListesi<KisileriListeleViewModel>.Olustur(kisiler.KisiListesiniGetir(_veterinerDbContext), sayfaNumarasi, sayfadaGosterilecekKayitSayisi);
            ViewBag.ToplamKayit = kisiler;
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> KisileriListele(string secilenKisiTckn, int sayfaNumarasi = 1)
        {
            int sayfadaGosterilecekKayitSayisi = 4;
            KisileriListeleViewModel kisiler = new();

            kisiler.SecilenKisi = await kisiler.SecilenKisiyiGetirAsync(secilenKisiTckn, _veterinerDbContext);


            if (kisiler.SecilenKisi == null)
                return View("BadRequest");


            ViewBag.SecilenKisi = kisiler.SecilenKisi;


            var viewModel = SayfalamaListesi<KisileriListeleViewModel>.Olustur(kisiler.KisiListesiniGetir(_veterinerDbContext), sayfaNumarasi, sayfadaGosterilecekKayitSayisi);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KategoriEkle(KategoriViewModel model)
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

            await _veterinerDbContext.Kategoriler.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["KategoriEklendi"] = $" {model.KategoriAdi} isimli kategori başarı ile eklendi";

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> KategoriSil()
        {
            KategoriSilViewModel model = new();
            model.KategoriListesi = await model.KategoriListesiniGetirAsync(_veterinerDbContext);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> KategoriSil(KategoriSilViewModel model)
        {
            KategoriSilValidator validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model.KategoriListesi = await model.KategoriListesiniGetirAsync(_veterinerDbContext);

                return View(model);
            }

            model.SilinecekKategori = await model.SilinecekKategoriyiGetirAsync(_veterinerDbContext, model);
            _veterinerDbContext.Kategoriler.Remove(model.SilinecekKategori);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["KategoriSilindi"] = $"{model.SilinecekKategori.KategoriAdi} başarı ile silindi.";
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult BirimEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BirimEkle(BirimEkleViewModel model)
        {

            BirimEkleValidator validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }
            model.BirimAdi = model.BirimAdi.ToUpper();

            await _veterinerDbContext.Birimler.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["BirimEklendi"] = $"{model.BirimAdi} isimli birim başarı ile eklendi.";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> BirimSil()
        {
            BirimSilViewModel model = new();
            model.BirimLerListesi = await model.BirimlerListesiniGetirAsync(_veterinerDbContext);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> BirimSil(BirimSilViewModel model)
        {
            BirimSilValidator validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model.BirimLerListesi = await model.BirimlerListesiniGetirAsync(_veterinerDbContext);
                return View();
            }

            model.SilinecekBirim = await model.SilinecekBirimGetirAsync(_veterinerDbContext, model);

            _veterinerDbContext.Birimler.Remove(model.SilinecekBirim);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["BirimSilindi"] = $"{model.SilinecekBirim.BirimAdi} başarı ile silindi.";
            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> StokKartiOlustur()
        {
            StokKartiOlusturViewModel model = new();
            model.BirimlerListesi = await model.BirimListesiniGetirAsync(_veterinerDbContext);
            model.KategoriListesi = await model.KategoriListesiniGetirAsync(_veterinerDbContext);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> StokKartiOlustur(StokKartiOlusturViewModel model)
        {
            StokKartiOlusturValidator validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model.BirimlerListesi = await model.BirimListesiniGetirAsync(_veterinerDbContext);
                model.KategoriListesi = await model.KategoriListesiniGetirAsync(_veterinerDbContext);
                return View(model);
            }

            model.StokAdi = model.StokAdi.ToUpper();
            model.StokBarkod = model.StokBarkod.ToUpper();
            model.AktifMi = true;

            if (string.IsNullOrEmpty(model.Aciklama))
                model.Aciklama = "";
            else
                model.Aciklama = model.Aciklama.ToUpper();

            await _veterinerDbContext.Stoklar.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["StokKartiOlusturuldu"] = $"{model.StokAdi} isimli stok kartı başarı ile oluşturuldu.";
            return RedirectToAction();

        }

        [HttpGet]
        public IActionResult StokGoruntule()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> StokGoruntuleData()
        {
            StokGoruntuleViewModel model = new();
            model.StokListesi = await model.StokListesiniGetirAsync(_veterinerDbContext);
            return Json(model.StokListesi);
        }
        [HttpPost]
        public async Task<IActionResult> StokDetay(string secilenStokId)
        {
            if (string.IsNullOrEmpty(secilenStokId))
                return View("BadRequest");

            if (!int.TryParse(secilenStokId, out int stokId))
                return View("BadRequest");

            if (!await _veterinerDbContext.Stoklar.AnyAsync(s => s.Id == stokId))
                return View("BadRequest");

            ViewBag.StokId = secilenStokId;
            return View();
        }
        [HttpPost]
        public IActionResult StokDetayData(string secilenStokId)
        {
            if (!int.TryParse(secilenStokId, out int stokId))
                return View();
            StokDetayViewModel model = new(_veterinerDbContext, stokId);

            return Json(model);
        }

        [HttpGet]
        public IActionResult StokDuzenle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StokDuzenle(StokDuzenleStokSecViewModel model)
        {
            StokDuzenleStokSecValidator validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View(model);
            }

            StokDuzenleKaydetViewModel stokDetay = new();
            stokDetay = await stokDetay.ModeliOlusturAsync(_veterinerDbContext, model);
            stokDetay.Signature = Signature.CreateSignature(stokDetay.Id, stokDetay.Id.ToString());

            ViewBag.StokModel = stokDetay;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> StokDuzenleKaydet(StokDuzenleKaydetViewModel model)
        {
            if (!Signature.VerifySignature(model.Id, model.Id.ToString(), model.Signature))
            {
                return View("BadRequest");
            }


            StokDuzenleKaydetValidator validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var Error in result.Errors)
                {
                    ModelState.AddModelError("", Error.ErrorMessage);
                }
                model.BirimListesi = await model.BirimListesiniGetirAsync(_veterinerDbContext);
                model.KategoriListesi = await model.KategoriListesiniGetirAsync(_veterinerDbContext);
                ViewBag.StokModel = model;
                return View("StokDuzenle");

            }

            model.StokAdi = model.StokAdi.ToUpper();
            model.Aciklama = model.Aciklama.ToUpper();
            model.StokBarkod = model.StokBarkod.ToUpper();

            _veterinerDbContext.Update(model);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["StokDuzenlendi"] = $"{model.StokAdi} isimli stoğa ait bilgiler başarı ile düzenlendi";

            return View("StokDuzenle");
        }
    }

}

