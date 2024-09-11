using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class CinsTurEkleViewModel :CinsTur
    {
        private readonly VeterinerDBContext _context;
        public CinsTurEkleViewModel()
        {

        }
        public CinsTurEkleViewModel(VeterinerDBContext context)
        {
            _context = context;
            TurlerListesiGetir();
            CinslerListesiGetir();
        }
        public List<SelectListItem> TurlerListesi { get; set; } 
        public List<SelectListItem> CinslerListesi { get; set; } 

        private List<SelectListItem> TurlerListesiGetir()
        {
            TurlerListesi = new List<SelectListItem>();
            foreach(var tur in _context.Turler)
            {
                TurlerListesi.Add(new SelectListItem
                {
                    Text = tur.TurAdi,
                    Value = tur.TurId.ToString()
                });
            }
            return TurlerListesi;
        }
        private List<SelectListItem> CinslerListesiGetir()
        {
            CinslerListesi = new List<SelectListItem>();
            foreach (var cins in _context.Cinsler)
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
