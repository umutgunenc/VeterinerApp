using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class KategoriSilViewModel : Kategori
    {
        public List<SelectListItem> KategoriListesi { get; set; }
        public Kategori SilinecekKategori {get;set;}
        public async Task<List<SelectListItem>> KategoriListesiniGetirAsync(VeterinerDBContext context)
        {
            var Kategoriler = await context.Kategoriler.ToListAsync();
            KategoriListesi = new();
            foreach (var kategori in Kategoriler)
            {
                KategoriListesi.Add(new SelectListItem
                {
                    Text = kategori.KategoriAdi,
                    Value = kategori.KategoriId.ToString()
                });
            }

            return KategoriListesi;

        }

        public async Task<Kategori> SilinecekKategoriyiGetirAsync(VeterinerDBContext context, KategoriSilViewModel model)
        {
            SilinecekKategori = new();
            SilinecekKategori = await context.Kategoriler
                                                .Where(k => k.KategoriId == model.KategoriId)
                                                .Select(k => k)
                                                .FirstOrDefaultAsync();
            return SilinecekKategori;
        }
    }
}
