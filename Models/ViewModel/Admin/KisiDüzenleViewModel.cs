using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class KisiDuzenleViewModel : AppUser
    {

        private AppRole Rol { get; set; }
        public int RolId { get; set; }
        public string RolAdi { get; set; }
        public List<SelectListItem> RollerListesi { get; set; }
        public string Signature { get; set; }
        public KisiDuzenleViewModel SecilenKisi { get; set; }

        public IdentityUserRole<int> EskiRol { get; set; }
        public IdentityUserRole<int> YeniRol { get; set; }
        public AppUser UpdateOlacakKullanici { get; set; }


        public async Task<IdentityUserRole<int>> KullanicininEskiRolunuGetirAsync(VeterinerDBContext context, KisiDuzenleViewModel model)
        {

            return await context.UserRoles.Where(ur => ur.UserId == model.Id).FirstOrDefaultAsync();


        }
        public async Task<IdentityUserRole<int>> KullanicininYeniRolunuGetirAsync(KisiDuzenleViewModel model) 
        {
            return new IdentityUserRole<int>
            {
                UserId = model.Id,
                RoleId = model.RolId
            };

        }
        public async Task<List<SelectListItem>> RollerListesiniGetirAsync(VeterinerDBContext context)
        {
            RollerListesi = new();
            var roller = await context.Roles.ToListAsync();

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
        public async Task<KisiDuzenleViewModel> SecilenKisiyiGetirAsync(VeterinerDBContext context, KisiDuzenleViewModel model)
        {
            var secilenKisi = await context.Users.Where(u => u.InsanTckn == model.InsanTckn).FirstOrDefaultAsync();

            model.Id = secilenKisi.Id;
            model.InsanAdi = secilenKisi.InsanAdi;
            model.InsanSoyadi = secilenKisi.InsanSoyadi;
            model.CalisiyorMu = secilenKisi.CalisiyorMu;
            model.Email = secilenKisi.Email;
            model.PhoneNumber = secilenKisi.PhoneNumber;
            model.DiplomaNo = secilenKisi.DiplomaNo;
            model.UserName = secilenKisi.UserName;
            model.Maas = secilenKisi.Maas;
            model.RollerListesi = await RollerListesiniGetirAsync(context);
            model.Rol = await context.Roles
                .Where(r => r.Id == context.UserRoles
                      .Where(ur => ur.UserId == secilenKisi.Id)
                      .Select(ur => ur.RoleId)
                      .FirstOrDefault())
                .FirstOrDefaultAsync();
            model.RolId = this.Rol.Id;
            model.RolAdi = this.Rol.Name;
            
            return model;
        }
        public async Task<AppUser> UpdateOlacakKullaniciyiGetirAsync(VeterinerDBContext context, KisiDuzenleViewModel model)
        {
            var UpdateOlacakKullanici = await context.AppUsers.FindAsync(model.Id);

            UpdateOlacakKullanici.InsanAdi = model.InsanAdi.ToUpper();
            UpdateOlacakKullanici.InsanSoyadi = model.InsanSoyadi.ToUpper();
            UpdateOlacakKullanici.CalisiyorMu = model.CalisiyorMu;
            UpdateOlacakKullanici.Email = model.Email.ToLower();
            UpdateOlacakKullanici.PhoneNumber = model.PhoneNumber;
            UpdateOlacakKullanici.DiplomaNo = model.DiplomaNo;
            UpdateOlacakKullanici.UserName = model.UserName.ToUpper();
            UpdateOlacakKullanici.Maas = model.Maas;

            return UpdateOlacakKullanici;
        }

    }
}


