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


        public List<SelectListItem> Turler { get; set; }

        public async Task<List<SelectListItem>> TurListesiniGetirASync(VeterinerDBContext _context)
        {
            var turler =await _context.Turler.ToListAsync();

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

        public async Task<Tur> SilinecekTuruGetirAsync(TurSilViewModel model, VeterinerDBContext _context)
        {
            return await _context.Turler.FindAsync(model.TurId);
        }


    }
}
