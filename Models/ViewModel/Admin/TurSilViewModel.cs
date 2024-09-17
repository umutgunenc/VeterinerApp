using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class TurSilViewModel :Tur
    {


        public List<SelectListItem> TurListesi { get; set; }

        public async Task<List<SelectListItem>> TurListesiniGetirASync(VeterinerDBContext context)
        {
            var turler =await context.Turler.ToListAsync();
            TurListesi = new();
            foreach (var tur in turler)
            {
                TurListesi.Add(new SelectListItem
                {
                    Text = tur.TurAdi,
                    Value = tur.TurId.ToString()
                });
            }
            return TurListesi;
        }

        public async Task<Tur> SilinecekTuruGetirAsync(TurSilViewModel model, VeterinerDBContext context)
        {
            return await context.Turler.FindAsync(model.TurId);
        }


    }
}
