using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Fonksiyonlar;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class KisiEkleViewModel : AppUser
    {

        public List<SelectListItem> RollerListesi { get; set; }
        public int RolId { get; set; }
        public string Error { get; set; }
        public string KullaniciSifresi { get; set; }

        public async Task<KisiEkleViewModel> RollerListesiniGetirAsync(VeterinerDBContext _context)
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

        public async Task<KisiEkleViewModel> KisiOlusturAsync(VeterinerDBContext _context, KisiEkleViewModel model)
        {
            UserName = await KullaniciAdiOlustur.GenerateUserNameAsync(model.InsanAdi, model.InsanSoyadi, model.Email, _context);
            CalisiyorMu = true;
            SifreOlusturmaTarihi = DateTime.Now;
            SifreGecerlilikTarihi = DateTime.Now.AddDays(120);
            TermOfUse = true;

            KullaniciSifresi = SifreOlustur.GeneratePassword(8);

            return this;

        }

        public async Task<IdentityUserRole<int>> KisininRolunuGetirAsync(VeterinerDBContext _context, KisiEkleViewModel model)
        {
            var kullanici =await _context.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName);
            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == model.RolId);

            return new IdentityUserRole<int>
            {
                UserId = kullanici.Id,
                RoleId = userRole.Id
            };
        }

    }
}
