using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators;
using VeterinerApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IValidator<RenkEkleViewModel> _renkEkleValidator;

        private readonly IValidator<RolSilViewModel> _rolSilValidator;


        public AdminController(VeterinerContext veterinerDbContext, IValidator<RolSilViewModel> rolSilValidator, IValidator<RenkEkleViewModel> renkEkleValidator)
        {
            _veterinerDbContext = veterinerDbContext;
            _rolSilValidator = rolSilValidator;
            _renkEkleValidator = renkEkleValidator;
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

            return RedirectToAction();
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
                if(model.Id is string)
                {
                    foreach (var erros in result.Errors)
                    {
                        ModelState.AddModelError("sadsad", erros.ErrorMessage);
                    }
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
        public IActionResult CinsEkle(string cins)
        {
            cins = cins.ToUpper();
            Cins cinsEntity = new Cins { cins = cins };
            CinsValidators Validator = new CinsValidators(_veterinerDbContext);
            ValidationResult result = Validator.Validate(cinsEntity);

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

            TempData["CinsEklendi"] = $"{cins} cinsi eklendi";

            return RedirectToAction();

        }

        [HttpGet]
        public IActionResult CinsSil()
        {
            var cinsler = _veterinerDbContext.Cins.ToList();
            var cins = new Cins();
            var model = (cinsler, cins);
            return View(model);

        }

        [HttpPost]
        public IActionResult CinsSil(string SecilenCins)
        {
            var cinsler = _veterinerDbContext.Cins.ToList();

            if (SecilenCins == "null" || SecilenCins == "NULL")
            {
                SecilenCins = "";
            }
            else
            {
                SecilenCins = SecilenCins.ToUpper();
            }

            var cins = new Cins { cins = SecilenCins };
            CinsSilValidator CinsValidator = new CinsSilValidator(_veterinerDbContext);
            ValidationResult result = CinsValidator.Validate(cins);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Item2.cins", error.ErrorMessage);
                }
                return View((cinsler, cins));
            }

            var Cins = _veterinerDbContext.Cins.FirstOrDefault(r => r.cins == SecilenCins);
            if (Cins != null)
            {
                _veterinerDbContext.Cins.Remove(Cins);
                TempData["CinsSilindi"] = $"{Cins.cins} cinsi silindi";
                _veterinerDbContext.SaveChanges();
            }

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult TurEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TurEkle(string tur)
        {
            tur = tur.ToUpper();
            Tur turEntity = new Tur { tur = tur };
            TurValidators Validator = new TurValidators(_veterinerDbContext);
            ValidationResult result = Validator.Validate(turEntity);

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

            TempData["TurEklendi"] = $"{tur} türü eklendi";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult TurSil()
        {
            var turler = _veterinerDbContext.Turs.ToList();
            var tur = new Tur();
            var model = (turler, tur);
            return View(model);
        }

        [HttpPost]
        public IActionResult TurSil(string secilenTur)
        {
            var turler = _veterinerDbContext.Turs.ToList();

            if (secilenTur == "null" || secilenTur == "NULL")
            {
                secilenTur = "";
            }
            else
            {
                secilenTur = secilenTur.ToUpper();
            }

            var tur = new Tur { tur = secilenTur };
            TurSilValidator turValidator = new TurSilValidator(_veterinerDbContext);
            ValidationResult result = turValidator.Validate(tur);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Item2.tur", error.ErrorMessage);
                }
                return View((turler, tur));
            }

            var Tur = _veterinerDbContext.Turs.FirstOrDefault(r => r.tur == secilenTur);
            if (Tur != null)
            {
                _veterinerDbContext.Turs.Remove(Tur);
                TempData["TurSilindi"] = $"{Tur.tur} türü silindi";
                _veterinerDbContext.SaveChanges();
            }

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult CinsTur()
        {
            var cinsler = _veterinerDbContext.Cins.ToList();
            var cins = new Cins();
            var turler = _veterinerDbContext.Turs.ToList();
            var tur = new Tur();
            var model = (cinsler, cins, turler, tur);
            return View(model);
        }

        [HttpPost]
        public IActionResult CinsTur(String CinsAdi, String TurAdi)
        {

            var cinsler = _veterinerDbContext.Cins.ToList();
            var cins = new Cins();
            var turler = _veterinerDbContext.Turs.ToList();
            var tur = new Tur();


            var cinsId = _veterinerDbContext.Cins
                .Where(x => x.cins.ToUpper() == CinsAdi.ToUpper())
                .Select(x => x.Id)
                .FirstOrDefault();

            var turId = _veterinerDbContext.Turs
                .Where(x => x.tur.ToUpper() == TurAdi.ToUpper())
                .Select(x => x.Id)
                .FirstOrDefault();

            var turCinsId = _veterinerDbContext.TurCins
                .Where(x => x.CinsId == cinsId && x.TurId == turId)
                .Select(x => x.Id)
                .FirstOrDefault();

            var turCins = new TurCins { CinsId = cinsId, TurId = turId };

            TurCinsValidators validator = new TurCinsValidators(_veterinerDbContext);
            ValidationResult result = validator.Validate(turCins);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View((cinsler, cins, turler, tur));
            }

            _veterinerDbContext.TurCins.Add(turCins);
            _veterinerDbContext.SaveChanges();
            TempData["CinsTurEklendi"] = $"{CinsAdi} cinsi ve {TurAdi} türü eşleştirildi";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult CinsTurSil()
        {
            TurCins turCins = new();

            var turCinsList = _veterinerDbContext.TurCins
                    .Join(
                        _veterinerDbContext.Cins,
                        turCins => turCins.CinsId,
                        cins => cins.Id,
                        (turCins, cins) => new { turCins.Id, turCins.TurId, CinsAdi = cins.cins }
                    )
                    .Join(
                        _veterinerDbContext.Turs,
                        turCinsWithCins => turCinsWithCins.TurId,
                        tur => tur.Id,
                        (turCinsWithCins, tur) => new { turCinsWithCins.Id, turCinsWithCins.CinsAdi, TurAdi = tur.tur }
                    )
                    .ToList();

            var turCinsIdList = turCinsList.Select(item => item.Id).ToList();
            var cinsAdlariList = turCinsList.Select(item => item.CinsAdi).ToList();
            var turAdlariList = turCinsList.Select(item => item.TurAdi).ToList();

            var model = (turCinsIdList, cinsAdlariList, turAdlariList, turCins);
            return View(model);

        }

        [HttpPost]

        public IActionResult CinsTurSil(string idValue)
        {
            int.TryParse(idValue, out int id);

            var eslemesiKaldirilacakTurCins = new TurCins { Id = id };


            var turCinsList = _veterinerDbContext.TurCins
                    .Join(
                        _veterinerDbContext.Cins,
                        turCins => turCins.CinsId,
                        cins => cins.Id,
                        (turCins, cins) => new { turCins.Id, turCins.TurId, CinsAdi = cins.cins }
                    )
                    .Join(
                        _veterinerDbContext.Turs,
                        turCinsWithCins => turCinsWithCins.TurId,
                        tur => tur.Id,
                        (turCinsWithCins, tur) => new { turCinsWithCins.Id, turCinsWithCins.CinsAdi, TurAdi = tur.tur }
                    )
                    .ToList();


            var turCinsIdList = turCinsList.Select(item => item.Id).ToList();
            var cinsAdlariList = turCinsList.Select(item => item.CinsAdi).ToList();
            var turAdlariList = turCinsList.Select(item => item.TurAdi).ToList();

            TurCins turCins = new();


            TurCinsSilValidators validator = new(_veterinerDbContext);
            ValidationResult result = validator.Validate(eslemesiKaldirilacakTurCins);


            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View((turCinsIdList, cinsAdlariList, turAdlariList, turCins));

            }

            var turCinsInfo = _veterinerDbContext.TurCins
                .Where(x => x.Id == id)
                .Join(
                    _veterinerDbContext.Cins,
                    tc => tc.CinsId,
                    c => c.Id,
                    (tc, c) => new { tc.Id, tc.TurId, CinsAdi = c.cins }
                )
                .Join(
                    _veterinerDbContext.Turs,
                    tc => tc.TurId,
                    t => t.Id,
                    (tc, t) => new { tc.Id, CinsAdi = tc.CinsAdi, TurAdi = t.tur }
                )
                .FirstOrDefault();

            _veterinerDbContext.TurCins.Remove(eslemesiKaldirilacakTurCins);
            _veterinerDbContext.SaveChanges();


            TempData["EslemeKaldiridi"] = $"{turCinsInfo.CinsAdi} cinsi ve {turCinsInfo.TurAdi} türü arasındaki eşleştirme kaldırıldı.";

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

