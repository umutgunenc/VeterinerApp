using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class BirimSilViewModel : Birim
    {
        public Birim SilinecekBirim { get; set; }

        public List<SelectListItem> BirimLerListesi { get; set; }

        public async Task<List<SelectListItem>> BirimlerListesiniGetirAsync(VeterinerDBContext context)
        {
            BirimLerListesi = new();
            var birimler = await context.Birimler.ToListAsync();

            foreach (var birim in birimler)
            {
                BirimLerListesi.Add(new SelectListItem
                {
                    Text = birim.BirimAdi,
                    Value = birim.BirimId.ToString()

                });
                
            }
            return BirimLerListesi;
        }

        public async Task<Birim> SilinecekBirimGetirAsync(VeterinerDBContext context, BirimSilViewModel model)
        {
            SilinecekBirim = await context.Birimler.FindAsync(model.BirimId);
            return SilinecekBirim;
        }
    }
}
