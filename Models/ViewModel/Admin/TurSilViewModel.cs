using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            TurListesiniGetir().Wait();
        }
        public List<SelectListItem> Turler { get; set; }

        private async Task<List<SelectListItem>> TurListesiniGetir()
        {
            var turler =await _context.Turler.ToListAsync();

            Turler = new List<SelectListItem>();

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
