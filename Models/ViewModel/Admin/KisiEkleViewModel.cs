using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IdentityUserRole<int> KullaniciRolu { get; set; }
        public KisiEkleViewModel Kullanici { get; set; }

        public async Task<List<SelectListItem>> RollerListesiniGetirAsync(VeterinerDBContext context)
        {
            var roller = await context.Roles.ToListAsync();
            RollerListesi = new();
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

        public async Task<KisiEkleViewModel> KisiOlusturAsync(VeterinerDBContext context, KisiEkleViewModel model)
        {
            UserName = await KullaniciAdiOlustur.GenerateUserNameAsync(model.InsanAdi, model.InsanSoyadi, model.Email, context);
            InsanAdi = model.InsanAdi;
            InsanSoyadi = model.InsanSoyadi;
            InsanTckn = model.InsanTckn;
            Email = model.Email;
            PhoneNumber = model.PhoneNumber;
            DiplomaNo = model.DiplomaNo;
            CalisiyorMu = true;
            SifreOlusturmaTarihi = DateTime.Now;
            SifreGecerlilikTarihi = DateTime.Now.AddDays(120);
            TermOfUse = true;
            KullaniciSifresi = SifreOlustur.GeneratePassword(8);

            return this;

        }

        public async Task<IdentityUserRole<int>> KisininRolunuGetirAsync(VeterinerDBContext _context, KisiEkleViewModel model)
        {
            var kullanici = await _context.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName);
            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == model.RolId);
            KullaniciRolu = new();
            KullaniciRolu.UserId = kullanici.Id;
            KullaniciRolu.RoleId = userRole.Id;

            return KullaniciRolu;

        }
    }
}
