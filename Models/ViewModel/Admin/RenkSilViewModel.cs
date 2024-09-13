using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class RenkSilViewModel :Renk
    {
        public RenkSilViewModel()
        {

        }

        public List<SelectListItem> RenklerListesi { get; set; }

        public async Task<RenkSilViewModel> RenklerListesiniGetirAsync(VeterinerDBContext _context)
        {
            var Renkler = await _context.Renkler.ToListAsync();
            RenklerListesi = new();

            foreach (var renk in Renkler)
            {
                RenklerListesi.Add(new SelectListItem
                {
                    Text = renk.RenkAdi,
                    Value = renk.RenkId.ToString()
                });
            }
            return this;
        }

        public async Task<Renk> SilinecekRengiGetirAsync(RenkSilViewModel model,VeterinerDBContext _context)
        {
            return await _context.Renkler.FindAsync(model.RenkId);

        }


    }
}
