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

        public List<StokGoruntuleViewModel> StokListesi { get; set; }


        public async Task<List<StokGoruntuleViewModel>> StokListesiniGetirAsync(VeterinerDBContext context)
        {
            StokListesi = new();
            var stoklar = await context.Stoklar.ToListAsync();


            foreach (var stok in stoklar)
            {
                StokListesi.Add(new StokGoruntuleViewModel
                {
                    Id = stok.Id,
                    StokAdi = stok.StokAdi,
                    AktifMi = stok.AktifMi,
                    Aciklama = stok.Aciklama,
                    StokBarkod = stok.StokBarkod,
                    BirimId = stok.BirimId,
                    KategoriId = stok.KategoriId,
                    KategoriAdi = await context.Kategoriler
                                                .Where(k => k.KategoriId == stok.KategoriId)
                                                .Select(k => k.KategoriAdi)
                                                .FirstOrDefaultAsync(),

                    BirimAdi = await context.Birimler
                                                .Where(b => b.BirimId == stok.BirimId)
                                                .Select(b => b.BirimAdi)
                                                .FirstOrDefaultAsync()

                });
            }

            return StokListesi;
        }
    }
}
