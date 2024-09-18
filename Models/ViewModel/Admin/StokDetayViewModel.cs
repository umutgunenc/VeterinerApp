using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class StokDetayViewModel 
    {
        public string KategoriAdi { get; set; }
        public string BirimAdi { get; set; }
         
        private int Id { get; set; }

        private string StokAdi { get; set; }

        private int StokHareketId { get; set; }
        private DateTime? SatisTarihi { get; set; }
        private double? SatisFiyati { get; set; }
        private DateTime? AlisTarihi { get; set; }
        private double? AlisFiyati { get; set; }
        public double? OrtalamaAlisFiyati { get; set; }
        private string CalisanSoyadi { get; set; }
        private string CalisanAdi { get; set; }
        private int? StokGirisAdet { get; set; }
        private int? StokCikisAdet { get; set; }
        private List<StokHareket> StokHarektlerListesi { get; set; }
        public List<StokDetayViewModel> StokDetayListesi { get; set; }


        private async Task<string> KategoriAdiniGetirAsync(int StokId, VeterinerDBContext context)
        {
            KategoriAdi = await context.Kategoriler
                .Where(k => k.KategoriId == context.Stoklar
                                                   .Where(s => s.Id == StokId)
                                                   .Select(s => s.KategoriId)
                                                   .FirstOrDefault())
                .Select(k => k.KategoriAdi)
                .FirstOrDefaultAsync();

            return KategoriAdi;
        }
        private async Task<string> BirimAdiniGetirAsync(int StokId, VeterinerDBContext context)
        {
            BirimAdi = await context.Birimler
                .Where(b => b.BirimId == context.Stoklar
                                                .Where(s => s.Id == StokId)
                                                .Select(s => s.BirimId)
                                                .FirstOrDefault())
                .Select(b => b.BirimAdi)
                .FirstOrDefaultAsync();

            return BirimAdi;
        }
        private async Task<List<StokHareket>> StokHareketListesiniGetirAsync(int stokId, VeterinerDBContext context)
        {
            var stokHareketler = await context.StokHareketler.Where(sh=>sh.StokId== stokId).ToListAsync();
            StokHarektlerListesi = new();
            foreach (var stokHareket in stokHareketler)
            {
                StokHarektlerListesi.Add(new StokHareket
                {
                    StokHareketId = stokHareket.StokHareketId,
                    StokHareketTarihi = stokHareket.StokHareketTarihi,
                    SatisTarihi = stokHareket.SatisTarihi,
                    SatisFiyati = stokHareket.SatisFiyati,
                    AlisTarihi = stokHareket.AlisTarihi,
                    AlisFiyati = stokHareket.AlisFiyati,
                    CalisanId = stokHareket.CalisanId,
                    StokGirisAdet = stokHareket.StokGirisAdet,
                    StokCikisAdet = stokHareket.StokCikisAdet
                });
            }
            return StokHarektlerListesi;

        }
        public async Task<List<StokDetayViewModel>> StokDetaylariniGetirAsync(int stokId, VeterinerDBContext context)
        {
            StokDetayListesi = new();
            StokHarektlerListesi = await StokHareketListesiniGetirAsync(stokId, context);
            foreach (var StokHareket in StokHarektlerListesi)
            {
                StokDetayListesi.Add(new StokDetayViewModel
                {
                    Id = stokId,
                    StokHareketId = StokHareket.StokHareketId,
                    StokAdi = await context.Stoklar.Where(s => s.Id == stokId).Select(s => s.StokAdi).FirstOrDefaultAsync(),
                    KategoriAdi = await KategoriAdiniGetirAsync(stokId, context),
                    BirimAdi = await BirimAdiniGetirAsync(stokId, context),
                    SatisTarihi = StokHareket.SatisTarihi,
                    AlisTarihi = StokHareket.AlisTarihi,
                    AlisFiyati = StokHareket.AlisFiyati,
                    SatisFiyati = StokHareket.SatisFiyati,
                    CalisanAdi = await context.AppUsers
                        .Where(ap => ap.Id == StokHareket.CalisanId)
                        .Select(ap => ap.InsanAdi)
                        .FirstAsync(),
                    CalisanSoyadi = await context.AppUsers
                        .Where(ap => ap.Id == StokHareket.CalisanId)
                        .Select(ap => ap.InsanSoyadi)
                        .FirstAsync(),
                    StokGirisAdet = StokHareket.StokGirisAdet,
                    StokCikisAdet = StokHareket.StokCikisAdet,

                });

            }
            return StokDetayListesi;

        }

        public double? OrtalamaAlisFiyatiniHesapla()
        {
            double? toplamAlisFiyati = 0.0;
            int? toplamAlinanAdet = 0;
            foreach (var StokHareket in StokHarektlerListesi)
            {
                toplamAlinanAdet += StokHareket.StokGirisAdet;
                toplamAlisFiyati += StokHareket.StokGirisAdet * StokHareket.AlisFiyati;

            }
            if (toplamAlinanAdet > 0) 
                return OrtalamaAlisFiyati = Math.Round((double)(toplamAlisFiyati / toplamAlinanAdet), 2);
            return null;
        }


    }
}
