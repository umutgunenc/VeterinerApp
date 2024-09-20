using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class StokDetayViewModel :Stok
    {
        private readonly VeterinerDBContext _context;
        public int StokSayisi { get; set; }

        public StokDetayViewModel(VeterinerDBContext context,  int stokId)
        {
            _context = context;
            ModeliOlusturAsync(stokId).Wait();
        }

        public async Task<StokDetayViewModel> ModeliOlusturAsync(int stokId)
        {
            StokHareketDetay stokHareketDetay = new(_context, stokId);
            var stok = await _context.Stoklar.Where(s => s.Id == stokId).FirstOrDefaultAsync();
            KategoriAdi = await KategoriAdiniGetirAsync(stokId);
            BirimAdi = await BirimAdiniGetirAsync(stokId);
            Id = stok.Id;
            StokAdi = stok.StokAdi;
            StokBarkod = stok.StokBarkod;
            Aciklama = stok.Aciklama;
            AktifMi = stok.AktifMi;
            BirimId = stok.BirimId;
            KategoriId = stok.KategoriId;
            StokSayisi = stokHareketDetay.StokSayisiniHesapla(stok);
            OrtalamaAlisFiyati = stokHareketDetay.OrtalamaAlisFiyatiniHesapla();
            StokHareketDetayListesi = stokHareketDetay.StokHareketDetayListesi;
            Birim = stok.Birim;

            return this;
        }

        public string KategoriAdi { get; set; }
        public string BirimAdi { get; set; }
        public double? OrtalamaAlisFiyati { get; set; }




        public List<StokHareketDetay> StokHareketDetayListesi { get; set; }

        private async Task<string> KategoriAdiniGetirAsync(int stokId)
        {
            KategoriAdi = await _context.Kategoriler
                .Where(k => k.KategoriId == _context.Stoklar
                                                   .Where(s => s.Id == stokId)
                                                   .Select(s => s.KategoriId)
                                                   .FirstOrDefault())
                .Select(k => k.KategoriAdi)
                .FirstOrDefaultAsync();

            return KategoriAdi;
        }
        private async Task<string> BirimAdiniGetirAsync(int stokId)
        {
            BirimAdi = await _context.Birimler
                .Where(b => b.BirimId == _context.Stoklar
                                                .Where(s => s.Id == stokId)
                                                .Select(s => s.BirimId)
                                                .FirstOrDefault())
                .Select(b => b.BirimAdi)
                .FirstOrDefaultAsync();

            return BirimAdi;
        }





    }

    public class StokHareketDetay : StokHareket
    {
        private readonly VeterinerDBContext _context;

        public StokHareketDetay()
        {
            
        }

        public StokHareketDetay(VeterinerDBContext context,int stokId)
        {
            _context = context;
            ModeliOlusturAsync(stokId).Wait();

        }
        private async Task<StokHareketDetay> ModeliOlusturAsync(int stokId)
        {
            StokHarektlerListesi = await StokHareketListesiniGetirAsync(stokId);
            StokHareketDetayListesi = await StokHareketDetayListesiniGetirAsync(stokId);
            return this;
        }

        public string CalisanAdi { get; set; }
        public string CalisanSoyadi { get; set; }
        
        private List<StokHareket> StokHarektlerListesi { get; set; }
        public List<StokHareketDetay> StokHareketDetayListesi { get; set; }

        public int StokSayisiniHesapla(Stok stok)
        {
            int stokGiris = 0;
            int stokCikis = 0;

            foreach (var stokHareket in StokHarektlerListesi)
            {
                stokGiris += stokHareket.StokGirisAdet ?? 0;
                stokCikis += stokHareket.StokCikisAdet ?? 0;
            }
            return stokGiris - stokCikis;
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
                return  Math.Round((double)(toplamAlisFiyati / toplamAlinanAdet), 2);
            return null;
        }
        private async Task<List<StokHareket>> StokHareketListesiniGetirAsync(int stokId)
        {
            var stokHareketler = await _context.StokHareketler.Where(sh => sh.StokId == stokId).ToListAsync();
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
        private async Task<List<StokHareketDetay>> StokHareketDetayListesiniGetirAsync(int stokId)
        {
            StokHareketDetayListesi = new();
            StokHarektlerListesi = await StokHareketListesiniGetirAsync(stokId);
            foreach (var StokHareket in StokHarektlerListesi)
            {
                StokHareketDetayListesi.Add(new StokHareketDetay()
                {

                    StokHareketId = StokHareket.StokHareketId,
                    SatisTarihi = StokHareket.SatisTarihi,
                    AlisTarihi = StokHareket.AlisTarihi,
                    AlisFiyati = StokHareket.AlisFiyati,
                    SatisFiyati = StokHareket.SatisFiyati,
                    StokGirisAdet = StokHareket.StokGirisAdet,
                    StokCikisAdet = StokHareket.StokCikisAdet,
                    CalisanAdi = await _context.AppUsers
                        .Where(ap => ap.Id == StokHareket.CalisanId)
                        .Select(ap => ap.InsanAdi)
                        .FirstAsync(),
                    CalisanSoyadi = await _context.AppUsers
                        .Where(ap => ap.Id == StokHareket.CalisanId)
                        .Select(ap => ap.InsanSoyadi)
                        .FirstAsync(),

                });

            }
            return StokHareketDetayListesi;

        }

    }
}
