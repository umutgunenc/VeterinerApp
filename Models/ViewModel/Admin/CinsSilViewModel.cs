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
        private readonly VeterinerDBContext _context;
        public CinsSilViewModel()
        {

        }

        public CinsSilViewModel(VeterinerDBContext context)
        {
            _context = context;
            CinslerListesiGetir().Wait();
        }
        public List<SelectListItem> CinslerListesi { get; set; }

        private async Task<List<SelectListItem>> CinslerListesiGetir()
        {
            var cinsler = await _context.Cinsler.ToListAsync();

            CinslerListesi = new List<SelectListItem>();

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
    }
}
