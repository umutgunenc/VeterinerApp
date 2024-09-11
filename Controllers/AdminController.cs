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
    [Authorize(Roles = "ADMIN")]
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
        public IActionResult RenkSil()
        {
            RenkSilViewModel model = new(_veterinerDbContext);

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

                model = new RenkSilViewModel(_veterinerDbContext);

                return View(model);
            }

            var silinecekRenk = await _veterinerDbContext.Renkler
                .Where(r => r.RenkId == model.RenkId)
                .FirstOrDefaultAsync();

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
        public IActionResult CinsSil()
        {
            CinsSilViewModel model = new(_veterinerDbContext);

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

                model = new CinsSilViewModel(_veterinerDbContext);

                return View(model);
            }
            var silinecekCins = await _veterinerDbContext.Cinsler
                .Where(x => x.CinsId == model.CinsId)
                .FirstOrDefaultAsync();

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
        public IActionResult TurSil()
        {

            TurSilViewModel model = new(_veterinerDbContext);

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

                model = new TurSilViewModel(_veterinerDbContext);

                return View(model);

            }
            var silinecekTur = await _veterinerDbContext.Turler
                .Where(x => x.TurId == model.TurId)
                .FirstOrDefaultAsync();

            _veterinerDbContext.Turler.Remove(silinecekTur);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["TurSilindi"] = $"{silinecekTur.TurAdi} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult CinsTur()
        {
            var model = new CinsTurEkleViewModel(_veterinerDbContext);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CinsTur(CinsTurEkleViewModel model)
        {
            TurCinsEkleValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model = new CinsTurEkleViewModel(_veterinerDbContext);

                return View(model);
            }


            await _veterinerDbContext.CinsTur.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            model.Cins = await _veterinerDbContext.Cinsler.FirstOrDefaultAsync(x => x.CinsId == model.CinsId);
            model.Tur = await _veterinerDbContext.Turler.FirstOrDefaultAsync(x => x.TurId == model.TurId);

            TempData["CinsTurEklendi"] = $"{model.Cins.CinsAdi} cinsi ve {model.Tur.TurAdi} türü eşleştirildi";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> CinsTurSil()
        {
            var model = new CinsTurSilViewModel(_veterinerDbContext);
            var umut = "";

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> CinsTurSil(CinsTurSilViewModel model)
        {

            TurCinsSilValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                model = new CinsTurSilViewModel(_veterinerDbContext);

                return View(model);
            }

            var sininecekCinsTur = await _veterinerDbContext.CinsTur.FirstOrDefaultAsync(ct => ct.Id == model.Id);

            var silinecekCins = await _veterinerDbContext.Cinsler
                .Where(c => c.CinsId ==
                            _veterinerDbContext.CinsTur
                                .Where(ct => ct.Id == sininecekCinsTur.Id)
                                .Select(ct => ct.CinsId)
                                .FirstOrDefault())

                .FirstOrDefaultAsync();

            var silinecekTur = await _veterinerDbContext.Turler
                .Where(c => c.TurId ==
                            _veterinerDbContext.CinsTur
                                .Where(ct => ct.Id == sininecekCinsTur.Id)
                                .Select(ct => ct.TurId)
                                .FirstOrDefault())

                .FirstOrDefaultAsync();


            TempData["EslemeKaldiridi"] = $"{silinecekCins.CinsAdi} cinsi ve {silinecekTur.TurAdi} türü arasındaki eşleştirme kaldırıldı.";
            _veterinerDbContext.CinsTur.Remove(sininecekCinsTur);
            await _veterinerDbContext.SaveChangesAsync();




            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult RolEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RolEkle(RolEkleViewModel model)
        {
            var rolEntity = new AppRole { Name = model.Name.ToUpper() };

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

            _veterinerDbContext.Roles.Add(rolEntity);
            _veterinerDbContext.SaveChanges();
            TempData["RolEklendi"] = $"Çalışanlar için {model.Name.ToUpper()} türünde bir rol eklendi";
            return RedirectToAction();

        }

        [HttpGet]
        public IActionResult RolSil()
        {
            var model = new RolSilViewModel
            {
                Roller = _veterinerDbContext.Roles.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult RolSil(RolSilViewModel model)
        {

            var rolEntity = _veterinerDbContext.Roles.FirstOrDefault(x => x.Id == model.Id);

            RolSilValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);
                }

                model = new RolSilViewModel
                {
                    Roller = _veterinerDbContext.Roles.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList()
                };
                return View(model);

            }
            _veterinerDbContext.Roles.Remove(rolEntity);
            _veterinerDbContext.SaveChanges();

            TempData["RolSilindi"] = $"{rolEntity.Name} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult CalisanEkle()
        {
            var model = new InsanEkleViewModel
            {
                Roller = _veterinerDbContext.Roles.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList()
            };
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> CalisanEkle(InsanEkleViewModel model)
        {
            kullaniciAdi username = new kullaniciAdi(_veterinerDbContext);
            string kullaniciAdi = username.GenerateUsername(model.InsanAdi, model.InsanSoyadi, model.Email).ToUpper();

            model.UserName = kullaniciAdi;
            model.CalisiyorMu = true;

            sifre sifre = new sifre();
            string kullaniciSifresi = sifre.GeneratePassword(8);

            var calisan = new AppUser
            {
                InsanAdi = model.InsanAdi.ToUpper(),
                InsanSoyadi = model.InsanSoyadi.ToUpper(),
                InsanTckn = model.InsanTckn,
                Email = model.Email.ToLower(),
                PhoneNumber = model.PhoneNumber,
                DiplomaNo = model.DiplomaNo,
                UserName = kullaniciAdi,
                CalisiyorMu = true,
                SifreOlusturmaTarihi = DateTime.Now,
                SifreGecerlilikTarihi = DateTime.Now.AddDays(120),
                TermOfUse = true,
            };



            InsanEkleValidators validator = new InsanEkleValidators();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model = new InsanEkleViewModel
                {
                    Roller = _veterinerDbContext.Roles.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList(),
                };
                return View(model);
            }

            var createResult = await _userManager.CreateAsync(calisan, kullaniciSifresi);
            _veterinerDbContext.SaveChanges(); // Önce kullanıcıyı kaydet

            if (!createResult.Succeeded)
            {
                foreach (var error in createResult.Errors)
                {
                    ModelState.AddModelError("error", error.Description);
                }
                return View(model);
            }
            var insanRol = new IdentityUserRole<int>()
            {
                UserId = calisan.Id,
                RoleId = model.rolId
            };

            _veterinerDbContext.UserRoles.Add(insanRol);

            if (_veterinerDbContext.SaveChanges() > 0)
            {
                var loginUrl = Url.Action("Login", "Account", null, Request.Scheme);
                var kullaniciAdSoyad = model.InsanAdi.ToUpper() + " " + model.InsanSoyadi.ToUpper();
                var tarih = DateTime.Now.ToString("HH:mm dd/MM/yyyy");
                var rolAdi = _veterinerDbContext.Roles
                    .Where(x => x.Id == model.rolId)
                    .Select(x => x.Name)
                    .FirstOrDefault();

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
                                <p><strong>Kullanıcı Adı:</strong> {kullaniciAdi}</p>
                                <p><strong>Şifre:</strong> {sifre}</p>
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
                    _veterinerDbContext.Users.Remove(calisan);
                    _veterinerDbContext.SaveChanges();
                    model = new InsanEkleViewModel
                    {
                        Roller = _veterinerDbContext.Roles.Select(r => new SelectListItem
                        {
                            Value = r.Id.ToString(),
                            Text = r.Name
                        }).ToList(),

                    };
                    return View(model);
                }

            }
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult CalisanSec()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CalisanSec(InsanSecViewModel model)
        {
            var secilenKisi = _veterinerDbContext.Users
                .Where(x => x.InsanTckn == model.InsanTckn)
                .Select(x => new InsanDuzenleViewModel
                {
                    Id = x.Id,
                    InsanAdi = x.InsanAdi,
                    InsanSoyadi = x.InsanSoyadi,
                    InsanTckn = x.InsanTckn,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    DiplomaNo = x.DiplomaNo,
                    UserName = x.UserName,
                    CalisiyorMu = x.CalisiyorMu,
                    Maas = x.Maas,
                    rolId = _veterinerDbContext.UserRoles.Where(r => r.UserId == x.Id)
                        .Select(r => r.RoleId)
                        .FirstOrDefault(),
                    Roller = _veterinerDbContext.Roles.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList(),
                    rolAdi = _veterinerDbContext.Roles
                        .Where(r => r.Id == _veterinerDbContext.UserRoles
                                            .Where(r => r.UserId == x.Id)
                                            .Select(r => r.RoleId)
                                            .FirstOrDefault())
                        .Select(r => r.Name)
                        .FirstOrDefault()
                })
                .FirstOrDefault();


            InsanSecValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }

            ViewBag.SecilenKisi = secilenKisi;
            return View(model);
        }
        [HttpPost]
        public IActionResult CalisanDuzenle(InsanDuzenleViewModel model)
        {
            var insan = _veterinerDbContext.Users.FirstOrDefault(x => x.InsanTckn == model.InsanTckn);
            var rol = _veterinerDbContext.UserRoles.FirstOrDefault(x => x.UserId == model.Id);

            InsanDuzenleValidators validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                var insanSec = new InsanSecViewModel() { InsanTckn = model.InsanTckn };
                var secilenKisi = _veterinerDbContext.Users
                .Where(x => x.InsanTckn == insanSec.InsanTckn)
                .Select(x => new InsanDuzenleViewModel
                {
                    Id = x.Id,
                    InsanAdi = x.InsanAdi,
                    InsanSoyadi = x.InsanSoyadi,
                    InsanTckn = x.InsanTckn,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    DiplomaNo = x.DiplomaNo,
                    UserName = x.UserName,
                    CalisiyorMu = x.CalisiyorMu,
                    Maas = x.Maas,
                    rolId = _veterinerDbContext.UserRoles.Where(r => r.UserId == x.Id)
                        .Select(r => r.RoleId)
                        .FirstOrDefault(),
                    Roller = _veterinerDbContext.Roles.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList(),
                    rolAdi = _veterinerDbContext.Roles
                        .Where(r => r.Id == _veterinerDbContext.UserRoles
                                            .Where(r => r.UserId == x.Id)
                                            .Select(r => r.RoleId)
                                            .FirstOrDefault())
                        .Select(r => r.Name)
                        .FirstOrDefault()
                }).FirstOrDefault();

                ViewBag.SecilenKisi = secilenKisi;
                return View("CalisanSec");
            }

            // Mevcut kullanıcı rolünü sil
            _veterinerDbContext.UserRoles.Remove(rol);
            _veterinerDbContext.SaveChanges();

            // Yeni kullanıcı rolünü ekle
            var yeniRol = new IdentityUserRole<int>()
            {
                RoleId = model.rolId,
                UserId = insan.Id
            };
            _veterinerDbContext.UserRoles.Add(yeniRol);

            // Kullanıcı bilgilerini güncelle
            insan.InsanAdi = model.InsanAdi;
            insan.InsanSoyadi = model.InsanSoyadi;
            insan.Email = model.Email.ToLower();
            insan.PhoneNumber = model.PhoneNumber;
            insan.DiplomaNo = model.DiplomaNo;
            insan.UserName = model.UserName;
            insan.CalisiyorMu = model.CalisiyorMu;
            insan.Maas = model.Maas;

            _veterinerDbContext.SaveChanges();

            _veterinerDbContext.SaveChanges();
            TempData["KisiGuncellendi"] = $"{insan.InsanAdi} {insan.InsanSoyadi} adlı kişinin bilgileri başarı ile güncellendi.";
            return View("CalisanSec");
        }

        [HttpGet]
        public IActionResult CalisanListele(int sayfaNumarasi = 1)
        {
            int sayfaBoyutu = 4;
            var toplamKayit = _veterinerDbContext.Users.Count();
            var calisanlar = _veterinerDbContext.Users.Select(insan => new CalisanListeleViewModel
            {
                InsanTckn = insan.InsanTckn,
                InsanAdi = insan.InsanAdi,
                InsanSoyadi = insan.InsanSoyadi,
                PhoneNumber = insan.PhoneNumber,
                Email = insan.Email,
                RolId = _veterinerDbContext.UserRoles
                    .Where(rol => rol.UserId == insan.Id)
                    .Select(rol => rol.RoleId)
                    .FirstOrDefault(),
                DiplomaNo = insan.DiplomaNo,
                CalisiyorMu = insan.CalisiyorMu,
                Maas = insan.Maas,
                UserName = insan.UserName,
                RolAdi = _veterinerDbContext.Roles
                    .Where(rol => rol.Id == _veterinerDbContext.UserRoles
                                            .Where(rol => rol.UserId == insan.Id)
                                            .Select(rol => rol.RoleId)
                                            .FirstOrDefault())
                    .Select(rol => rol.Name)
                    .FirstOrDefault()
            });

            var viewModel = SayfalamaListesi<CalisanListeleViewModel>.Olustur(calisanlar, sayfaNumarasi, sayfaBoyutu);
            ViewBag.ToplamKayit = toplamKayit;
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CalisanListele(string secilenKisiTckn, int sayfaNumarasi = 1)
        {
            int sayfaBoyutu = 4;
            var toplamKayit = _veterinerDbContext.AppUsers.Count();
            var calisanlar = _veterinerDbContext.Users.Select(insan => new CalisanListeleViewModel
            {

                InsanTckn = insan.InsanTckn,
                InsanAdi = insan.InsanAdi,
                InsanSoyadi = insan.InsanSoyadi,
                PhoneNumber = insan.PhoneNumber,
                Email = insan.Email,
                RolId = _veterinerDbContext.UserRoles
                    .Where(rol => rol.UserId == insan.Id)
                    .Select(rol => rol.RoleId)
                    .FirstOrDefault(),
                DiplomaNo = insan.DiplomaNo,
                CalisiyorMu = insan.CalisiyorMu,
                Maas = insan.Maas,
                UserName = insan.UserName,
                RolAdi = _veterinerDbContext.Roles
                    .Where(rol => rol.Id == _veterinerDbContext.UserRoles
                                            .Where(rol => rol.UserId == insan.Id)
                                            .Select(rol => rol.RoleId)
                                            .FirstOrDefault())
                    .Select(rol => rol.Name)
                    .FirstOrDefault()
            });

            var viewModel = SayfalamaListesi<CalisanListeleViewModel>.Olustur(calisanlar, sayfaNumarasi, sayfaBoyutu);

            var secilenKisi = await _veterinerDbContext.Users
                .Where(insan => insan.InsanTckn == secilenKisiTckn)
                .Select(insan => new CalisanListeleViewModel
                {

                    InsanAdi = insan.InsanAdi,
                    InsanSoyadi = insan.InsanSoyadi,
                    InsanTckn = insan.InsanTckn,
                    Email = insan.Email.ToLower(),
                    PhoneNumber = insan.PhoneNumber,
                    DiplomaNo = insan.DiplomaNo,
                    UserName = insan.UserName,
                    CalisiyorMu = insan.CalisiyorMu,
                    Maas = insan.Maas,
                    RolId = _veterinerDbContext.UserRoles
                        .Where(rol => rol.UserId == insan.Id)
                        .Select(rol => rol.RoleId)
                        .FirstOrDefault(),
                    RolAdi = _veterinerDbContext.Roles
                        .Where(rol => rol.Id == _veterinerDbContext.UserRoles
                                                .Where(rol => rol.UserId == insan.Id)
                                                .Select(rol => rol.RoleId)
                                                .FirstOrDefault())
                        .Select(rol => rol.Name)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            if (secilenKisi == null)
            {
                return View("BadRequest");
            }

            ViewBag.SecilenKisi = secilenKisi;
            ViewBag.ToplamKayit = toplamKayit;

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

