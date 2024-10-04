using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Enum;

namespace VeterinerApp.Models.ViewModel.Animal
{
    public class AddAnimalViewModel : Hayvan
    {

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

        public async Task<List<SelectListItem>> AnnelerListesiOlusturAsync(VeterinerDBContext context)
        {
            HayvanAnneListesi = new();
            bool disiHayvanVarMi = context.Hayvanlar.Any(h => h.HayvanCinsiyet == Cinsiyet.Dişi);
            if (disiHayvanVarMi)
            {
                var disiHayvanlar = await context.Hayvanlar.Where(h => h.HayvanCinsiyet == Cinsiyet.Dişi)
                     .Select(h => new
                     {
                         sahipTckn = context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanTckn).FirstOrDefault(),
                         sahipAdSoyad = context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanAdi + " " + sh.AppUser.InsanSoyadi).FirstOrDefault(),
                         h.HayvanAdi,
                         h.HayvanId,
                         h.HayvanCinsiyet,
                         CinsAdi = context.Cinsler.Where(c => c.CinsId == h.CinsTur.CinsId).Select(c => c.CinsAdi).FirstOrDefault(),
                         TurAdi = context.Turler.Where(t => t.TurId == h.CinsTur.TurId).Select(t => t.TurAdi).FirstOrDefault(),
                         RenkAdi = context.Renkler.Where(r => r.RenkId == h.RenkId).Select(r => r.RenkAdi).FirstOrDefault(),
                         hayvanDogumTarihi = h.HayvanDogumTarihi.ToString("dd-MM-yyyy")
                     })
                     .ToListAsync();

                return disiHayvanlar.Select(h => new SelectListItem
                {
                    Text = $"{h.sahipTckn.Substring(0, 3) + new string('*', Math.Max(h.sahipTckn.Length - 6, 0)) + h.sahipTckn.Substring(h.sahipTckn.Length - 3)} " +
                                    $"{h.sahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.sahipAdSoyad.Length - 4, 0)) + h.sahipAdSoyad.Substring(h.sahipAdSoyad.Length - 2)} - " +
                                    $"{h.HayvanAdi} {h.CinsAdi} {h.TurAdi} {h.RenkAdi} {h.hayvanDogumTarihi}",
                    Value = h.HayvanId.ToString()
                }).ToList();
            }

            return HayvanAnneListesi;



        }
        public async Task<List<SelectListItem>> BabalarListesiOlusturAsync(VeterinerDBContext context)
        {
            HayvanBabaListesi = new();
            bool erkekHayvanVarMi = context.Hayvanlar.Any(h => h.HayvanCinsiyet == Cinsiyet.Erkek);
            if (erkekHayvanVarMi)
            {
                var erkekHayvanlar = await context.Hayvanlar.Where(h => h.HayvanCinsiyet == Cinsiyet.Dişi)
                     .Select(h => new
                     {
                         sahipTckn = context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanTckn).FirstOrDefault(),
                         sahipAdSoyad = context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanAdi + " " + sh.AppUser.InsanSoyadi).FirstOrDefault(),
                         h.HayvanAdi,
                         h.HayvanId,
                         h.HayvanCinsiyet,
                         CinsAdi = context.Cinsler.Where(c => c.CinsId == h.CinsTur.CinsId).Select(c => c.CinsAdi).FirstOrDefault(),
                         TurAdi = context.Turler.Where(t => t.TurId == h.CinsTur.TurId).Select(t => t.TurAdi).FirstOrDefault(),
                         RenkAdi = context.Renkler.Where(r => r.RenkId == h.RenkId).Select(r => r.RenkAdi).FirstOrDefault(),
                         hayvanDogumTarihi = h.HayvanDogumTarihi.ToString("dd-MM-yyyy")
                     })
                     .ToListAsync();

                return erkekHayvanlar.Select(h => new SelectListItem
                {
                    Text = $"{h.sahipTckn.Substring(0, 3) + new string('*', Math.Max(h.sahipTckn.Length - 6, 0)) + h.sahipTckn.Substring(h.sahipTckn.Length - 3)} " +
                                    $"{h.sahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.sahipAdSoyad.Length - 4, 0)) + h.sahipAdSoyad.Substring(h.sahipAdSoyad.Length - 2)} - " +
                                    $"{h.HayvanAdi} {h.CinsAdi} {h.TurAdi} {h.RenkAdi} {h.hayvanDogumTarihi}",
                    Value = h.HayvanId.ToString()
                }).ToList();
            }

            return HayvanBabaListesi;
        }
        public List<SelectListItem> CinsiyetListesiOlustur()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Erkek", Value = Cinsiyet.Erkek.ToString() },
                new SelectListItem { Text = "Dişi", Value = Cinsiyet.Dişi.ToString() }
            };
        }
        public async Task<List<SelectListItem>> TurListesiOlusturAsync(VeterinerDBContext context)
        {
            return await context.Turler.Select(t => new SelectListItem
            {
                Text = t.TurAdi,
                Value = t.TurId.ToString()
            }).ToListAsync();
        }
        public async Task<List<SelectListItem>> CinsListesiOlusturAsync(VeterinerDBContext context)
        {
            return await context.Cinsler.Select(c => new SelectListItem
            {
                Text = c.CinsAdi,
                Value = c.CinsId.ToString()
            }).ToListAsync();
        }
        public async Task<List<SelectListItem>> RenkListesiOlusturAsync(VeterinerDBContext context)
        {
            return await context.Renkler.Select(Renk => new SelectListItem
            {
                Value = Renk.RenkId.ToString(),
                Text = Renk.RenkAdi
            }).ToListAsync();
        }
    }
}
