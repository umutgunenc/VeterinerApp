using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
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
                    Text = $"{h.SahipTckn} {h.SahipAdSoyad} - {h.HayvanId} {h.HayvanAdi} {h.cinsi} {h.turu}",
                    Value = h.HayvanId.ToString()
                }).ToList(),
                HayvanBabaList = Babalar.Select(h => new SelectListItem
                {
                    Text = $"{h.SahipTckn} {h.SahipAdSoyad} - {h.HayvanId} {h.HayvanAdi} {h.cinsi} {h.turu} ",
                    Value = h.HayvanId.ToString()
                }).ToList(),
                CinsiyetList = cinsiyet,
                Sahip = _userManager.GetUserAsync(User).Result
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
        public IActionResult AddAnimal(AddAnimalViewModel model)
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
                        Text = $"{h.SahipTckn} {h.SahipAdSoyad} - {h.HayvanId} {h.HayvanAdi} {h.cinsi} {h.turu}",
                        Value = h.HayvanId.ToString()
                    }).ToList(),
                    HayvanBabaList = Babalar.Select(h => new SelectListItem
                    {
                        Text = $"{h.SahipTckn} {h.SahipAdSoyad} - {h.HayvanId} {h.HayvanAdi} {h.cinsi} {h.turu} ",
                        Value = h.HayvanId.ToString()
                    }).ToList(),
                    CinsiyetList = cinsiyet,
                    Sahip = _userManager.GetUserAsync(User).Result
                };

                return View(returnModel);
            }
            Hayvan hayvan = new Hayvan();

            hayvan.HayvanAdi = model.HayvanAdi;
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
            sahipHayvan.SahiplikTarihi = DateTime.Now;
            sahipHayvan.SahipTckn = _userManager.GetUserAsync(User).Result.InsanTckn;
            sahipHayvan.HayvanId = hayvan.HayvanId;

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
}
}
