using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class CinsTurEslesmeKaldirViewModel : CinsTur
    {

        public List<SelectListItem> CinslerTurlerListesi { get; set; }

        public async Task<CinsTurEslesmeKaldirViewModel> CinslerTurlerListesiniGetirAsync(VeterinerDBContext _context)
        {
            var cinsTurler = await _context.CinsTur.ToListAsync();
            var turler = await _context.Turler.ToListAsync();
            var cinsler = await _context.Cinsler.ToListAsync();

            CinslerTurlerListesi = new List<SelectListItem>();

            foreach (var cinsTur in cinsTurler)
            {
                var tur = turler.FirstOrDefault(t => t.TurId == cinsTur.TurId);
                var cins = cinsler.FirstOrDefault(c => c.CinsId == cinsTur.CinsId);

                if (tur != null && cins != null)
                {
                    CinslerTurlerListesi.Add(new SelectListItem
                    {
                        Text = $"{tur.TurAdi} - {cins.CinsAdi}",
                        Value = cinsTur.Id.ToString()
                    });
                }
            }

            return this;
        }

        public async Task<CinsTur> EslesmesiKaldirilacakCinsTuruGetirAsync(CinsTurEslesmeKaldirViewModel model, VeterinerDBContext _context)
        {
            return await _context.CinsTur.FirstOrDefaultAsync(c => c.Id == model.Id);
        }



    }
}
