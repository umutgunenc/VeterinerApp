using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class StokKartiOlusturViewModel : Stok
    {

        public string KategoriAdı { get; set; }
        public string BirimnAdı { get; set; }

        public List<SelectListItem> KategoriListesi { get; set; }
        public List<SelectListItem> BirimlerListesi { get; set; }

        public async Task<List<SelectListItem>> KategoriListesiniGetirAsync(VeterinerDBContext context)
        {
            KategoriListesi = new ();
            var kategoriler = await context.Kategoriler.ToListAsync();
            foreach(var kategori in kategoriler)
            {
                KategoriListesi.Add(new SelectListItem
                {
                    Text = kategori.KategoriAdi,
                    Value = kategori.KategoriId.ToString()
                });
            }
            return KategoriListesi;
        }

        public async Task<List<SelectListItem>> BirimListesiniGetirAsync(VeterinerDBContext context)
        {
            BirimlerListesi = new();
            var birimler = await context.Birimler.ToListAsync();
            foreach(var birim in birimler)
            {
                BirimlerListesi.Add(new SelectListItem
                {
                    Text = birim.BirimAdi,
                    Value = birim.BirimId.ToString()
                });

            }

            return BirimlerListesi;
        }
    }
}
