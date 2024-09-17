using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class RolSilViewModel : AppRole
    {

        public List<SelectListItem> RollerListesi { get; set; }
        public AppRole SilinecekRol { get; set; }

        public async Task<List<SelectListItem>> RollerListesiniGetir(VeterinerDBContext context)
        {
            var roller = await context.Roles.ToListAsync();
            RollerListesi = new ();
            foreach (var rol in roller)
            {
                RollerListesi.Add(new SelectListItem
                {
                    Text = rol.Name,
                    Value = rol.Id.ToString()
                });
            }

            return RollerListesi;
        }

        public async Task<AppRole> SilinecekRoluGetir(VeterinerDBContext context, RolSilViewModel model)
        {
            return await context.Roles.FirstOrDefaultAsync(x => x.Id == model.Id);
        }
    }
}
