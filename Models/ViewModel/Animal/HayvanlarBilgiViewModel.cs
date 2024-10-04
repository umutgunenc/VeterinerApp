using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Fonksiyonlar;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Enum;

namespace VeterinerApp.Models.ViewModel.Animal
{
    public class HayvanlarBilgiViewModel : Hayvan
    {
        public string TurAdi { get; set; }
        public string CinsAdi { get; set; }
        public string HayvanAnneAdi { get; set; }
        public string HayvanBabaAdi { get; set; }
        public string RenkAdi { get; set; }
        public DateTime SahiplenmeTarihi { get; set; }

        public List<MuayeneViewModel> Muayeneler { get; set; }
        private async Task<string> HayvanAnneAdiGetirAsync(Hayvan hayvan, VeterinerDBContext context)
        {
            if (hayvan.HayvanAnneId.HasValue)
            {
                var anne = await context.Hayvanlar.FindAsync(hayvan.HayvanAnneId);
                return anne.HayvanAdi;
            }
            return "Bilinmiyor";
        }
        private async Task<string> HayvanBabaAdiGetirAsync(Hayvan hayvan, VeterinerDBContext context)
        {
            if (hayvan.HayvanBabaId.HasValue)
            {
                var baba = await context.Hayvanlar.FindAsync(hayvan.HayvanBabaId);
                return baba.HayvanAdi;
            }
            return "Bilinmiyor";
        }
        private async Task<string> RenkAdiniGetirAsync(Hayvan hayvan, VeterinerDBContext context)
        {
            return await context.Renkler.Where(r => r.RenkId == hayvan.RenkId).Select(r => r.RenkAdi).FirstOrDefaultAsync();
        }
        private async Task<string> CinsAdiniGetirAsync(Hayvan hayvan, VeterinerDBContext context)
        {
            return await context.Cinsler
                .Where(c => c.CinsId == context.CinsTur
                    .Where(ct => ct.Id == hayvan.CinsTurId)
                    .Select(ct => ct.CinsId).FirstOrDefault())
                .Select(c => c.CinsAdi).FirstOrDefaultAsync();
        }
        private async Task<string> TurAdiniGetirAsync(Hayvan hayvan, VeterinerDBContext context)
        {
            return await context.Turler
                 .Where(t => t.TurId == context.CinsTur
                     .Where(ct => ct.Id == hayvan.CinsTurId)
                     .Select(ct => ct.TurId).FirstOrDefault())
                 .Select(t => t.TurAdi).FirstOrDefaultAsync();
        }

        public async Task<HayvanlarBilgiViewModel> HayvanBilgileriniGetirAsync(Hayvan hayvan, AppUser user, VeterinerDBContext context)
        {
            var muayeneler = context.Muayeneler
            .Where(muayene => muayene.HayvanId == hayvan.HayvanId)
            .Select(muayene => new
            {
                muayene.MuayeneId,
                muayene.MuayeneTarihi,
                muayene.HekimId,
                HekimAdi = context.AppUsers
                    .Where(hekim => hekim.Id == muayene.HekimId)
                    .Select(hekim => hekim.InsanAdi + " " + hekim.InsanSoyadi)
                    .FirstOrDefault()
            }).ToList();


            return new HayvanlarBilgiViewModel()
            {
                HayvanId = hayvan.HayvanId,
                HayvanAdi = hayvan.HayvanAdi,
                HayvanAnneAdi = await HayvanAnneAdiGetirAsync(hayvan, context),
                HayvanBabaAdi = await HayvanBabaAdiGetirAsync(hayvan, context),
                HayvanCinsiyet = hayvan.HayvanCinsiyet,
                ImgUrl = hayvan.ImgUrl,
                HayvanDogumTarihi = hayvan.HayvanDogumTarihi,
                HayvanOlumTarihi = hayvan.HayvanOlumTarihi,
                SahiplenmeTarihi = hayvan.Sahipler.Where(s => s.SahipId == user.Id).Select(s => s.SahiplenmeTarihi).FirstOrDefault(),
                HayvanKilo = hayvan.HayvanKilo,
                TurAdi = await TurAdiniGetirAsync(hayvan, context),
                CinsAdi = await CinsAdiniGetirAsync(hayvan, context),
                RenkAdi = await RenkAdiniGetirAsync(hayvan, context),
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
