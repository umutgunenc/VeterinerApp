using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class StokGoruntuleViewModel : Stok
    {
        public string KategoriAdi { get; set; }
        public string BirimAdi { get; set; }

        public int StokSayisi { get; set; }
        

        public List<StokGoruntuleViewModel> StokListesi { get; set; }


        public async Task<List<StokGoruntuleViewModel>> StokListesiniGetirAsync(VeterinerDBContext context)
        {
            StokListesi = new();
            var stoklar = await context.Stoklar.ToListAsync();


            foreach (var stok in stoklar)
            {
                
                var StokHareketler = await context.StokHareketler.Where(sh => sh.StokId == stok.Id).ToListAsync();
                if (StokHareketler.Any())
                {
                    int stokGiris = 0;
                    int stokCikis = 0;
                    foreach (var stokHareket in StokHareketler)
                    {
                        stokGiris += stokHareket.StokGirisAdet ?? 0;
                        stokCikis += stokHareket.StokCikisAdet ?? 0;
                    }
                    StokSayisi = stokGiris - stokCikis ;
                }
                else
                {
                    StokSayisi = 0;
                }

                StokListesi.Add(new StokGoruntuleViewModel
                {
                    Id = stok.Id,
                    StokAdi = stok.StokAdi,
                    AktifMi = stok.AktifMi,
                    Aciklama = stok.Aciklama,
                    StokBarkod = stok.StokBarkod,
                    KategoriAdi = await context.Kategoriler
                                                .Where(k => k.KategoriId == stok.KategoriId)
                                                .Select(k => k.KategoriAdi)
                                                .FirstOrDefaultAsync(),

                    BirimAdi = await context.Birimler
                                                .Where(b => b.BirimId == stok.BirimId)
                                                .Select(b => b.BirimAdi)
                                                .FirstOrDefaultAsync(),
                    StokSayisi = StokSayisi
                });
            }

            return StokListesi;
        }
    }
}
