using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class RenkSilViewModel :Renk
    {
        private readonly VeterinerDBContext _context;
        public RenkSilViewModel()
        {

        }

        public RenkSilViewModel(VeterinerDBContext context) 
        {
            _context = context;
            RenkleriGetir().Wait();
        }
        public List<SelectListItem> RenklerListesi { get; set; }

        private async Task<List<SelectListItem>> RenkleriGetir()
        {
            var Renkler = await _context.Renkler.ToListAsync();

            RenklerListesi = new List<SelectListItem>();

            foreach (var renk in Renkler)
            {
                RenklerListesi.Add(new SelectListItem
                {
                    Text = renk.RenkAdi,
                    Value = renk.RenkId.ToString()
                });
            }
            return RenklerListesi;
        }


    }
}
