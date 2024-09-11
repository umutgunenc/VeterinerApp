using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class TurSilViewModel :Tur
    {
        private readonly VeterinerDBContext _context;
        public TurSilViewModel()
        {

        }

        public TurSilViewModel(VeterinerDBContext context)
        {
            _context = context;
            TurListesiniGetir();
        }
        public List<SelectListItem> Turler { get; set; }

        private List<SelectListItem> TurListesiniGetir()
        {
            Turler = new List<SelectListItem>();
            var turler = _context.Turler;
            foreach (var tur in turler)
            {
                Turler.Add(new SelectListItem
                {
                    Text = tur.TurAdi,
                    Value = tur.TurId.ToString()
                });
            }
            return Turler;
        }


    }
}
