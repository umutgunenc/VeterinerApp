using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class CinsSilViewModel :Cins
    {
        private readonly VeterinerDBContext _context;
        public CinsSilViewModel()
        {
            
        }

        public CinsSilViewModel(VeterinerDBContext context)
        {
            _context = context;
            CinslerListesiGetir();
        }
        public List<SelectListItem> CinslerListesi { get; set; }

        private List<SelectListItem> CinslerListesiGetir() {
            CinslerListesi = new List<SelectListItem>();
            foreach(var cins in _context.Cinsler)
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
