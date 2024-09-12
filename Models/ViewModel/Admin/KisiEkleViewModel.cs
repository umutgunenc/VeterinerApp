using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class KisiEkleViewModel : AppUser
    {
        private readonly VeterinerDBContext _context;
        public KisiEkleViewModel()
        {
            
        }
        public KisiEkleViewModel(VeterinerDBContext context)
        {
            _context = context;
            RollerListesiniGetir().Wait();
        }

        public List<SelectListItem> RollerListesi { get; set; }
        public int RolId { get; set; }
        public AppRole Rol { get; set; }
        public string Error { get; set; }

        private async Task<List<SelectListItem>> RollerListesiniGetir()
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

            return RollerListesi;
        }

    }
}
