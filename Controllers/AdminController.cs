using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators;
using VeterinerApp.Data;
using VeterinerApp.Fonksiyonlar;
using VeterinerApp.Fonksiyonlar.MailGonderme;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinerApp.Models.ViewModel.Admin;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;



namespace VeterinerApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly VeterinerContext _veterinerDbContext;

        public AdminController(VeterinerContext veterinerDbContext)
        {
            _veterinerDbContext = veterinerDbContext;
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

            RenkEkleValidators renkvalidator = new RenkEkleValidators(_veterinerDbContext);
            ValidationResult result = renkvalidator.Validate(model);

            _veterinerDbContext.Renks.Add(renkEntity);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }
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

            RenkSilValidator validator = new(_veterinerDbContext);
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

            CinsEkleValidators validator = new CinsEkleValidators(_veterinerDbContext);
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

            CinsSilValidator validator = new(_veterinerDbContext);
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

            TurEkleValidators validator = new TurEkleValidators(_veterinerDbContext);
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

            TurSilValidator validator = new(_veterinerDbContext);
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

            TurCinsEkleValidators validator = new(_veterinerDbContext);
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

            TurCinsSilValidators validator = new(_veterinerDbContext);
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
                    }).ToList(),

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
            string rolAdi = model.RolAdi.ToUpper();
            var rolEntity = new Rol { RolAdi = rolAdi };

            RolValidators rolValidator = new RolValidators(_veterinerDbContext);
            ValidationResult result = rolValidator.Validate(model);


            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }

            _veterinerDbContext.Rols.Add(rolEntity);
            _veterinerDbContext.SaveChanges();
            TempData["RolEklendi"] = $"Çalışanlar için {rolAdi} türünde bir rol eklendi";
            return RedirectToAction();

        }

        [HttpGet]
        public IActionResult RolSil()
        {
            var model = new RolSilViewModel
            {
                Roller = _veterinerDbContext.Rols.Select(r => new SelectListItem
                {
                    Value = r.RolId.ToString(),
                    Text = r.RolAdi
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult RolSil(RolSilViewModel model)
        {

            var silinecekRolAdı = _veterinerDbContext.Rols
                .Where(x => x.RolId == model.RolId)
                .Select(x => x.RolAdi).FirstOrDefault();

            var rolEntity = new Rol { RolId = model.RolId, RolAdi = silinecekRolAdı };

            RolSilValidators validator = new(_veterinerDbContext);
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);


                }

                model = new RolSilViewModel
                {
                    Roller = _veterinerDbContext.Rols.Select(r => new SelectListItem
                    {
                        Value = r.RolId.ToString(),
                        Text = r.RolAdi
                    }).ToList()
                };
                return View(model);

            }
            _veterinerDbContext.Rols.Remove(rolEntity);
            _veterinerDbContext.SaveChanges();

            TempData["RolSilindi"] = $"{rolEntity.RolAdi} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult CalisanEkle()
        {
            var model = new InsanEkleViewModel
            {
                Roller = _veterinerDbContext.Rols.Select(r => new SelectListItem
                {
                    Value = r.RolId.ToString(),
                    Text = r.RolAdi
                }).ToList()
            };
            return View(model);

        }
        [HttpPost]
        public IActionResult CalisanEkle(InsanEkleViewModel model)
        {
            kullaniciAdi username = new kullaniciAdi(_veterinerDbContext);
            string kullaniciAdi = username.GenerateUsername(model.InsanAdi, model.InsanSoyadi, model.InsanMail).ToUpper();

            model.KullaniciAdi = kullaniciAdi;
            model.CalisiyorMu = true;

            var calisan = new Insan
            {
                InsanAdi = model.InsanAdi.ToUpper(),
                InsanSoyadi = model.InsanSoyadi.ToUpper(),
                InsanTckn = model.InsanTckn,
                InsanMail = model.InsanMail.ToLower(),
                InsanTel = model.InsanTel,
                DiplomaNo = model.DiplomaNo,
                RolId = model.RolId,
                KullaniciAdi = kullaniciAdi,
                CalisiyorMu = true
            };

            sifre sifre = new sifre();
            string kullaniciSifresi = sifre.GeneratePassword();

            var password = new Sifre()
            {
                SifreOlusturmaTarihi = DateTime.Now,
                SifreGecerlilikTarihi = DateTime.Now.AddDays(120),
                sifre = kullaniciSifresi,
                KullaniciAdi = kullaniciAdi,
                Id = _veterinerDbContext.Sifres.OrderByDescending(s => s.Id).FirstOrDefault()?.Id + 1 ?? 1

            };

            InsanEkleValidators validator = new InsanEkleValidators(_veterinerDbContext);
            ValidationResult result = validator.Validate(model);

            SifreValidators validatorSifre = new SifreValidators(_veterinerDbContext);
            ValidationResult resultSifre = validatorSifre.Validate(password);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model = new InsanEkleViewModel
                {
                    Roller = _veterinerDbContext.Rols.Select(r => new SelectListItem
                    {
                        Value = r.RolId.ToString(),
                        Text = r.RolAdi
                    }).ToList(),

                };
                return View(model);
            }

            if (!resultSifre.IsValid)
            {
                foreach (var error in resultSifre.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model = new InsanEkleViewModel
                {
                    Roller = _veterinerDbContext.Rols.Select(r => new SelectListItem
                    {
                        Value = r.RolId.ToString(),
                        Text = r.RolAdi
                    }).ToList(),

                };
                return View(model);
            }

            _veterinerDbContext.Insans.Add(calisan);
            _veterinerDbContext.Sifres.Add(password);
            if (_veterinerDbContext.SaveChanges() > 0)
            {
                MailGonder mail = new MailGonder(model.InsanMail, kullaniciAdi, kullaniciSifresi);

                if (!mail.MailGonderHotmail(mail))
                {
                    ViewBag.Hata = "Mail Gönderme işlemi başarısız oldu. Kayıt işlemi tamamlanamadı.";
                    _veterinerDbContext.Sifres.Remove(password);
                    _veterinerDbContext.Insans.Remove(calisan);
                    _veterinerDbContext.SaveChanges();
                    model = new InsanEkleViewModel
                    {
                        Roller = _veterinerDbContext.Rols.Select(r => new SelectListItem
                        {
                            Value = r.RolId.ToString(),
                            Text = r.RolAdi
                        }).ToList(),

                    };
                    return View(model);
                }
                var rolAdi = _veterinerDbContext.Rols
                    .Where(x => x.RolId == model.RolId)
                    .Select(x => x.RolAdi);

                TempData["CalısanEklendi"] = $"{model.InsanAdi.ToUpper()} {model.InsanSoyadi.ToUpper()} isimli calışan {rolAdi.First().ToUpper()} görevi ile sisteme kaydedildi. Kullanıcı adı ve şifresi {model.InsanMail.ToUpper()} adresine gönderildi.";
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
            var insanSec = new InsanSecViewModel() { InsanTckn = model.InsanTckn };
            var secilenKisi = _veterinerDbContext.Insans
                .Where(x => x.InsanTckn == insanSec.InsanTckn)
                .Select(x => new InsanDuzenleViewModel
                {
                    InsanAdi = x.InsanAdi,
                    InsanSoyadi = x.InsanSoyadi,
                    InsanTckn = x.InsanTckn,
                    InsanMail = x.InsanMail,
                    InsanTel = x.InsanTel,
                    DiplomaNo = x.DiplomaNo,
                    KullaniciAdi = x.KullaniciAdi,
                    CalisiyorMu = x.CalisiyorMu,
                    Maas = x.Maas,
                    RolId = x.RolId,
                    Roller = _veterinerDbContext.Rols.Select(r => new SelectListItem
                    {
                        Value = r.RolId.ToString(),
                        Text = r.RolAdi
                    }).ToList(),
                    RolAdi = _veterinerDbContext.Rols
                        .Where(r => r.RolId == x.RolId)
                        .Select(r => r.RolAdi)
                        .FirstOrDefault()
                }).FirstOrDefault();

            if (secilenKisi == null)
            {
                return View(model);
            }

            ViewBag.SecilenKisi = secilenKisi;
            return View(model);
        }
        [HttpPost]
        public IActionResult CalisanDuzenle(InsanDuzenleViewModel model)
        {
            var insan = _veterinerDbContext.Insans.FirstOrDefault(x => x.InsanTckn == model.InsanTckn);

            if (insan == null)
            {
                return View("CalisanSec");
            }
            InsanDuzenleValidators validator = new(_veterinerDbContext);
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View("CalisanSec");
            }

            insan.InsanAdi = model.InsanAdi;
            insan.InsanSoyadi = model.InsanSoyadi;
            insan.InsanMail = model.InsanMail.ToLower();
            insan.InsanTel = model.InsanTel;
            insan.DiplomaNo = model.DiplomaNo;
            insan.KullaniciAdi = model.KullaniciAdi;
            insan.CalisiyorMu = model.CalisiyorMu;
            insan.Maas = model.Maas;
            insan.RolId = model.RolId;

            _veterinerDbContext.SaveChanges();
            TempData["KisiGuncellendi"] = $"{insan.InsanAdi} {insan.InsanSoyadi} adlı kişinin bilgileri başarı ile güncellendi.";
            return View("CalisanSec");
        }

        [HttpGet]
        public IActionResult CalisanListele(int sayfaNumarasi = 1)
        {
            int sayfaBoyutu = 4;
            var toplamKayit = _veterinerDbContext.Insans.Count();
            var calisanlar = _veterinerDbContext.Insans.Select(insan => new CalisanListeleViewModel
            {
                InsanTckn = insan.InsanTckn,
                InsanAdi = insan.InsanAdi,
                InsanSoyadi = insan.InsanSoyadi,
                InsanTel = insan.InsanTel,
                InsanMail = insan.InsanMail,
                RolId = insan.RolId,
                DiplomaNo = insan.DiplomaNo,
                CalisiyorMu = insan.CalisiyorMu,
                Maas = insan.Maas,
                KullaniciAdi = insan.KullaniciAdi,
                RolAdi = _veterinerDbContext.Rols
                    .Where(rol => rol.RolId == insan.RolId)
                    .Select(rol => rol.RolAdi)
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
            var calisanlar = _veterinerDbContext.Insans.Select(insan => new CalisanListeleViewModel
            {
                InsanTckn = insan.InsanTckn,
                InsanAdi = insan.InsanAdi,
                InsanSoyadi = insan.InsanSoyadi,
                InsanTel = insan.InsanTel,
                InsanMail = insan.InsanMail,
                RolId = insan.RolId,
                DiplomaNo = insan.DiplomaNo,
                CalisiyorMu = insan.CalisiyorMu,
                Maas = insan.Maas,
                KullaniciAdi = insan.KullaniciAdi,
                RolAdi = _veterinerDbContext.Rols
                    .Where(rol => rol.RolId == insan.RolId)
                    .Select(rol => rol.RolAdi)
                    .FirstOrDefault()
            });

            var viewModel = SayfalamaListesi<CalisanListeleViewModel>.Olustur(calisanlar, sayfaNumarasi, sayfaBoyutu);

            var secilenKisi = await _veterinerDbContext.Insans
                .Where(insan => insan.InsanTckn == secilenKisiTckn)
                .Select(insan => new CalisanListeleViewModel
                {
                    InsanAdi = insan.InsanAdi,
                    InsanSoyadi = insan.InsanSoyadi,
                    InsanTckn = insan.InsanTckn,
                    InsanMail = insan.InsanMail.ToLower(),
                    InsanTel = insan.InsanTel,
                    DiplomaNo = insan.DiplomaNo,
                    KullaniciAdi = insan.KullaniciAdi,
                    CalisiyorMu = insan.CalisiyorMu,
                    Maas = insan.Maas,
                    RolId = insan.RolId,
                    RolAdi = _veterinerDbContext.Rols
                        .Where(rol => rol.RolId == insan.RolId)
                        .Select(rol => rol.RolAdi)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            ViewBag.SecilenKisi = secilenKisi;
            ViewBag.ToplamKayit = toplamKayit; 

            return View(viewModel);
        }



    }
}
