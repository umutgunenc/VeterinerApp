using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
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
            RenkleriGetir();
        }
        public List<SelectListItem> RenklerListesi { get; set; }

        private List<SelectListItem> RenkleriGetir()
        {
            RenklerListesi = new List<SelectListItem>();
            foreach (var renk in _context.Renkler)
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
