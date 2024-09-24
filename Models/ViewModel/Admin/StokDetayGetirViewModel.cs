using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class StokDetayGetirViewModel : Stok
    {
        private readonly VeterinerDBContext _context;
        public StokDetayGetirViewModel(VeterinerDBContext context)
        {
            _context = context;
        }

        public string Signature { get; set; }
        private StokDetayViewModel Model { get; set; }
        public List<SelectListItem> KategoriListesi { get; set; }
        public List<SelectListItem> BirimListesi { get; set; }
        private async Task<List<SelectListItem>> KategoriListesiniGetirAsync()
        {
            KategoriListesi = new();
            var kategoriler = await _context.Kategoriler.ToListAsync();

            foreach (var kategori in kategoriler)
            {
                KategoriListesi.Add(new SelectListItem
                {
                    Text = kategori.KategoriAdi,
                    Value = kategori.KategoriId.ToString()

                });
            }

            return KategoriListesi;
        }
        private async Task<List<SelectListItem>> BirimListesiniGetirAsync()
        {
            BirimListesi = new();
            var birimler = await _context.Birimler.ToListAsync();

            foreach (var birim in birimler)
            {
                BirimListesi.Add(new SelectListItem
                {
                    Text = birim.BirimAdi,
                    Value = birim.BirimId.ToString()

                });
            }

            return BirimListesi;
        }
        public async Task<StokDetayGetirViewModel> ModeliOlusturAsync(StokDuzenleViewModel model)
        {
            var stok = await _context.Stoklar
                .Where(x => x.StokBarkod.ToUpper() == model.StokBarkod.ToUpper())
                .FirstOrDefaultAsync();

            Id = stok.Id;
            StokAdi = stok.StokAdi;
            StokBarkod = stok.StokBarkod;
            Aciklama = stok.Aciklama;
            AktifMi = stok.AktifMi;
            BirimId = stok.BirimId;
            KategoriId = stok.KategoriId;
            KategoriListesi = await KategoriListesiniGetirAsync();
            BirimListesi = await BirimListesiniGetirAsync();

            return this;

        }


    }
}
