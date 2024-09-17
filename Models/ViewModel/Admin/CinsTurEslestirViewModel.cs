using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class CinsTurEslestirViewModel :CinsTur
    {
 
        public List<SelectListItem> TurlerListesi { get; set; } 
        public List<SelectListItem> CinslerListesi { get; set; }

        public async Task<List<SelectListItem>> TurlerListesiGetirAsync(VeterinerDBContext context)
        {
            var turler=await context.Turler.ToListAsync();
            TurlerListesi = new();
            foreach (var tur in turler)
            {
                TurlerListesi.Add(new SelectListItem
                {
                    Text = tur.TurAdi,
                    Value = tur.TurId.ToString()
                });
            }
            return TurlerListesi;
        }
        public async Task<List<SelectListItem>> CinslerListesiGetirAsync(VeterinerDBContext context)
        {
            var cinsler = await context.Cinsler.ToListAsync();
            CinslerListesi = new();

            foreach (var cins in cinsler)
            {
                CinslerListesi.Add(new SelectListItem
                {
                    Text = cins.CinsAdi,
                    Value = cins.CinsId.ToString()
                });
            }
            return CinslerListesi;
        }
        public async Task<Cins> SecilenCinsiGetirAsync(VeterinerDBContext context, CinsTurEslestirViewModel model)
        {
           return await context.Cinsler.FirstOrDefaultAsync(x => x.CinsId == model.CinsId);
        }

        public async Task<Tur> SecilenTuruGetirAsync(VeterinerDBContext context, CinsTurEslestirViewModel model)
        {
            return await context.Turler.FirstOrDefaultAsync(x => x.TurId == model.TurId);
        }

    }
}
