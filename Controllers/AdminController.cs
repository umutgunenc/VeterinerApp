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
    [Authorize(Roles = "ADMİN")]
    public class AdminController : Controller
    {
        private readonly VeterinerContext _veterinerDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AdminController(VeterinerContext veterinerDbContext, UserManager<AppUser> userManager, IEmailSender emailSender)
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
        public IActionResult RenkEkle(RenkEkleViewModel model)
        {
            string renk = model.renk.ToUpper();
            var renkEntity = new Renk { renk = renk };

            RenkEkleValidators renkvalidator = new RenkEkleValidators();
            ValidationResult result = renkvalidator.Validate(model);


            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }
            _veterinerDbContext.Renks.Add(renkEntity);
            _veterinerDbContext.SaveChanges();
            TempData["Success"] = $"{renk} rengi eklendi";

            return View();
        }

        [HttpGet]
        public IActionResult RenkSil()
        {
            RenkSilViewModel model = new RenkSilViewModel()
            {
                Renkler = _veterinerDbContext.Renks.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.renk,
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult RenkSil(RenkSilViewModel model)
        {

            var silinecekRenkAdı = _veterinerDbContext.Renks
                .Where(x => x.Id == model.Id)
                .Select(x => x.renk).FirstOrDefault();

            var renkEntity = new Renk { Id = model.Id, renk = silinecekRenkAdı };

            RenkSilValidator validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);
                }

                model = new RenkSilViewModel
                {
                    Renkler = _veterinerDbContext.Renks.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.renk,
                    }).ToList()
                };
                return View(model);
            }
            _veterinerDbContext.Renks.Remove(renkEntity);
            _veterinerDbContext.SaveChanges();

            TempData["RenkSilindi"] = $"{renkEntity.renk} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult CinsEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CinsEkle(CinsEkleViewModel model)
        {

            string cinsAdi = model.cins.ToUpper();
            var cinsEntity = new Cins { cins = cinsAdi };

            CinsEkleValidators validator = new CinsEkleValidators();
            ValidationResult result = validator.Validate(model);

            _veterinerDbContext.Cins.Add(cinsEntity);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }
            _veterinerDbContext.SaveChanges();

            TempData["CinsEklendi"] = $"{cinsAdi} cinsi eklendi";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult CinsSil()
        {
            CinsSilViewModel model = new CinsSilViewModel()
            {
                Cinsler = _veterinerDbContext.Cins.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.cins,
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult CinsSil(CinsSilViewModel model)
        {
            var silinecekCinsAdı = _veterinerDbContext.Cins
                    .Where(x => x.Id == model.Id)
                    .Select(x => x.cins).FirstOrDefault();

            var cinsEntity = new Cins { Id = model.Id, cins = silinecekCinsAdı };

            CinsSilValidator validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);
                }

                model = new CinsSilViewModel
                {
                    Cinsler = _veterinerDbContext.Cins.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.cins,
                    }).ToList()
                };
                return View(model);
            }
            _veterinerDbContext.Cins.Remove(cinsEntity);
            _veterinerDbContext.SaveChanges();

            TempData["CinsSilindi"] = $"{cinsEntity.cins} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult TurEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TurEkle(TurEKleViewModel model)
        {
            string turAdi = model.tur.ToUpper();
            var turEntity = new Tur { tur = turAdi };

            TurEkleValidators validator = new TurEkleValidators();
            ValidationResult result = validator.Validate(model);

            _veterinerDbContext.Turs.Add(turEntity);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }
            _veterinerDbContext.SaveChanges();

            TempData["TurEklendi"] = $"{turAdi} türü eklendi";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult TurSil()
        {

            var model = new TurSilViewModel
            {
                Turler = _veterinerDbContext.Turs.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.tur,
                }).ToList()
            };
            return View(model);

        }
        [HttpPost]
        public IActionResult TurSil(TurSilViewModel model)
        {
            var silinecekTurAdı = _veterinerDbContext.Turs
                    .Where(x => x.Id == model.Id)
                    .Select(x => x.tur).FirstOrDefault();

            var turEntity = new Tur { Id = model.Id, tur = silinecekTurAdı };

            TurSilValidator validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);
                }

                model = new TurSilViewModel
                {
                    Turler = _veterinerDbContext.Turs.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.tur,
                    }).ToList()
                };
                return View(model);

            }
            _veterinerDbContext.Turs.Remove(turEntity);
            _veterinerDbContext.SaveChanges();

            TempData["TurSilindi"] = $"{turEntity.tur} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult CinsTur()
        {
            var model = new TurCinsEkleViewModel
            {
                Cinsler = _veterinerDbContext.Cins.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.cins
                }).ToList(),

                Turler = _veterinerDbContext.Turs.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.tur,
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult CinsTur(TurCinsEkleViewModel model)
        {
            string turAdi = _veterinerDbContext.Turs
                .Where(x => x.Id == model.TurId)
                .Select(x => x.tur).FirstOrDefault();
            string cinsAdi = _veterinerDbContext.Cins
                .Where(x => x.Id == model.CinsId)
                .Select(x => x.cins).FirstOrDefault();

            TurCins turCinsEntity = new TurCins()
            {
                TurId = model.TurId,
                CinsId = model.CinsId,
            };

            TurCinsEkleValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model = new TurCinsEkleViewModel
                {
                    Cinsler = _veterinerDbContext.Cins.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.cins
                    }).ToList(),

                    Turler = _veterinerDbContext.Turs.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.tur,
                    }).ToList()
                };
                return View(model);
            }

            _veterinerDbContext.TurCins.Add(turCinsEntity);
            _veterinerDbContext.SaveChanges();
            TempData["CinsTurEklendi"] = $"{cinsAdi} cinsi ve {turAdi} türü eşleştirildi";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult CinsTurSil()
        {
            var model = new TurCinsSilViewModel
            {
                TurlerCinsler = _veterinerDbContext.TurCins.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Cins.cins.ToUpper()} - {x.Tur.tur.ToUpper()}"
                }).ToList(),

            };

            return View(model);

        }
        [HttpPost]
        public IActionResult CinsTurSil(TurCinsSilViewModel model)
        {
            var silinecekCinsId = _veterinerDbContext.TurCins
                .Where(x => x.Id == model.Id)
                .Select(x => x.CinsId).FirstOrDefault();
            var silinecekTurId = _veterinerDbContext.TurCins
                .Where(x => x.Id == model.Id)
                .Select(x => x.TurId).FirstOrDefault();

            string silinecekCinsAdi = _veterinerDbContext.Cins
                .Where(x => x.Id == silinecekCinsId)
                .Select(x => x.cins).FirstOrDefault();

            string silinecekTurAdi = _veterinerDbContext.Turs
                .Where(x => x.Id == silinecekTurId)
                .Select(x => x.tur).FirstOrDefault();

            var turCinsEntity = new TurCins
            {
                Id = model.Id,
                CinsId = silinecekCinsId,
                TurId = silinecekTurId
            };

            TurCinsSilValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                model = new TurCinsSilViewModel
                {
                    TurlerCinsler = _veterinerDbContext.TurCins.Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = $"{x.Cins.cins.ToUpper()} - {x.Tur.tur.ToUpper()}"
                    }).ToList()
                };
                return View(model);
            }

            _veterinerDbContext.TurCins.Remove(turCinsEntity);
            _veterinerDbContext.SaveChanges();

            TempData["EslemeKaldiridi"] = $"{silinecekCinsAdi} cinsi ve {silinecekTurAdi} türü arasındaki eşleştirme kaldırıldı.";

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
                                .credentials {{
                                    background-color: #f9f9f9;
                                    border-left: 4px solid #007bff;
                                    padding: 10px;
                                    margin-bottom: 20px;
                                    font-size: 16px;
                                }}
                                a.button {{
                                    display: inline-block;
                                    background-color: #28a745;
                                    color: #fff;
                                    padding: 10px 20px;
                                    text-decoration: none;
                                    border-radius: 5px;
                                    font-weight: bold;
                                    text-align: center;
                                }}
                                a.button:hover {{
                                    background-color: #218838;
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
                                <h1>Hoş Geldiniz!</h1>
                                <p>Sayın {kullaniciAdSoyad}, {tarih} tarihinde sisteme {rolAdi} olarak başarıyla üye oldunuz. Aşağıda giriş bilgileriniz yer almaktadır.</p>
                                <p>Giriş yaptıktan sonra lütfen şifrenizi değiştiriniz.</p>

                                <div class='credentials'>
                                    <p><strong>Kullanıcı Adı:</strong> {kullaniciAdi}</p>
                                    <p><strong>Şifre:</strong> {kullaniciSifresi}</p>
                                </div>
                                <p style='text-align:center;'>
                                    <a href='{loginUrl}' class='button'>Giriş Yap</a>
                                </p>
                                <p class='footer'>Bu e-posta otomatik olarak gönderilmiştir, lütfen yanıtlamayın.</p>
                            </div>
                        </body>
                        </html>";


                if (_emailSender.SendEmailAsync(model.Email, "Veteriner Bilgi Sistemi'ne Hoş Geldiniz!", mailMessage).IsFaulted)
                {
                    ViewBag.Hata = "Mail Gönderme işlemi başarısız oldu. Kayıt işlemi tamamlanamadı.";
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
                else
                {
                    TempData["CalısanEklendi"] = $"{model.InsanAdi.ToUpper()} {model.InsanSoyadi.ToUpper()} isimli calışan {rolAdi.ToUpper()} görevi ile sisteme kaydedildi. Kullanıcı adı ve şifresi {model.Email.ToUpper()} adresine gönderildi.";
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
            var toplamKayit = _veterinerDbContext.Insans.Count();
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

            if(secilenKisi == null)
            {
                return View("BadRequest");
            }

            ViewBag.SecilenKisi = secilenKisi;
            ViewBag.ToplamKayit = toplamKayit;

            return View(viewModel);
        }

    }
}

