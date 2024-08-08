using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.Animal;
using VeterinerApp.Models.ViewModel.Animal;

namespace VeterinerApp.Controllers
{
    [Authorize]
    public class AnimalController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly VeterinerContext _context;
        public AnimalController(UserManager<AppUser> usermanager, VeterinerContext context)
        {
            _userManager = usermanager;
            _context = context;
        }

        [HttpGet]
        public IActionResult AddAnimal()
        {
            var cinsAdlari = _context.Cins.Select(c => new SelectListItem
            {
                Text = c.cins,
                Value = c.Id.ToString()
            }).ToList();

            var renkAdlari = _context.Renks.Select(r => new SelectListItem
            {
                Text = r.renk,
                Value = r.Id.ToString()
            }).ToList();

            var Anneler = _context.Hayvans
                .Where(h => h.HayvanCinsiyet == "D" || h.HayvanCinsiyet == "d")
                .Select(h => new AddAnimalViewModel
                {
                    HayvanAdi = h.HayvanAdi,
                    HayvanId = h.HayvanId,
                    rengi = _context.Renks
                        .Where(r => r.Id == h.RenkId)
                        .Select(r => r.renk)
                        .FirstOrDefault(),
                    cinsi = _context.Cins
                        .Where(c => c.Id == h.CinsId)
                        .Select(c => c.cins)
                        .FirstOrDefault(),
                    turu = _context.Turs
                        .Where(t => t.Id == h.TurId)
                        .Select(t => t.tur)
                        .FirstOrDefault(),
                    SahipTckn = _context.SahipHayvans
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.SahipTckn)
                        .FirstOrDefault(),
                    SahipAdSoyad = _context.Users
                        .Where(u => u.InsanTckn == _context.SahipHayvans
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.SahipTckn)
                        .FirstOrDefault())
                        .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                        .FirstOrDefault()
                })
                .ToList();

            var Babalar = _context.Hayvans
                .Where(h => h.HayvanCinsiyet == "E" || h.HayvanCinsiyet == "e")
                .Select(h => new AddAnimalViewModel
                {
                    HayvanAdi = h.HayvanAdi,
                    HayvanId = h.HayvanId,
                    rengi = _context.Renks
                        .Where(r => r.Id == h.RenkId)
                        .Select(r => r.renk)
                        .FirstOrDefault(),
                    cinsi = _context.Cins
                        .Where(c => c.Id == h.CinsId)
                        .Select(c => c.cins)
                        .FirstOrDefault(),
                    turu = _context.Turs
                        .Where(t => t.Id == h.TurId)
                        .Select(t => t.tur)
                        .FirstOrDefault(),
                    SahipTckn = _context.SahipHayvans
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.SahipTckn)
                        .FirstOrDefault(),
                    SahipAdSoyad = _context.Users
                        .Where(u => u.InsanTckn == _context.SahipHayvans
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.SahipTckn)
                        .FirstOrDefault())
                        .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                        .FirstOrDefault()
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

            };

            return View(model);

        }

        public JsonResult TurleriGetir(int cinsId)
        {
            var turAdlari = _context.TurCins
                .Where(tc => tc.CinsId == cinsId)
                .Join(_context.Turs,
                      tc => tc.TurId,
                      t => t.Id,
                      (tc, t) => new SelectListItem
                      {
                          Text = t.tur,
                          Value = t.Id.ToString()
                      }).ToList();

            return Json(turAdlari);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimal(AddAnimalViewModel model, IFormFile filePhoto)
        {

            HayvanValidator validator = new HayvanValidator(_context);
            var result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                var cinsAdlari = _context.Cins.Select(c => new SelectListItem
                {
                    Text = c.cins,
                    Value = c.Id.ToString()
                }).ToList();

                var renkAdlari = _context.Renks.Select(r => new SelectListItem
                {
                    Text = r.renk,
                    Value = r.Id.ToString()
                }).ToList();

                var Anneler = _context.Hayvans
                    .Where(h => h.HayvanCinsiyet == "D" || h.HayvanCinsiyet == "d")
                    .Select(h => new AddAnimalViewModel
                    {
                        HayvanAdi = h.HayvanAdi,
                        HayvanId = h.HayvanId,
                        rengi = _context.Renks
                            .Where(r => r.Id == h.RenkId)
                            .Select(r => r.renk)
                            .FirstOrDefault(),
                        cinsi = _context.Cins
                            .Where(c => c.Id == h.CinsId)
                            .Select(c => c.cins)
                            .FirstOrDefault(),
                        turu = _context.Turs
                            .Where(t => t.Id == h.TurId)
                            .Select(t => t.tur)
                            .FirstOrDefault(),
                        SahipTckn = _context.SahipHayvans
                            .Where(sh => sh.HayvanId == h.HayvanId)
                            .Select(s => s.SahipTckn)
                            .FirstOrDefault(),
                        SahipAdSoyad = _context.Users
                            .Where(u => u.InsanTckn == _context.SahipHayvans
                            .Where(sh => sh.HayvanId == h.HayvanId)
                            .Select(s => s.SahipTckn)
                            .FirstOrDefault())
                            .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                            .FirstOrDefault()


                    })
                    .ToList();

                var Babalar = _context.Hayvans
                    .Where(h => h.HayvanCinsiyet == "E" || h.HayvanCinsiyet == "e")
                    .Select(h => new AddAnimalViewModel
                    {
                        HayvanAdi = h.HayvanAdi,
                        HayvanId = h.HayvanId,
                        rengi = _context.Renks
                            .Where(r => r.Id == h.RenkId)
                            .Select(r => r.renk)
                            .FirstOrDefault(),
                        cinsi = _context.Cins
                            .Where(c => c.Id == h.CinsId)
                            .Select(c => c.cins)
                            .FirstOrDefault(),
                        turu = _context.Turs
                            .Where(t => t.Id == h.TurId)
                            .Select(t => t.tur)
                            .FirstOrDefault(),
                        SahipTckn = _context.SahipHayvans
                            .Where(sh => sh.HayvanId == h.HayvanId)
                            .Select(s => s.SahipTckn)
                            .FirstOrDefault(),
                        SahipAdSoyad = _context.Users
                            .Where(u => u.InsanTckn == _context.SahipHayvans
                            .Where(sh => sh.HayvanId == h.HayvanId)
                            .Select(s => s.SahipTckn)
                            .FirstOrDefault())
                            .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                            .FirstOrDefault()
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
            _context.Hayvans.Add(hayvan);
            _context.SaveChanges();

            SahipHayvan sahipHayvan = new SahipHayvan();
            sahipHayvan.SahiplikTarihi = model.SahiplikTarihi;
            sahipHayvan.SahipTckn = _userManager.GetUserAsync(User).Result.InsanTckn;
            sahipHayvan.HayvanId = hayvan.HayvanId;


            if (filePhoto != null)
            {
                var dosyaUzantısı = Path.GetExtension(filePhoto.FileName);
                var dosyaAdi = string.Format($"{Guid.NewGuid()}{dosyaUzantısı}");
                var hayvanKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\animals", hayvan.HayvanId.ToString());

                if (!Directory.Exists(hayvanKlasoru))
                {
                    Directory.CreateDirectory(hayvanKlasoru);
                }

                var filePath = Path.Combine(hayvanKlasoru, dosyaAdi);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await filePhoto.CopyToAsync(stream);
                }

                // Web URL'sini oluşturma
                var fileUrl = $"/img/animals/{hayvan.HayvanId}/{dosyaAdi}";

                // Veritabanına URL'yi kaydetme
                hayvan.imgURl = fileUrl;
                await _context.SaveChangesAsync();
            }

            _context.SahipHayvans.Add(sahipHayvan);
            _context.SaveChanges();

            TempData["AddAnimal"] = $"{model.HayvanAdi} isimli hayvan başarıyla eklendi.";

            return RedirectToAction();

        }

        [HttpPost]
        public async Task<IActionResult> Information(int hayvanId)
        {
            var user = await _userManager.GetUserAsync(User);
            var userAnimalsIds = _context.SahipHayvans.Where(s => s.SahipTckn == user.InsanTckn).Select(h => h.HayvanId).ToList();
            var animal = await _context.Hayvans.FindAsync(hayvanId);

            if (userAnimalsIds.Contains(hayvanId))
            {
                var muayeneler = _context.Muayenes
                    .Where(muayene => muayene.HayvanId == hayvanId)
                    .Select(muayene => new
                    {
                        muayene.MuayeneId,
                        muayene.MuayeneTarihi,
                        muayene.HekimkTckn,
                        HekimAdi = _userManager.Users
                            .Where(hekim => hekim.InsanTckn == muayene.HekimkTckn)
                            .Select(hekim => hekim.InsanAdi + " " + hekim.InsanSoyadi)
                            .FirstOrDefault()
                    })
                    .ToList();

                var UserAnimal = new HayvanlarViewModel
                {
                    HayvanId = animal.HayvanId,
                    HayvanAdi = animal.HayvanAdi,
                    imgURL = animal.imgURl,
                    HayvanCinsiyet = animal.HayvanCinsiyet,
                    HayvanKilo = animal.HayvanKilo,
                    HayvanDogumTarihi = animal.HayvanDogumTarihi,
                    HayvanOlumTarihi = animal.HayvanOlumTarihi,
                    HayvanAnneAdi = _context.Hayvans.Where(ha => ha.HayvanId == animal.HayvanAnneId).Select(ha => ha.HayvanAdi).FirstOrDefault(),
                    HayvanBabaAdi = _context.Hayvans.Where(hb => hb.HayvanId == animal.HayvanBabaId).Select(hb => hb.HayvanAdi).FirstOrDefault(),
                    TurAdi = _context.Turs.Where(t => t.Id == animal.TurId).Select(t => t.tur).FirstOrDefault(),
                    CinsAdi = _context.Cins.Where(c => c.Id == animal.CinsId).Select(c => c.cins).FirstOrDefault(),
                    RenkAdi = _context.Renks.Where(r => r.Id == animal.RenkId).Select(r => r.renk).FirstOrDefault(),
                    SahiplikTarihi = _context.SahipHayvans
                        .Where(x => x.HayvanId == hayvanId && x.SahipTckn == user.InsanTckn)
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
            else
                return View("BadRequest");
        }

        [HttpPost]
        public IActionResult EditAnimal(int hayvanId)
        {
            var cinsAdlari = _context.Cins.Select(c => new SelectListItem
            {
                Text = c.cins,
                Value = c.Id.ToString()
            }).ToList();

            var renkAdlari = _context.Renks.Select(r => new SelectListItem
            {
                Text = r.renk,
                Value = r.Id.ToString()
            }).ToList();

            var Anneler = _context.Hayvans
                .Where(h => h.HayvanCinsiyet == "D" || h.HayvanCinsiyet == "d")
                .Select(h => new AddAnimalViewModel
                {
                    HayvanAdi = h.HayvanAdi,
                    HayvanId = h.HayvanId,
                    rengi = _context.Renks
                        .Where(r => r.Id == h.RenkId)
                        .Select(r => r.renk)
                        .FirstOrDefault(),
                    cinsi = _context.Cins
                        .Where(c => c.Id == h.CinsId)
                        .Select(c => c.cins)
                        .FirstOrDefault(),
                    turu = _context.Turs
                        .Where(t => t.Id == h.TurId)
                        .Select(t => t.tur)
                        .FirstOrDefault(),
                    SahipTckn = _context.SahipHayvans
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.SahipTckn)
                        .FirstOrDefault(),
                    SahipAdSoyad = _context.Users
                        .Where(u => u.InsanTckn == _context.SahipHayvans
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.SahipTckn)
                        .FirstOrDefault())
                        .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                        .FirstOrDefault()
                })
                .ToList();

            var Babalar = _context.Hayvans
                .Where(h => h.HayvanCinsiyet == "E" || h.HayvanCinsiyet == "e")
                .Select(h => new AddAnimalViewModel
                {
                    HayvanAdi = h.HayvanAdi,
                    HayvanId = h.HayvanId,
                    rengi = _context.Renks
                        .Where(r => r.Id == h.RenkId)
                        .Select(r => r.renk)
                        .FirstOrDefault(),
                    cinsi = _context.Cins
                        .Where(c => c.Id == h.CinsId)
                        .Select(c => c.cins)
                        .FirstOrDefault(),
                    turu = _context.Turs
                        .Where(t => t.Id == h.TurId)
                        .Select(t => t.tur)
                        .FirstOrDefault(),
                    SahipTckn = _context.SahipHayvans
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.SahipTckn)
                        .FirstOrDefault(),
                    SahipAdSoyad = _context.Users
                        .Where(u => u.InsanTckn == _context.SahipHayvans
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.SahipTckn)
                        .FirstOrDefault())
                        .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                        .FirstOrDefault()
                })
                .ToList();

            var cinsiyet = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Erkek", Value = "E" },
                    new SelectListItem { Text = "Dişi", Value = "D" }
                };

            var SahipTCKN = _userManager.GetUserAsync(User).Result.InsanTckn;
            EditAnimalViewModel model = new()
            {
                HayvanId = hayvanId,
                HayvanAdi = _context.Hayvans.Find(hayvanId).HayvanAdi.ToUpper(),
                HayvanCinsiyet = _context.Hayvans.Find(hayvanId).HayvanCinsiyet,
                HayvanKilo = _context.Hayvans.Find(hayvanId).HayvanKilo,
                HayvanDogumTarihi = _context.Hayvans.Find(hayvanId).HayvanDogumTarihi,
                HayvanOlumTarihi = _context.Hayvans.Find(hayvanId).HayvanOlumTarihi,
                CinsId = _context.Hayvans.Find(hayvanId).CinsId,
                TurId = _context.Hayvans.Find(hayvanId).TurId,
                RenkId = _context.Hayvans.Find(hayvanId).RenkId,
                cinsi = _context.Cins
                    .Where(c => c.Id == _context.Hayvans.Find(hayvanId).CinsId)
                    .Select(c => c.cins)
                    .FirstOrDefault(),
                turu = _context.Turs
                    .Where(t => t.Id == _context.Hayvans.Find(hayvanId).TurId)
                    .Select(t => t.tur)
                    .FirstOrDefault(),
                rengi = _context.Renks
                    .Where(r => r.Id == _context.Hayvans.Find(hayvanId).RenkId)
                    .Select(r => r.renk)
                    .FirstOrDefault(),

                isDeath = _context.Hayvans.Find(hayvanId).HayvanOlumTarihi == null ? false : true,

                HayvanAnneId = _context.Hayvans.Find(hayvanId).HayvanAnneId,
                HayvanBabaId = _context.Hayvans.Find(hayvanId).HayvanBabaId,

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
                Sahip = _userManager.GetUserAsync(User).Result,
                SahiplikTarihi = _context.SahipHayvans
                    .Where(x => x.HayvanId == hayvanId && x.SahipTckn == SahipTCKN)
                    .Select(x => x.SahiplikTarihi)
                    .FirstOrDefault(),
                SahiplikCikisTarihi = _context.SahipHayvans
                    .Where(x => x.HayvanId == hayvanId && x.SahipTckn == SahipTCKN)
                    .Select(x => x.SahiplikCikisTarihi)
                    .FirstOrDefault(),

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit(EditAnimalViewModel model)
        {
            EditHayvanValidator validator = new EditHayvanValidator(_context);
            ValidationResult result = validator.Validate(model);

            var cinsAdlari = _context.Cins.Select(c => new SelectListItem
            {
                Text = c.cins,
                Value = c.Id.ToString()
            }).ToList();

            var renkAdlari = _context.Renks.Select(r => new SelectListItem
            {
                Text = r.renk,
                Value = r.Id.ToString()
            }).ToList();

            var Anneler = _context.Hayvans
                .Where(h => h.HayvanCinsiyet == "D" || h.HayvanCinsiyet == "d")
                .Select(h => new AddAnimalViewModel
                {
                    HayvanAdi = h.HayvanAdi,
                    HayvanId = h.HayvanId,
                    rengi = _context.Renks
                        .Where(r => r.Id == h.RenkId)
                        .Select(r => r.renk)
                        .FirstOrDefault(),
                    cinsi = _context.Cins
                        .Where(c => c.Id == h.CinsId)
                        .Select(c => c.cins)
                        .FirstOrDefault(),
                    turu = _context.Turs
                        .Where(t => t.Id == h.TurId)
                        .Select(t => t.tur)
                        .FirstOrDefault(),
                    SahipTckn = _context.SahipHayvans
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.SahipTckn)
                        .FirstOrDefault(),
                    SahipAdSoyad = _context.Users
                        .Where(u => u.InsanTckn == _context.SahipHayvans
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.SahipTckn)
                        .FirstOrDefault())
                        .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                        .FirstOrDefault()
                })
                .ToList();

            var Babalar = _context.Hayvans
                .Where(h => h.HayvanCinsiyet == "E" || h.HayvanCinsiyet == "e")
                .Select(h => new AddAnimalViewModel
                {
                    HayvanAdi = h.HayvanAdi,
                    HayvanId = h.HayvanId,
                    rengi = _context.Renks
                        .Where(r => r.Id == h.RenkId)
                        .Select(r => r.renk)
                        .FirstOrDefault(),
                    cinsi = _context.Cins
                        .Where(c => c.Id == h.CinsId)
                        .Select(c => c.cins)
                        .FirstOrDefault(),
                    turu = _context.Turs
                        .Where(t => t.Id == h.TurId)
                        .Select(t => t.tur)
                        .FirstOrDefault(),
                    SahipTckn = _context.SahipHayvans
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.SahipTckn)
                        .FirstOrDefault(),
                    SahipAdSoyad = _context.Users
                        .Where(u => u.InsanTckn == _context.SahipHayvans
                        .Where(sh => sh.HayvanId == h.HayvanId)
                        .Select(s => s.SahipTckn)
                        .FirstOrDefault())
                        .Select(u => u.InsanAdi + " " + u.InsanSoyadi)
                        .FirstOrDefault()
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
                HayvanAdi = _context.Hayvans.Find(model.HayvanId).HayvanAdi.ToUpper(),
                HayvanCinsiyet = _context.Hayvans.Find(model.HayvanId).HayvanCinsiyet,
                HayvanKilo = _context.Hayvans.Find(model.HayvanId).HayvanKilo,
                HayvanDogumTarihi = _context.Hayvans.Find(model.HayvanId).HayvanDogumTarihi,
                HayvanOlumTarihi = _context.Hayvans.Find(model.HayvanId).HayvanOlumTarihi,
                CinsId = _context.Hayvans.Find(model.HayvanId).CinsId,
                TurId = _context.Hayvans.Find(model.HayvanId).TurId,
                RenkId = _context.Hayvans.Find(model.HayvanId).RenkId,
                cinsi = _context.Cins
                    .Where(c => c.Id == _context.Hayvans.Find(model.HayvanId).CinsId)
                    .Select(c => c.cins)
                    .FirstOrDefault(),
                turu = _context.Turs
                    .Where(t => t.Id == _context.Hayvans.Find(model.HayvanId).TurId)
                    .Select(t => t.tur)
                    .FirstOrDefault(),
                rengi = _context.Renks
                    .Where(r => r.Id == _context.Hayvans.Find(model.HayvanId).RenkId)
                    .Select(r => r.renk)
                    .FirstOrDefault(),

                isDeath = _context.Hayvans.Find(model.HayvanId).HayvanOlumTarihi == null ? false : true,

                CinsAdlari = cinsAdlari,
                RenkAdlari = renkAdlari,

                HayvanAnneId = _context.Hayvans.Find(model.HayvanId).HayvanAnneId,
                HayvanBabaId = _context.Hayvans.Find(model.HayvanId).HayvanBabaId,

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
                SahiplikTarihi = _context.SahipHayvans
                    .Where(x => x.HayvanId == model.HayvanId && x.SahipTckn == SahipTCKN)
                    .Select(x => x.SahiplikTarihi)
                    .FirstOrDefault(),
                SahiplikCikisTarihi = _context.SahipHayvans
                    .Where(x => x.HayvanId == model.HayvanId && x.SahipTckn == SahipTCKN)
                    .Select(x => x.SahiplikCikisTarihi)
                    .FirstOrDefault(),

            };

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);

                }

                return View("EditAnimal", returnModel);
            }
            var hayvan = _context.Hayvans.Find(model.HayvanId);
            if (model.PhotoOption == "changePhoto")
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

                hayvan.imgURl = fileUrl;

            }
            else if (model.PhotoOption == "useDefault") {
                hayvan.imgURl = null; 
            }
            else if (model.PhotoOption == "keepPhoto")
            {
                hayvan.imgURl = hayvan.imgURl;
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
            _context.Hayvans.Update(hayvan);

            var SahipHayvan = _context.SahipHayvans
                .Where(sh => sh.HayvanId == model.HayvanId && sh.SahipTckn == _userManager.GetUserAsync(User).Result.InsanTckn)
                .FirstOrDefault();

            SahipHayvan.HayvanId = model.HayvanId;
            SahipHayvan.SahipTckn = _userManager.GetUserAsync(User).Result.InsanTckn;
            SahipHayvan.SahiplikTarihi = model.SahiplikTarihi;
            SahipHayvan.SahiplikCikisTarihi = model.SahiplikCikisTarihi;

            _context.SahipHayvans.Update(SahipHayvan);
            _context.SaveChanges();

            TempData["Edit"] = "Hayvan bilgileri başarıyla güncellendi.";

            return View("EditAnimal", returnModel);

        }
    }
}
