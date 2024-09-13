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

        private async Task<List<SelectListItem>> TurlerListesiGetirAsync(VeterinerDBContext _context)
        {
            var turler=await _context.Turler.ToListAsync();
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
        private async Task<List<SelectListItem>> CinslerListesiGetirAsync(VeterinerDBContext _context)
        {
            var cinsler = await _context.Cinsler.ToListAsync();
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

        public async Task<CinsTurEslestirViewModel> CinsTurListesiGetirAsync(VeterinerDBContext _context)
        {
            TurlerListesi = await TurlerListesiGetirAsync( _context);
            CinslerListesi = await CinslerListesiGetirAsync( _context);
            return this;
        }
    }
}
