using FluentValidation;
using Microsoft.AspNetCore.Http;
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

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class StokGirisKaydetViewModel : StokHareket
    {

        public List<SelectListItem> AramaSonucListesi { get; set; }
        public List<string> ImzaListesi { get; set; }

        public async Task<(bool, StokGirisKaydetViewModel)> AramaSonucunuGetirAsync(StokGirisViewModel model, VeterinerDBContext context)
        {

            if (string.IsNullOrEmpty(model.ArananMetin))
                return (false, null);

            var aramaSonucu = await ArananStoguGetirAsync(model, context);

            if (aramaSonucu.Count == 0)
                return (false, null);

            AramaSonucListesi = aramaSonucu;
            ImzaListesi = await ImzaListesiOlusturAsync(context);
            return (true, this);

        }


        private async Task<List<SelectListItem>> ArananStoguGetirAsync(StokGirisViewModel model, VeterinerDBContext context)
        {
            string arananMetin = model.ArananMetin.ToUpper();

            var stoklar = await context.Stoklar
                    .Where(s => (s.StokBarkod.ToUpper().Contains(arananMetin) ||
                                 s.StokAdi.ToUpper().Contains(arananMetin)) &&
                                 s.AktifMi == true)
                    .ToListAsync();

            AramaSonucListesi = new();

            foreach (var stok in stoklar)
            {
                AramaSonucListesi.Add(new SelectListItem
                {
                    Text = $"{stok.StokBarkod} - {stok.StokAdi}",
                    Value = stok.Id.ToString()
                });
            }

            return AramaSonucListesi;
        }


        private async Task<List<string>> ImzaListesiOlusturAsync(VeterinerDBContext context)
        {
            ImzaListesi = new();
            foreach (var item in AramaSonucListesi)
            {
                var stok = await context.Stoklar
                    .Where(s => s.Id == Int32.Parse(item.Value))
                    .FirstOrDefaultAsync();

                ImzaListesi.Add(Signature.CreateSignature(stok.Id.ToString(), stok.StokBarkod));
            }

            return ImzaListesi;
        }

        public async Task<Stok> StoguGetirAsync(StokGirisKaydetViewModel model, VeterinerDBContext context)
        {
            var stok = await context.Stoklar
                .Where(s => s.Id == model.StokId)
                .FirstOrDefaultAsync();
            return stok;
        }

        public StokHareket StokHareketBigileriniGetir(StokGirisKaydetViewModel model, AppUser user)
        {
            StokHareketTarihi = System.DateTime.Now;
            StokId = model.StokId;
            AlisTarihi = model.AlisTarihi;
            StokGirisAdet = model.StokGirisAdet;
            CalisanId = user.Id;

            return this;


        }
    }
}
