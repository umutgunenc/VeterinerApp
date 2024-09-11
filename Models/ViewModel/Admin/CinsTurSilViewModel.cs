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
    public class CinsTurSilViewModel : CinsTur
    {
        private readonly VeterinerDBContext _context;
        public CinsTurSilViewModel()
        {

        }
        public CinsTurSilViewModel(VeterinerDBContext context)
        {
            _context = context;
            CinslerTurlerListesiniGetir().Wait();
        }

        public List<SelectListItem> CinslerTurlerListesi { get; set; }

        private async Task<List<SelectListItem>> CinslerTurlerListesiniGetir()
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

            return CinslerTurlerListesi;
        }

    }
}
