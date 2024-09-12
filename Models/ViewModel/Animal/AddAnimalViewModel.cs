﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Enum;

namespace VeterinerApp.Models.ViewModel.Animal
{
    public class AddAnimalViewModel : Hayvan
    {
        private readonly VeterinerDBContext _context;
        public AddAnimalViewModel()
        {
            
        }
        public AddAnimalViewModel(VeterinerDBContext context)
        {
            _context = context;
        }

        public string CinsAdi { get; set; }
        public string TurAdi { get; set; }
        public string RenkAdi { get; set; }
        public int SecilenTurId { get; set; }
        public int SecilenCinsId { get; set; }
        public List<SelectListItem> TurlerListesi { get; set; }
        public List<SelectListItem> CinslerListesi { get; set; }
        public List<SelectListItem> RenklerListesi { get; set; }
        public List<SelectListItem> CinsiyetListesi { get; set; }
        public List<SelectListItem> HayvanAnneListesi { get; set; }
        public List<SelectListItem> HayvanBabaListesi { get; set; }
        public IFormFile filePhoto { get; set; }
        public string PhotoOption { get; set; }

        public DateTime SahiplenmeTarihi { get; set; }

        public List<SelectListItem> AnnelerListesiOlustur()
        {
            var hayvanlar = _context.Hayvanlar.Select(h => new
            {
                sahipTckn = _context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanTckn).FirstOrDefault(),
                sahipAdSoyad = _context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanAdi + " " + sh.AppUser.InsanSoyadi).FirstOrDefault(),
                h.HayvanAdi,
                h.HayvanId,
                h.HayvanCinsiyet,
                CinsAdi = _context.Cinsler.Where(c => c.CinsId == h.CinsTur.CinsId).Select(c => c.CinsAdi).FirstOrDefault(),
                TurAdi = _context.Turler.Where(t => t.TurId == h.CinsTur.TurId).Select(t => t.TurAdi).FirstOrDefault(),
                RenkAdi=_context.Renkler.Where(r=>r.RenkId==h.RenkId).Select(r=>r.RenkAdi).FirstOrDefault(),
                hayvanDogumTarihi = h.HayvanDogumTarihi.ToString("dd-MM-yyyy")
            }).ToList();

            return hayvanlar.Where(h => h.HayvanCinsiyet == Cinsiyet.Dişi).Select(h => new SelectListItem
            {
                Text = $"{h.sahipTckn.Substring(0, 3) + new string('*', Math.Max(h.sahipTckn.Length - 6, 0)) + h.sahipTckn.Substring(h.sahipTckn.Length - 3)} " +
                                    $"{h.sahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.sahipAdSoyad.Length - 4, 0)) + h.sahipAdSoyad.Substring(h.sahipAdSoyad.Length - 2)} - " +
                                    $"{h.HayvanAdi} {h.CinsAdi} {h.TurAdi} {h.RenkAdi} {h.hayvanDogumTarihi}",
                Value = h.HayvanId.ToString()
            }).ToList();
        }
        public List<SelectListItem> BabalarListesiOlustur()
        {
            var hayvanlar = _context.Hayvanlar.Select(h => new
            {
                sahipTckn = _context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanTckn).FirstOrDefault(),
                sahipAdSoyad = _context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanAdi + " " + sh.AppUser.InsanSoyadi).FirstOrDefault(),
                h.HayvanAdi,
                h.HayvanId,
                h.HayvanCinsiyet,
                CinsAdi = _context.Cinsler.Where(c => c.CinsId == h.CinsTur.CinsId).Select(c => c.CinsAdi).FirstOrDefault(),
                TurAdi = _context.Turler.Where(t => t.TurId == h.CinsTur.TurId).Select(t => t.TurAdi).FirstOrDefault(),
                RenkAdi = _context.Renkler.Where(r => r.RenkId == h.RenkId).Select(r => r.RenkAdi).FirstOrDefault(),
                hayvanDogumTarihi = h.HayvanDogumTarihi.ToString("dd-MM-yyyy")
            }).ToList();

            return hayvanlar.Where(h => h.HayvanCinsiyet == Cinsiyet.Erkek).Select(h => new SelectListItem
            {
                Text = $"{h.sahipTckn.Substring(0, 3) + new string('*', Math.Max(h.sahipTckn.Length - 6, 0)) + h.sahipTckn.Substring(h.sahipTckn.Length - 3)} " +
                                    $"{h.sahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.sahipAdSoyad.Length - 4, 0)) + h.sahipAdSoyad.Substring(h.sahipAdSoyad.Length - 2)} - " +
                                    $"{h.HayvanAdi} {h.CinsAdi} {h.TurAdi} {h.RenkAdi} {h.hayvanDogumTarihi}",
                Value = h.HayvanId.ToString()
            }).ToList();
        }
        public List<SelectListItem> CinsiyetListesiOlustur()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Erkek", Value = "E" },
                new SelectListItem { Text = "Dişi", Value = "D" }
            };
        }
        public List<SelectListItem> TurListesiOlustur()
        {
            return _context.Turler.Select(t => new SelectListItem
            {
                Text = t.TurAdi,
                Value = t.TurId.ToString()
            }).ToList();
        }
        public List<SelectListItem> CinsListesiOlustur()
        {
            return _context.Cinsler.Select(c => new SelectListItem
            {
                Text = c.CinsAdi,
                Value = c.CinsId.ToString()
            }).ToList();
        }
        public List<SelectListItem> RenkListesiOlustur()
        {
            return _context.Renkler.Select(Renk => new SelectListItem
            {
                Value= Renk.RenkId.ToString(),
                Text = Renk.RenkAdi
            }).ToList();
        }
    }
}
