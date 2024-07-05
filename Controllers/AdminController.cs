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
        public IActionResult RenkEkle(RenkViewModel model)
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

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult RenkSil()
        {
            RenkViewModel model = new RenkViewModel()
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
        public IActionResult RenkSil(RenkViewModel model)
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

                model = new RenkViewModel
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
        public IActionResult CinsEkle(CinsViewModel model)
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
            CinsViewModel model = new CinsViewModel()
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
        public IActionResult CinsSil(CinsViewModel model)
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

                model = new CinsViewModel
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
        public IActionResult TurEkle(TurViewModel model)
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

            var model = new TurViewModel
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
        public IActionResult TurSil(TurViewModel model)
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

                model = new TurViewModel
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
            var model = new TurCinsViewModel
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
        public IActionResult CinsTur(TurCinsViewModel model)
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
                model = new TurCinsViewModel
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
            var model = new TurCinsViewModel
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

        public IActionResult CinsTurSil(TurCinsViewModel model)
        {
            var silinecekCinsId = _veterinerDbContext.TurCins
                .Where(x=>x.Id == model.Id)
                .Select(x=>x.CinsId).FirstOrDefault();
            var silinecekTurId = _veterinerDbContext.TurCins
                .Where(x => x.Id == model.Id)
                .Select(x => x.TurId).FirstOrDefault();

            string silinecekCinsAdi = _veterinerDbContext.Cins
                .Where(x => x.Id == silinecekCinsId)
                .Select(x => x.cins).FirstOrDefault();

            string silinecekTurAdi = _veterinerDbContext.Turs
                .Where(x => x.Id == silinecekTurId)
                .Select(x => x.tur).FirstOrDefault();

            var turCinsEntity = new TurCins{ 
                Id = model.Id, 
                CinsId = silinecekCinsId , 
                TurId= silinecekTurId 
            };

            TurCinsSilValidators validator = new(_veterinerDbContext);
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model = new TurCinsViewModel
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
        public IActionResult RolEkle(string RolAdi)
        {
            RolAdi = RolAdi.ToUpper();
            var rolEntity = new Rol { RolAdi = RolAdi };

            RolValidators rolValidator = new RolValidators(_veterinerDbContext);
            ValidationResult result = rolValidator.Validate(rolEntity);


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
            TempData["RolEklendi"] = $"Çalışanlar için {RolAdi} türünde bir rol eklendi";
            return RedirectToAction();

        }

        [HttpGet]
        public IActionResult RolSil()
        {
            var model = new RolViewModel
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
        public IActionResult RolSil(RolViewModel model)
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

                model = new RolViewModel
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
            ViewBag.roller = _veterinerDbContext.Rols.Select(x => x).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CalisanEkle(string InsanAdi, string InsanSoyadi, string InsanTckn, string InsanMail, string InsanTel, string DiplomaNo, int RolId)
        {
            ViewBag.roller = _veterinerDbContext.Rols.Select(x => x).ToList();

            kullaniciAdi username = new kullaniciAdi(_veterinerDbContext);
            string kullaniciAdi = username.GenerateUsername(InsanAdi, InsanSoyadi, InsanMail).ToUpper();


            var calisan = new Insan
            {
                InsanAdi = InsanAdi.ToUpper(),
                InsanSoyadi = InsanSoyadi.ToUpper(),
                InsanTckn = InsanTckn,
                InsanMail = InsanMail.ToUpper(),
                InsanTel = InsanTel,
                DiplomaNo = DiplomaNo,
                RolId = RolId,
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

            InsanValidators validator = new InsanValidators(_veterinerDbContext);
            ValidationResult result = validator.Validate(calisan);


            SifreValidators validatorSifre = new SifreValidators(_veterinerDbContext);
            ValidationResult resultSifre = validatorSifre.Validate(password);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }

            if (!resultSifre.IsValid)
            {
                foreach (var error in resultSifre.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }


            _veterinerDbContext.Insans.Add(calisan);
            _veterinerDbContext.Sifres.Add(password);
            if (_veterinerDbContext.SaveChanges() > 0)
            {
                MailGonder mail = new MailGonder(InsanMail, kullaniciAdi, kullaniciSifresi);

                if (!mail.MailGonderHotmail(mail))
                {
                    ViewBag.Hata = "Mail Gönderme işlemi başarısız oldu. Kayıt işlemi tamamlanamadı.";
                    _veterinerDbContext.Sifres.Remove(password);
                    _veterinerDbContext.Insans.Remove(calisan);
                    _veterinerDbContext.SaveChanges();
                    return View();

                }
                var rolAdi = _veterinerDbContext.Rols.Where(x => x.RolId == RolId).Select(x => x.RolAdi);
                TempData["CalısanEklendi"] = $"{InsanAdi.ToUpper()} {InsanSoyadi.ToUpper()} isimli calisan {rolAdi.First().ToUpper()} görevi ile sisteme kaydedildi. Kullanıcı adı ve şifresi {InsanMail.ToUpper()} adresine gönderildi.";

            }

            return RedirectToAction();


        }


    }
}

