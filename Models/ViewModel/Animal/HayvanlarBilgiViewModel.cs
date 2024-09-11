using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Animal
{
    public class HayvanlarBilgiViewModel :Hayvan
    {
        private readonly VeterinerDBContext _context;
        public HayvanlarBilgiViewModel()
        {
            
        }

        public HayvanlarBilgiViewModel(VeterinerDBContext context)
        {
            _context = context;
        }

        public string HayvanCinsiyet { get; set; }
        public double HayvanKilo { get; set; }
        public string TurAdi { get; set; }
        public string CinsAdi { get; set; }
        public string HayvanAnneAdi { get; set; }
        public string HayvanBabaAdi { get; set; }
        public string RenkAdi { get; set; }
        public DateTime SahiplenmeTarihi { get; set; }

        public List<MuayeneViewModel> Muayeneler { get; set; }
        private string HayvanAnneAdiGetir(Hayvan hayvan)
        {
            if (hayvan.HayvanAnneId.HasValue)
            {
                var anne = _context.Hayvanlar.Find(hayvan.HayvanAnneId);
                return anne.HayvanAdi;
            }
            return "Bilinmiyor";
        }
        private string HayvanBabaAdiGetir(Hayvan hayvan)
        {
            if (hayvan.HayvanBabaId.HasValue)
            {
                var baba = _context.Hayvanlar.Find(hayvan.HayvanBabaId);
                return baba.HayvanAdi;
            }
            return "Bilinmiyor";
        }       
        public HayvanlarBilgiViewModel HayvanBilgileriniGetir(Hayvan hayvan, AppUser user)
        {
            var muayeneler = _context.Muayeneler
            .Where(muayene => muayene.HayvanId == hayvan.HayvanId)
            .Select(muayene => new
            {
                muayene.MuayeneId,
                muayene.MuayeneTarihi,
                muayene.HekimId,
                HekimAdi = _context.AppUsers
                    .Where(hekim => hekim.Id == muayene.HekimId)
                    .Select(hekim => hekim.InsanAdi + " " + hekim.InsanSoyadi)
                    .FirstOrDefault()
            }).ToList();


            return new HayvanlarBilgiViewModel(_context)
            {
                HayvanId = hayvan.HayvanId,
                HayvanAdi = hayvan.HayvanAdi,
                HayvanAnneAdi = HayvanAnneAdiGetir(hayvan),
                HayvanBabaAdi = HayvanBabaAdiGetir(hayvan),
                HayvanCinsiyet = hayvan.HayvanCinsiyet,
                ImgUrl = hayvan.ImgUrl,
                HayvanDogumTarihi = hayvan.HayvanDogumTarihi,
                HayvanOlumTarihi = hayvan.HayvanOlumTarihi,
                SahiplenmeTarihi = hayvan.Sahipler.Where(s=>s.SahipId==user.Id).Select(s => s.SahiplenmeTarihi).FirstOrDefault(),
                HayvanKilo = hayvan.HayvanKilo,
                TurAdi = hayvan.CinsTur.Tur.TurAdi,
                CinsAdi = hayvan.CinsTur.Cins.CinsAdi,
                RenkAdi = hayvan.Renk.RenkAdi,
                Muayeneler = muayeneler.Select(m => new MuayeneViewModel
                {
                    MuayeneId = m.MuayeneId,
                    MuayeneTarihi = m.MuayeneTarihi,
                    HekimAdi = m.HekimAdi
                }).ToList(),               
                
            };


            
        }


    }
}
