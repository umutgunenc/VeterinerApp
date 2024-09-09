using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Fonksiyonlar;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.Animal;
using VeterinerApp.Models.ViewModel.Animal;

namespace VeterinerApp.Controllers
{
    [Authorize]
    public class AnimalController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly VeterinerDBContext _context;
        private readonly IEmailSender _emailSender;
        public AnimalController(UserManager<AppUser> usermanager, VeterinerDBContext context, IEmailSender emailSender)
        {
            _userManager = usermanager;
            _context = context;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult AddAnimal()
        {
            var cinsAdlari = _context.Cinsler.Select(c => new SelectListItem
            {
                Text = c.CinsAdi,
                Value = c.CinsId.ToString()
            }).ToList();

            var renkAdlari = _context.Renkler.Select(r => new SelectListItem
            {
                Text = r.RenkAdi,
                Value = r.RenkId.ToString()
            }).ToList();

            var Anneler = _context.Hayvanlar
                .Where(h => h.HayvanCinsiyet == "D" || h.HayvanCinsiyet == "d")
                .Select(h => new AddAnimalViewModel
                {
                    HayvanAdi = h.HayvanAdi,
                    HayvanId = h.HayvanId,
                    rengi = _context.Renkler
                        .Where(r => r.RenkId == h.RenkId)
                        .Select(r => r.RenkAdi)
                        .FirstOrDefault(),
                    cinsi = _context.Cinsler
                        .Where(c => c.CinsId == h.CinsId)
                        .Select(c => c.CinsAdi)
                        .FirstOrDefault(),
                    turu = _context.Turler
                        .Where(t => t.TurId == h.TurId)
                        .Select(t => t.TurAdi)
                        .FirstOrDefault(),
                    SahipTckn = _context.SahipHayvanlar
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.Sahip.InsanTckn)
                        .FirstOrDefault(),
                    SahipAdSoyad = _context.Users
                        .Where(u => u.InsanTckn == _context.SahipHayvanlar
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.Sahip.InsanTckn)
                        .FirstOrDefault())
                        .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                        .FirstOrDefault(),
                    HayvanDogumTarihi = h.HayvanDogumTarihi
                })
                .ToList();

