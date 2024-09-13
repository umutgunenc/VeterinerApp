using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class RolSilViewModel :AppRole
    {
 
        public List<SelectListItem> RollerListesi { get; set; }

        public async Task<RolSilViewModel> RollerListesiniGetir(VeterinerDBContext _context)
        {
            var roller = await _context.Roles.ToListAsync();
            RollerListesi = new List<SelectListItem>();
            foreach (var rol in roller)
            {
                RollerListesi.Add(new SelectListItem
                {
                    Text = rol.Name,
                    Value = rol.Id.ToString()
                });
            }

            return this;
        }

        public async Task<AppRole> SilinecekRoluGetir(VeterinerDBContext _context, RolSilViewModel model)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.Id == model.Id);
        }
    }
}
