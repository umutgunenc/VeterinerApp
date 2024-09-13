using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class CinsSilViewModel : Cins
    {

        public List<SelectListItem> CinslerListesi { get; set; }

        public async Task<CinsSilViewModel> CinslerListesiGetirAsync(VeterinerDBContext _context)
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
            return this;
        }

        public async Task<Cins> SilinecekCinsiGetir(CinsSilViewModel model, VeterinerDBContext _context)
        {
            return await _context.Cinsler.FindAsync(model.CinsId);
        }
    }
}