            var Babalar = _context.Hayvanlar
                .Where(h => h.HayvanCinsiyet == "E" || h.HayvanCinsiyet == "e")
                .Select(h => new AddAnimalViewModel
                {
                    HayvanAdi = h.HayvanAdi,
                    HayvanId = h.HayvanId,
                    rengi = _context.Renkler
                        .Where(r => r.RenkId == h.RenkId)
                        .Select(r => r.RenkAdi)
                        .FirstOrDefault(),
                    cinsi = _context.Cinsler
                        .Where(c => c.CinsId == h.CinsId)
                        .Select(c => c.CinsAdi)
                        .FirstOrDefault(),
                    turu = _context.Turler
                        .Where(t => t.TurId == h.TurId)
                        .Select(t => t.TurAdi)
                        .FirstOrDefault(),
                    SahipTckn = _context.SahipHayvanlar
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.Sahip.InsanTckn)
                        .FirstOrDefault(),
                    SahipAdSoyad = _context.Users
                        .Where(u => u.InsanTckn == _context.SahipHayvanlar
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.Sahip.InsanTckn)
                        .FirstOrDefault())
                        .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                        .FirstOrDefault(),
                    HayvanDogumTarihi = h.HayvanDogumTarihi
                })
                .ToList();

            var cinsiyet = new List<SelectListItem>
            {
                new SelectListItem { Text = "Erkek", Value = "E" },
                new SelectListItem { Text = "Dişi", Value = "D" }
            };


            var model = new AddAnimalViewModel
            {
                CinsAdlari = cinsAdlari,
                RenkAdlari = renkAdlari,
                HayvanAnneList = Anneler.Select(h => new SelectListItem
                {
                    Text = $"{h.SahipTckn.Substring(0, 3) + new string('*', Math.Max(h.SahipTckn.Length - 6, 0)) + h.SahipTckn.Substring(h.SahipTckn.Length - 3)} " +
                        $"{h.SahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.SahipAdSoyad.Length - 4, 0)) + h.SahipAdSoyad.Substring(h.SahipAdSoyad.Length - 2)} - " +
                        $"{h.HayvanId} {h.HayvanAdi} {h.cinsi} {h.turu} {h.rengi} {h.HayvanDogumTarihi.ToString("dd-MM-yyyy")}",
                    Value = h.HayvanId.ToString()
                }).ToList(),
                HayvanBabaList = Babalar.Select(h => new SelectListItem
                {
                    Text = $"{h.SahipTckn.Substring(0, 3) + new string('*', Math.Max(h.SahipTckn.Length - 6, 0)) + h.SahipTckn.Substring(h.SahipTckn.Length - 3)} " +
                        $"{h.SahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.SahipAdSoyad.Length - 4, 0)) + h.SahipAdSoyad.Substring(h.SahipAdSoyad.Length - 2)} - " +
                        $"{h.HayvanId} {h.HayvanAdi} {h.cinsi} {h.turu} {h.rengi} {h.HayvanDogumTarihi.ToString("dd-MM-yyyy")} ",
                    Value = h.HayvanId.ToString()
                }).ToList(),
                CinsiyetList = cinsiyet,
                Sahip = _userManager.GetUserAsync(User).Result,

            };

            return View(model);

        }

        public JsonResult TurleriGetir(int cinsId)
        {
            var turAdlari = _context.CinsTur
                .Where(tc => tc.CinsId == cinsId)
                .Join(_context.Turler,
                      tc => tc.TurId,
                      t => t.TurId,
                      (tc, t) => new SelectListItem
                      {
                          Text = t.TurAdi,
                          Value = t.TurId.ToString()
                      }).ToList();

            return Json(turAdlari);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimal(AddAnimalViewModel model)
        {

            HayvanValidator validator = new HayvanValidator();
            var result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                var cinsAdlari = _context.Cinsler.Select(c => new SelectListItem
                {
                    Text = c.CinsAdi,
                    Value = c.CinsId.ToString()
                }).ToList();

                var renkAdlari = _context.Renkler.Select(r => new SelectListItem
                {
                    Text = r.RenkAdi,
                    Value = r.RenkId.ToString()
                }).ToList();

                var Anneler = _context.Hayvanlar
                    .Where(h => h.HayvanCinsiyet == "D" || h.HayvanCinsiyet == "d")
                    .Select(h => new AddAnimalViewModel
                    {
                        HayvanAdi = h.HayvanAdi,
                        HayvanId = h.HayvanId,
                        rengi = _context.Renkler
                            .Where(r => r.RenkId == h.RenkId)
                            .Select(r => r.RenkAdi)
                            .FirstOrDefault(),
                        cinsi = _context.Cinsler
                            .Where(c => c.CinsId == h.CinsId)
                            .Select(c => c.CinsAdi)
                            .FirstOrDefault(),
                        turu = _context.Turler
                            .Where(t => t.TurId == h.TurId)
                            .Select(t => t.TurAdi)
                            .FirstOrDefault(),
                        SahipTckn = _context.SahipHayvanlar
                            .Where(sh => sh.HayvanId == h.HayvanId)
                            .Select(s => s.Sahip.InsanTckn)
                            .FirstOrDefault(),
                        SahipAdSoyad = _context.Users
                            .Where(u => u.InsanTckn == _context.SahipHayvanlar
                            .Where(sh => sh.HayvanId == h.HayvanId)
                            .Select(s => s.Sahip.InsanTckn)
                            .FirstOrDefault())
                            .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                            .FirstOrDefault(),
                        HayvanDogumTarihi = h.HayvanDogumTarihi
                    })
                    .ToList();

                var Babalar = _context.Hayvanlar
                    .Where(h => h.HayvanCinsiyet == "E" || h.HayvanCinsiyet == "e")
                    .Select(h => new AddAnimalViewModel
                    {
                        HayvanAdi = h.HayvanAdi,
                        HayvanId = h.HayvanId,
                        rengi = _context.Renkler
                            .Where(r => r.RenkId == h.RenkId)
                            .Select(r => r.RenkAdi)
                            .FirstOrDefault(),
                        cinsi = _context.Cinsler
                            .Where(c => c.CinsId == h.CinsId)
                            .Select(c => c.CinsAdi)
                            .FirstOrDefault(),
                        turu = _context.Turler
                            .Where(t => t.TurId == h.TurId)
                            .Select(t => t.TurAdi)
                            .FirstOrDefault(),
                        SahipTckn = _context.SahipHayvanlar
                            .Where(sh => sh.HayvanId == h.HayvanId)
                            .Select(s => s.Sahip.InsanTckn)
                            .FirstOrDefault(),
                        SahipAdSoyad = _context.Users
                            .Where(u => u.InsanTckn == _context.SahipHayvanlar
                            .Where(sh => sh.HayvanId == h.HayvanId)
                            .Select(s => s.Sahip.InsanTckn)
                            .FirstOrDefault())
                            .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                            .FirstOrDefault(),
                        HayvanDogumTarihi = h.HayvanDogumTarihi

                    })
                    .ToList();

                var cinsiyet = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Erkek", Value = "E" },
                    new SelectListItem { Text = "Dişi", Value = "D" }
                };

                var returnModel = new AddAnimalViewModel
                {
                    CinsAdlari = cinsAdlari,
                    RenkAdlari = renkAdlari,
                    HayvanAnneList = Anneler.Select(h => new SelectListItem
                    {
                        Text = $"{h.SahipTckn.Substring(0, 3) + new string('*', Math.Max(h.SahipTckn.Length - 6, 0)) + h.SahipTckn.Substring(h.SahipTckn.Length - 3)} " +
                        $"{h.SahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.SahipAdSoyad.Length - 4, 0)) + h.SahipAdSoyad.Substring(h.SahipAdSoyad.Length - 2)} - " +
                        $"{h.HayvanId} {h.HayvanAdi} {h.cinsi} {h.turu}",
                        Value = h.HayvanId.ToString()
                    }).ToList(),
                    HayvanBabaList = Babalar.Select(h => new SelectListItem
                    {
                        Text = $"{h.SahipTckn.Substring(0, 3) + new string('*', Math.Max(h.SahipTckn.Length - 6, 0)) + h.SahipTckn.Substring(h.SahipTckn.Length - 3)} " +
                        $"{h.SahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.SahipAdSoyad.Length - 4, 0)) + h.SahipAdSoyad.Substring(h.SahipAdSoyad.Length - 2)} - " +
                        $"{h.HayvanId} {h.HayvanAdi} {h.cinsi} {h.turu}",
                        Value = h.HayvanId.ToString()
                    }).ToList(),
                    CinsiyetList = cinsiyet,
                    Sahip = _userManager.GetUserAsync(User).Result
                };

                return View(returnModel);
            }


            Hayvan hayvan = new Hayvan();

            hayvan.HayvanAdi = model.HayvanAdi.ToUpper();
            hayvan.HayvanCinsiyet = model.HayvanCinsiyet;
            hayvan.HayvanKilo = model.HayvanKilo;
            hayvan.HayvanDogumTarihi = model.HayvanDogumTarihi;
            hayvan.HayvanOlumTarihi = model.HayvanOlumTarihi;
            hayvan.HayvanAnneId = model.HayvanAnneId;
            hayvan.HayvanBabaId = model.HayvanBabaId;
            hayvan.CinsId = model.CinsId;
            hayvan.RenkId = model.RenkId;
            hayvan.TurId = model.TurId;
            _context.Hayvanlar.Add(hayvan);
            _context.SaveChanges();

            SahipHayvan sahipHayvan = new SahipHayvan();
            sahipHayvan.SahiplikTarihi = model.SahiplikTarihi;
            sahipHayvan.Sahip.InsanTckn = _userManager.GetUserAsync(User).Result.InsanTckn;
            sahipHayvan.HayvanId = hayvan.HayvanId;


            if (model.PhotoOption == "changePhoto" && model.filePhoto != null)
            {
                var dosyaUzantısı = Path.GetExtension(model.filePhoto.FileName);
                var dosyaAdi = string.Format($"{Guid.NewGuid()}{dosyaUzantısı}");
                var hayvanKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\animals", hayvan.HayvanId.ToString());

                if (!Directory.Exists(hayvanKlasoru))
                {
                    Directory.CreateDirectory(hayvanKlasoru);
                }

                var filePath = Path.Combine(hayvanKlasoru, dosyaAdi);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.filePhoto.CopyToAsync(stream);
                }

                // Web URL'sini oluşturma
                var fileUrl = $"/img/animals/{hayvan.HayvanId}/{dosyaAdi}";

                // Veritabanına URL'yi kaydetme
                hayvan.ImgUrl = fileUrl;
                await _context.SaveChangesAsync();
            }
            else
            {
                hayvan.ImgUrl = null;
                await _context.SaveChangesAsync();

            }

            _context.SahipHayvanlar.Add(sahipHayvan);

            _context.SaveChangesAsync();

            TempData["AddAnimal"] = $"{model.HayvanAdi} isimli hayvan başarıyla eklendi.";

            return RedirectToAction();

        }

        [HttpGet]
        public async Task<IActionResult> Information(int hayvanId)
        {
            var hayvan = _context.Hayvanlar.Find(hayvanId);
            var kullanici = _userManager.GetUserAsync(User).Result;

            var userHayvan = _context.SahipHayvanlar
                .Where(sh => sh.HayvanId == hayvanId && sh.Sahip.InsanTckn == kullanici.InsanTckn)
                .FirstOrDefault();

            if (userHayvan == null)
            {
                return View("BadRequest");
            }

            var muayeneler = _context.Muayeneler
                    .Where(muayene => muayene.HayvanId == hayvanId)
                    .Select(muayene => new
                    {
                        muayene.MuayeneId,
                        muayene.MuayeneTarihi,
                        muayene.HekimId,
                        HekimAdi = _userManager.Users
                            .Where(hekim => hekim.Id == muayene.HekimId)
                            .Select(hekim => hekim.InsanAdi + " " + hekim.InsanSoyadi)
                            .FirstOrDefault()
                    })
                    .ToList();

            var UserAnimal = new HayvanlarViewModel
            {
                HayvanId = hayvan.HayvanId,
                HayvanAdi = hayvan.HayvanAdi,
                imgURL = hayvan.ImgUrl,
                HayvanCinsiyet = hayvan.HayvanCinsiyet,
                HayvanKilo = hayvan.HayvanKilo,
                HayvanDogumTarihi = hayvan.HayvanDogumTarihi,
                HayvanOlumTarihi = hayvan.HayvanOlumTarihi,
                HayvanAnneAdi = _context.Hayvanlar.Where(ha => ha.HayvanId == hayvan.HayvanAnneId).Select(ha => ha.HayvanAdi).FirstOrDefault(),
                HayvanBabaAdi = _context.Hayvanlar.Where(hb => hb.HayvanId == hayvan.HayvanBabaId).Select(hb => hb.HayvanAdi).FirstOrDefault(),
                TurAdi = _context.Turler.Where(t => t.TurId == hayvan.TurId).Select(t => t.TurAdi).FirstOrDefault(),
                CinsAdi = _context.Cinsler.Where(c => c.CinsId == hayvan.CinsId).Select(c => c.CinsAdi).FirstOrDefault(),
                RenkAdi = _context.Renkler.Where(r => r.RenkId == hayvan.RenkId).Select(r => r.RenkAdi).FirstOrDefault(),
                SahiplikTarihi = _context.SahipHayvanlar
                    .Where(x => x.HayvanId == hayvanId && x.Sahip.InsanTckn == kullanici.InsanTckn)
                    .Select(x => x.SahiplikTarihi)
                    .FirstOrDefault(),
                Muayeneler = muayeneler.Select(m => new MuayeneViewModel
                {
                    MuayeneId = m.MuayeneId,
                    MuayeneTarihi = m.MuayeneTarihi,
                    HekimAdi = m.HekimAdi
                }).ToList()
            };

            return View(UserAnimal);

        }

        [HttpGet]
        public IActionResult EditAnimal(int hayvanId)
        {

            var hayvan = _context.Hayvanlar.Find(hayvanId);
            var kullanici = _userManager.GetUserAsync(User).Result;

            var userHayvan = _context.SahipHayvanlar
                .Where(sh => sh.HayvanId == hayvanId && sh.Sahip.InsanTckn == kullanici.InsanTckn)
                .FirstOrDefault();

            if (userHayvan == null)
            {
                return View("BadRequest");
            }

            // İmza oluştur
            var signature = Signature.CreateSignature(hayvanId, kullanici.Id);


            var cinsAdlari = _context.Cinsler.Select(c => new SelectListItem
            {
                Text = c.CinsAdi,
                Value = c.CinsId.ToString()
            }).ToList();

            var renkAdlari = _context.Renkler.Select(r => new SelectListItem
            {
                Text = r.RenkAdi,
                Value = r.RenkId.ToString()
            }).ToList();

            var Anneler = _context.Hayvanlar
                .Where(h => h.HayvanCinsiyet == "D" || h.HayvanCinsiyet == "d")
                .Select(h => new AddAnimalViewModel
                {
                    HayvanAdi = h.HayvanAdi,
                    HayvanId = h.HayvanId,
                    rengi = _context.Renkler
                        .Where(r => r.RenkId == h.RenkId)
                        .Select(r => r.RenkAdi)
                        .FirstOrDefault(),
                    cinsi = _context.Cinsler
                        .Where(c => c.CinsId == h.CinsId)
                        .Select(c => c.CinsAdi)
                        .FirstOrDefault(),
                    turu = _context.Turler
                        .Where(t => t.TurId == h.TurId)
                        .Select(t => t.TurAdi)
                        .FirstOrDefault(),
                    SahipTckn = _context.SahipHayvanlar
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.Sahip.InsanTckn)
                        .FirstOrDefault(),
                    SahipAdSoyad = _context.Users
                        .Where(u => u.InsanTckn == _context.SahipHayvanlar
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.Sahip.InsanTckn)
                        .FirstOrDefault())
                        .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                        .FirstOrDefault(),
                    HayvanDogumTarihi = h.HayvanDogumTarihi
                })
                .ToList();

            var Babalar = _context.Hayvanlar
                .Where(h => h.HayvanCinsiyet == "E" || h.HayvanCinsiyet == "e")
                .Select(h => new AddAnimalViewModel
                {
                    HayvanAdi = h.HayvanAdi,
                    HayvanId = h.HayvanId,
                    rengi = _context.Renkler
                        .Where(r => r.RenkId == h.RenkId)
                        .Select(r => r.RenkAdi)
                        .FirstOrDefault(),
                    cinsi = _context.Cinsler
                        .Where(c => c.CinsId == h.CinsId)
                        .Select(c => c.CinsAdi)
                        .FirstOrDefault(),
                    turu = _context.Turler
                        .Where(t => t.TurId == h.TurId)
                        .Select(t => t.TurAdi)
                        .FirstOrDefault(),
                    SahipTckn = _context.SahipHayvanlar
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.Sahip.InsanTckn)
                        .FirstOrDefault(),
                    SahipAdSoyad = _context.Users
                        .Where(u => u.InsanTckn == _context.SahipHayvanlar
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.Sahip.InsanTckn)
                        .FirstOrDefault())
                        .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                        .FirstOrDefault(),
                    HayvanDogumTarihi = h.HayvanDogumTarihi
                })
                .ToList();

            var cinsiyet = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Erkek", Value = "E" },
                    new SelectListItem { Text = "Dişi", Value = "D" }
                };

            var SahipTCKN = kullanici.InsanTckn;
            EditAnimalViewModel model = new()
            {
                HayvanId = hayvanId,
                HayvanAdi = _context.Hayvanlar.Find(hayvanId).HayvanAdi.ToUpper(),
                HayvanCinsiyet = _context.Hayvanlar.Find(hayvanId).HayvanCinsiyet,
                HayvanKilo = _context.Hayvanlar.Find(hayvanId).HayvanKilo,
                HayvanDogumTarihi = _context.Hayvanlar.Find(hayvanId).HayvanDogumTarihi,
                HayvanOlumTarihi = _context.Hayvanlar.Find(hayvanId).HayvanOlumTarihi,
                CinsId = _context.Hayvanlar.Find(hayvanId).CinsId,
                TurId = _context.Hayvanlar.Find(hayvanId).TurId,
                RenkId = _context.Hayvanlar.Find(hayvanId).RenkId,
                cinsi = _context.Cinsler
                    .Where(c => c.CinsId == _context.Hayvanlar.Find(hayvanId).CinsId)
                    .Select(c => c.CinsAdi)
                    .FirstOrDefault(),
                turu = _context.Turler
                    .Where(t => t.TurId == _context.Hayvanlar.Find(hayvanId).TurId)
                    .Select(t => t.TurAdi)
                    .FirstOrDefault(),
                rengi = _context.Renkler
                    .Where(r => r.RenkId == _context.Hayvanlar.Find(hayvanId).RenkId)
                    .Select(r => r.RenkAdi)
                    .FirstOrDefault(),

                isDeath = _context.Hayvanlar.Find(hayvanId).HayvanOlumTarihi == null ? false : true,

                HayvanAnneId = _context.Hayvanlar.Find(hayvanId).HayvanAnneId,
                HayvanBabaId = _context.Hayvanlar.Find(hayvanId).HayvanBabaId,

                CinsAdlari = cinsAdlari,
                RenkAdlari = renkAdlari,


                HayvanAnneList = Anneler.Select(h => new SelectListItem
                {
                    Text = $"{h.SahipTckn.Substring(0, 3) + new string('*', Math.Max(h.SahipTckn.Length - 6, 0)) + h.SahipTckn.Substring(h.SahipTckn.Length - 3)} " +
                    $"{h.SahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.SahipAdSoyad.Length - 4, 0)) + h.SahipAdSoyad.Substring(h.SahipAdSoyad.Length - 2)} - " +
                    $"{h.HayvanId} {h.HayvanAdi} {h.cinsi} {h.turu} {h.rengi} {h.HayvanDogumTarihi.ToString("dd-MM-yyyy")}",
                    Value = h.HayvanId.ToString()
                }).ToList(),
                HayvanBabaList = Babalar.Select(h => new SelectListItem
                {
                    Text = $"{h.SahipTckn.Substring(0, 3) + new string('*', Math.Max(h.SahipTckn.Length - 6, 0)) + h.SahipTckn.Substring(h.SahipTckn.Length - 3)} " +
                    $"{h.SahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.SahipAdSoyad.Length - 4, 0)) + h.SahipAdSoyad.Substring(h.SahipAdSoyad.Length - 2)} - " +
                    $"{h.HayvanId} {h.HayvanAdi} {h.cinsi} {h.turu} {h.rengi} {h.HayvanDogumTarihi.ToString("dd-MM-yyyy")}",
                    Value = h.HayvanId.ToString()
                }).ToList(),
                CinsiyetList = cinsiyet,
                Sahip = _userManager.GetUserAsync(User).Result,
                SahiplikTarihi = _context.SahipHayvanlar
                    .Where(x => x.HayvanId == hayvanId && x.Sahip.InsanTckn == SahipTCKN)
                    .Select(x => x.SahiplikTarihi)
                    .FirstOrDefault(),
                SahiplikCikisTarihi = _context.SahipHayvanlar
                    .Where(x => x.HayvanId == hayvanId && x.Sahip.InsanTckn == SahipTCKN)
                    .Select(x => x.SahiplikCikisTarihi)
                    .FirstOrDefault(),
                ImgUrl = _context.Hayvanlar.Find(hayvanId).ImgUrl,
                SahipTckn = SahipTCKN,
                Signature = signature

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAnimal(EditAnimalViewModel model)
        {

            if (!Signature.VerifySignature(model.HayvanId, model.Sahip.Id, model.Signature))
            {
                //ModelState.AddModelError("Signature", "Veri manipülasyonu tespit edildi.");
                //return View("EditAnimal", returnModel);

                return View("Badrequest");
            }
            var cinsAdlari = _context.Cinsler.Select(c => new SelectListItem
            {
                Text = c.CinsAdi,
                Value = c.CinsId.ToString()
            }).ToList();
            var renkAdlari = _context.Renkler.Select(r => new SelectListItem
            {
                Text = r.RenkAdi,
                Value = r.RenkId.ToString()
            }).ToList();
            var Anneler = _context.Hayvanlar
                .Where(h => h.HayvanCinsiyet == "D" || h.HayvanCinsiyet == "d")
                .Select(h => new AddAnimalViewModel
                {
                    HayvanAdi = h.HayvanAdi,
                    HayvanId = h.HayvanId,
                    rengi = _context.Renkler
                        .Where(r => r.RenkId == h.RenkId)
                        .Select(r => r.RenkAdi)
                        .FirstOrDefault(),
                    cinsi = _context.Cinsler
                        .Where(c => c.CinsId == h.CinsId)
                        .Select(c => c.CinsAdi)
                        .FirstOrDefault(),
                    turu = _context.Turler
                        .Where(t => t.TurId == h.TurId)
                        .Select(t => t.TurAdi)
                        .FirstOrDefault(),
                    SahipTckn = _context.SahipHayvanlar
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.Sahip.InsanTckn)
                        .FirstOrDefault(),
                    SahipAdSoyad = _context.Users
                        .Where(u => u.InsanTckn == _context.SahipHayvanlar
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.Sahip.InsanTckn)
                        .FirstOrDefault())
                        .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                        .FirstOrDefault(),
                    HayvanDogumTarihi = h.HayvanDogumTarihi

                })
                .ToList();
            var Babalar = _context.Hayvanlar
                .Where(h => h.HayvanCinsiyet == "E" || h.HayvanCinsiyet == "e")
                .Select(h => new AddAnimalViewModel
                {
                    HayvanAdi = h.HayvanAdi,
                    HayvanId = h.HayvanId,
                    rengi = _context.Renkler
                        .Where(r => r.RenkId == h.RenkId)
                        .Select(r => r.RenkAdi)
                        .FirstOrDefault(),
                    cinsi = _context.Cinsler
                        .Where(c => c.CinsId == h.CinsId)
                        .Select(c => c.CinsAdi)
                        .FirstOrDefault(),
                    turu = _context.Turler
                        .Where(t => t.TurId == h.TurId)
                        .Select(t => t.TurAdi)
                        .FirstOrDefault(),
                    SahipTckn = _context.SahipHayvanlar
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.Sahip.InsanTckn)
                        .FirstOrDefault(),
                    SahipAdSoyad = _context.Users
                        .Where(u => u.InsanTckn == _context.SahipHayvanlar
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.Sahip.InsanTckn)
                        .FirstOrDefault())
                        .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                        .FirstOrDefault(),
                    HayvanDogumTarihi = h.HayvanDogumTarihi

                })
                .ToList();
            var cinsiyet = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Erkek", Value = "E" },
                    new SelectListItem { Text = "Dişi", Value = "D" }
                };
            var SahipTCKN = _userManager.GetUserAsync(User).Result.InsanTckn;

            EditAnimalViewModel returnModel = new()
            {
                HayvanId = model.HayvanId,
                HayvanAdi = _context.Hayvanlar.Find(model.HayvanId).HayvanAdi.ToUpper(),
                HayvanCinsiyet = _context.Hayvanlar.Find(model.HayvanId).HayvanCinsiyet,
                HayvanKilo = _context.Hayvanlar.Find(model.HayvanId).HayvanKilo,
                HayvanDogumTarihi = _context.Hayvanlar.Find(model.HayvanId).HayvanDogumTarihi,
                HayvanOlumTarihi = _context.Hayvanlar.Find(model.HayvanId).HayvanOlumTarihi,
                CinsId = _context.Hayvanlar.Find(model.HayvanId).CinsId,
                TurId = _context.Hayvanlar.Find(model.HayvanId).TurId,
                RenkId = _context.Hayvanlar.Find(model.HayvanId).RenkId,
                cinsi = _context.Cinsler
                    .Where(c => c.CinsId == _context.Hayvanlar.Find(model.HayvanId).CinsId)
                    .Select(c => c.CinsAdi)
                    .FirstOrDefault(),
                turu = _context.Turler
                    .Where(t => t.TurId == _context.Hayvanlar.Find(model.HayvanId).TurId)
                    .Select(t => t.TurAdi)
                    .FirstOrDefault(),
                rengi = _context.Renkler
                    .Where(r => r.RenkId == _context.Hayvanlar.Find(model.HayvanId).RenkId)
                    .Select(r => r.RenkAdi)
                    .FirstOrDefault(),
                isDeath = _context.Hayvanlar.Find(model.HayvanId).HayvanOlumTarihi == null ? false : true,
                CinsAdlari = cinsAdlari,
                RenkAdlari = renkAdlari,
                HayvanAnneId = _context.Hayvanlar.Find(model.HayvanId).HayvanAnneId,
                HayvanBabaId = _context.Hayvanlar.Find(model.HayvanId).HayvanBabaId,
                HayvanAnneList = Anneler.Select(h => new SelectListItem
                {
                    Text = $"{h.SahipTckn.Substring(0, 3) + new string('*', Math.Max(h.SahipTckn.Length - 6, 0)) + h.SahipTckn.Substring(h.SahipTckn.Length - 3)} " +
                    $"{h.SahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.SahipAdSoyad.Length - 4, 0)) + h.SahipAdSoyad.Substring(h.SahipAdSoyad.Length - 2)} - " +
                    $"{h.HayvanId} {h.HayvanAdi} {h.cinsi} {h.turu}",
                    Value = h.HayvanId.ToString()
                }).ToList(),
                HayvanBabaList = Babalar.Select(h => new SelectListItem
                {
                    Text = $"{h.SahipTckn.Substring(0, 3) + new string('*', Math.Max(h.SahipTckn.Length - 6, 0)) + h.SahipTckn.Substring(h.SahipTckn.Length - 3)} " +
                    $"{h.SahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.SahipAdSoyad.Length - 4, 0)) + h.SahipAdSoyad.Substring(h.SahipAdSoyad.Length - 2)} - " +
                    $"{h.HayvanId} {h.HayvanAdi} {h.cinsi} {h.turu}",
                    Value = h.HayvanId.ToString()
                }).ToList(),
                CinsiyetList = cinsiyet,
                Sahip = _userManager.GetUserAsync(User).Result,
                SahiplikTarihi = _context.SahipHayvanlar
                    .Where(x => x.HayvanId == model.HayvanId && x.Sahip.InsanTckn == SahipTCKN)
                    .Select(x => x.SahiplikTarihi)
                    .FirstOrDefault(),
                SahiplikCikisTarihi = _context.SahipHayvanlar
                    .Where(x => x.HayvanId == model.HayvanId && x.Sahip.InsanTckn == SahipTCKN)
                    .Select(x => x.SahiplikCikisTarihi)
                    .FirstOrDefault(),
                ImgUrl = _context.Hayvanlar.Find(model.HayvanId).ImgUrl,
                Signature = model.Signature

            };

            EditHayvanValidator validator = new EditHayvanValidator();
            ValidationResult result = validator.Validate(model);




            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View("EditAnimal", returnModel);
            }

            var hayvan = _context.Hayvanlar.FindAsync(model.HayvanId).Result;
            if (model.PhotoOption == "changePhoto" && model.filePhoto != null)
            {

                var dosyaUzantısı = Path.GetExtension(model.filePhoto.FileName);

                var dosyaAdi = string.Format($"{Guid.NewGuid()}{dosyaUzantısı}");
                var hayvanKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\animals", hayvan.HayvanId.ToString());

                if (!Directory.Exists(hayvanKlasoru))
                {
                    Directory.CreateDirectory(hayvanKlasoru);
                }
                var eskiFotograflar = Directory.GetFiles(hayvanKlasoru);
                if (eskiFotograflar.Length > 0)
                {
                    foreach (var eskiFotograf in eskiFotograflar)
                    {
                        System.IO.File.Delete(eskiFotograf);
                    }
                }


                var filePath = Path.Combine(hayvanKlasoru, dosyaAdi);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.filePhoto.CopyToAsync(stream);
                }

                // Web URL'sini oluşturma
                var fileUrl = $"/img/animals/{hayvan.HayvanId}/{dosyaAdi}";

                hayvan.ImgUrl = fileUrl;

            }
            else if (model.PhotoOption == "changePhoto" && model.filePhoto == null)
            {
                hayvan.ImgUrl = _context.Hayvanlar.Find(model.HayvanId).ImgUrl;
            }
            else if (model.PhotoOption == "deletePhoto")
            {
                hayvan.ImgUrl = null;
            }
            else if (model.PhotoOption == "keepPhoto")
            {
                hayvan.ImgUrl = _context.Hayvanlar.Find(model.HayvanId).ImgUrl;
            }


            hayvan.HayvanAdi = model.HayvanAdi.ToUpper();
            hayvan.CinsId = model.CinsId;
            hayvan.TurId = model.TurId;
            hayvan.RenkId = model.RenkId;
            hayvan.HayvanCinsiyet = model.HayvanCinsiyet;
            hayvan.HayvanKilo = model.HayvanKilo;
            hayvan.HayvanDogumTarihi = model.HayvanDogumTarihi;
            hayvan.HayvanOlumTarihi = model.HayvanOlumTarihi;
            hayvan.HayvanAnneId = model.HayvanAnneId;
            hayvan.HayvanBabaId = model.HayvanBabaId;
            _context.Hayvanlar.Update(hayvan);

            var SahipHayvan = _context.SahipHayvanlar
                .Where(sh => sh.HayvanId == model.HayvanId && sh.Sahip.InsanTckn == _userManager.GetUserAsync(User).Result.InsanTckn)
                .FirstOrDefault();

            SahipHayvan.HayvanId = model.HayvanId;
            SahipHayvan.Sahip.InsanTckn = _userManager.GetUserAsync(User).Result.InsanTckn;
            SahipHayvan.SahiplikTarihi = model.SahiplikTarihi;
            SahipHayvan.SahiplikCikisTarihi = model.SahiplikCikisTarihi;

            _context.SahipHayvanlar.Update(SahipHayvan);
            _context.SaveChanges();
            returnModel.ImgUrl = hayvan.ImgUrl;
            TempData["Edit"] = "Hayvan bilgileri başarıyla güncellendi.";

            return View("EditAnimal", returnModel);

        }

        [HttpGet]
        public IActionResult AddSahip(int hayvanId)
        {
            var hayvan = _context.Hayvanlar.Find(hayvanId);
            var user = _userManager.GetUserAsync(User).Result;

            var userHayvan = _context.SahipHayvanlar
                .Where(sh => sh.HayvanId == hayvanId && sh.Sahip.InsanTckn == user.InsanTckn)
                .FirstOrDefault();

            if (userHayvan == null)
            {
                return View("BadRequest");
            }
            var signature = Signature.CreateSignature(hayvanId, user.Id);
            var model = new AddNewSahipViewModel()
            {
                HayvanId = hayvanId,
                HayvanAdi = hayvan.HayvanAdi,
                ImgUrl = hayvan.ImgUrl,
                HayvanDogumTarihi = hayvan.HayvanDogumTarihi,
                HayvanOlumTarihi = hayvan.HayvanOlumTarihi,
                renkAdi = _context.Renkler.Where(r => r.RenkId == hayvan.RenkId).Select(r => r.RenkAdi).FirstOrDefault(),
                cinsAdi = _context.Cinsler.Where(c => c.CinsId == hayvan.CinsId).Select(c => c.CinsAdi).FirstOrDefault(),
                turAdi = _context.Turler.Where(t => t.TurId == hayvan.TurId).Select(t => t.TurAdi).FirstOrDefault(),
                HayvanCinsiyet = hayvan.HayvanCinsiyet,
                HayvanKilo = hayvan.HayvanKilo,
                userTCKN = user.InsanTckn,
                Signature = signature
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSahip(AddNewSahipViewModel model)
        {

            if (!Signature.VerifySignature(model.HayvanId, model.Sahip.Id, model.Signature))
            {
                return View("Badrequest");
            }

            AddYeniSahipValidator validator = new AddYeniSahipValidator();
            ValidationResult result = validator.Validate(model);

            var hayvan = _context.Hayvanlar.Find(model.HayvanId);

            var returnModel = new AddNewSahipViewModel()
            {
                HayvanId = model.HayvanId,
                HayvanAdi = hayvan.HayvanAdi,
                ImgUrl = hayvan.ImgUrl,
                HayvanDogumTarihi = hayvan.HayvanDogumTarihi,
                HayvanOlumTarihi = hayvan.HayvanOlumTarihi,
                renkAdi = _context.Renkler.Where(r => r.RenkId == hayvan.RenkId).Select(r => r.RenkAdi).FirstOrDefault(),
                cinsAdi = _context.Cinsler.Where(c => c.CinsId == hayvan.CinsId).Select(c => c.CinsAdi).FirstOrDefault(),
                turAdi = _context.Turler.Where(t => t.TurId == hayvan.TurId).Select(t => t.TurAdi).FirstOrDefault(),
                HayvanCinsiyet = hayvan.HayvanCinsiyet,
                HayvanKilo = hayvan.HayvanKilo,
                userTCKN = model.userTCKN,
                Signature = model.Signature,
                yeniSahipTCKN = ""
            };

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(returnModel);
            }

            var yeniSahip = _context.Users.Where(u => u.InsanTckn == model.yeniSahipTCKN).FirstOrDefault();
            var acceptUrl = Url.Action("EmailConfirmYeniSahip", "Animal", new { hayvanId = model.HayvanId, yeniSahipTCKN = model.yeniSahipTCKN, imza = Signature.CreateSignature(model.HayvanId, model.Sahip.Id) }, Request.Scheme, Request.Host.Value);
            var declineUrl = Url.Action("EmailRejectYeniSahip", "Animal", new { hayvanId = model.HayvanId, yeniSahipTCKN = model.yeniSahipTCKN, imza = Signature.CreateSignature(model.HayvanId, model.Sahip.Id) }, Request.Scheme, Request.Host.Value);
            var cinsiyet = hayvan.HayvanCinsiyet == "D" ? "Dişi" : "Erkek";
            var dogumTarihi = hayvan.HayvanDogumTarihi.ToString("dd-MM-yyyy");
            var olumTarihi = hayvan.HayvanOlumTarihi != null ? hayvan.HayvanOlumTarihi?.ToString("dd-MM-yyyy") : "Hayatta";
            var sahipAdSoyad = _userManager.GetUserAsync(User).Result.InsanAdi + " " + _userManager.GetUserAsync(User).Result.InsanSoyadi;
            var turAdi = _context.Turler.Where(t => t.TurId == hayvan.TurId).Select(t => t.TurAdi).FirstOrDefault();
            var cinsAdi = _context.Cinsler.Where(c => c.CinsId == hayvan.CinsId).Select(c => c.CinsAdi).FirstOrDefault();
            var renkAdi = _context.Renkler.Where(r => r.RenkId == hayvan.RenkId).Select(r => r.RenkAdi).FirstOrDefault();
            var hayvanAdi = hayvan.HayvanAdi;
            // Uygulamanızın geçerli URL'sini almak için
            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            string resimYolu = hayvan.ImgUrl != null ? hayvan.ImgUrl : Url.Content("/img/animal.png");
            string imgUrl = $"{baseUrl}{resimYolu}";


            string mailBody = $@"
            <!DOCTYPE html>
            <html>
            <head>

             <style>
                  body {{
                      font-family: Arial, sans-serif;
                      color: #333;
                      margin: 0;
                      padding: 0;
                      font-size: 1.2rem;
                      background-color: #f8f9fa;
                  }}
                  h2 {{font-size: 1.8rem;
                  }}
                  h3 {{font-size: 1.6rem;
                  }}
                  h6 {{font-size: 1.2rem;
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
                  .btn {{
                      display: inline-block;
                      padding: 10px 20px;
                      color: white !important;
                      text-decoration: none;
                      border-radius: 5px;
                      font-weight: bold;
                      text-align: center;
                      margin-top: 10px;
                      background-color: #6c757d;
                      border: none;
                  }}
                  .btn:hover {{
                      background-color: #5a6268;
                  }}
                  .btn-secondary {{
                      background-color: #6c757d;
                      margin-right: 10px;
                  }}
                  .img-container {{
                      text-align: center;
                      margin-bottom: 20px;
                  }}
                  .img-container img {{
                      border-radius: 8px;
                      max-width: 100%;
                      height: auto;
                      object-fit: cover;
                  }}
                  .details {{
                      margin-top: 20px;
                  }}
                  .details h6 {{
                      margin: 0 0 10px 0;
                  }}
                  .footer {{
                      text-align: center;
                      padding: 20px;
                      font-size: 12px;
                      color: #888;
                      border-top: 1px solid #e9ecef;
                      margin-top: 20px;
                  }}
            </style>
            </head>
            <body>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h2>Hayvan Sahiplendirme Talebi</h2>
                    </div>
                    <div class='content'>
                        <h3>Merhaba {yeniSahip.InsanAdi} {yeniSahip.InsanSoyadi},</h3>
                        <p>{sahipAdSoyad} isimli kullanıcı size bir hayvan sahiplendirme talebi gönderdi. Aşağıda hayvana ait bilgileri bulabilirsiniz:</p>
                        
                        <div class='img-container'>
                            <img src='{imgUrl}' alt='{hayvanAdi}' />
                        </div>

                        <div class='details'>
                            <h6><strong>Hayvan Adı:</strong> {hayvanAdi}</h6>
                            <h6><strong>Türü:</strong> {turAdi}</h6>
                            <h6><strong>Cinsi:</strong> {cinsAdi}</h6>
                            <h6><strong>Rengi:</strong> {renkAdi}</h6>
                            <h6><strong>Kilosu:</strong> {hayvan.HayvanKilo} kg</h6>
                            <h6><strong>Cinsiyeti:</strong> {cinsiyet}</h6>
                            <h6><strong>Doğum Tarihi:</strong> {dogumTarihi}</h6>
                            <h6><strong>Ölüm Tarihi:</strong> {olumTarihi}</h6>
                        </div>

                        <div>
                            <a href='{acceptUrl}' class='btn btn-secondary'>Kabul Et</a>
                            <a href='{declineUrl}' class='btn btn-secondary'>Red Et</a>
                        </div>
                    </div>
                    <div class='footer'>
                        <p>Bu e-postayı aldıysanız ancak bu talebi yapmadıysanız, lütfen bizimle iletişime geçin.</p>
                    </div>
                </div>
            </body>
            </html>
            ";


            try
            {
                _emailSender.SendEmailAsync(yeniSahip.Email, "Hayvan Sahiplenme", mailBody);
                ViewBag.Mail = "Yeni sahip ekleme talebi, kişinin mail adresine başarıyla gönderildi.";

            }
            catch (Exception ex)
            {
                ViewBag.MailHata = "Mail gönderilirken bir hata oluştu. Hata: " + ex.Message;
                return View(returnModel);

            }

            return View(returnModel);
        }

        public async Task<IActionResult> EmailConfirmYeniSahip(int hayvanId, int yeniSahipId, string imza)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user.Id != yeniSahipId)
            {
                return View("BadRequest");
            }
            else
            {
                if (!Signature.VerifySignature(hayvanId, yeniSahipId, imza))
                {
                    return View("BadRequest");
                }

                var hayvan = _context.Hayvanlar.Find(hayvanId);
                var yeniSahip = _context.Users.Where(u => u.Id == yeniSahipId).FirstOrDefault();
                if (!_context.SahipHayvanlar.Any(x => x.HayvanId == hayvanId && x.SahipId == yeniSahipId))
                {
                    _context.SahipHayvanlar.Add(new SahipHayvan
                    {
                        HayvanId = hayvanId,
                        SahipId = yeniSahipId,
                        SahiplikTarihi = DateTime.Now
                    });
                    if (_context.SaveChanges() > 0)
                    {
                        TempData["YeniHayvanEklendi"] = $"{hayvan.HayvanAdi.ToUpper()} isimli yeni evcil hayvanınız hesabınıza başarı ile eklendi.";
                    }
                    else
                    {
                        TempData["YeniHayvanEklendiHata"] = $"{hayvan.HayvanAdi.ToUpper()} isimli evcil hayvan hesabınıza eklenirken bir hata oluştu.";
                    }
                }
                else
                {
                    TempData["HayvanSahibisiniz"] = $"{hayvan.HayvanAdi.ToUpper()} isimli evcil hayvanın zaten sahibisiniz.";
                }

                return RedirectToAction("Information", "User");
            }

        }

        public async Task<IActionResult> EmailRejectYeniSahip(int hayvanId, int yeniSahipId, string imza)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user.Id != yeniSahipId)
            {
                return View("BadRequest");
            }
            else
            {
                if (!Signature.VerifySignature(hayvanId, yeniSahipId, imza))
                {
                    return View("BadRequest");
                }
                var hayvan = _context.Hayvanlar.Find(hayvanId);

                TempData["EvcilHayvanRed"] = $"{hayvan.HayvanAdi.ToUpper()} isimli evcil hayvanı hesabınıza eklemek istemediniz.";
                return RedirectToAction("Information", "User");
            }
        }
    }
}
