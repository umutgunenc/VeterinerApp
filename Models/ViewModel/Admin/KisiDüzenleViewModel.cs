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
        private readonly VeterinerDBContext _context;


        private readonly KisiSecViewModel _model;

        public KisiDuzenleViewModel()
        {

        }


        public KisiDuzenleViewModel(VeterinerDBContext context, KisiSecViewModel model)
        {
            _context = context;
            _model = model;
            SecilenKisiyiGetirAsync().Wait();
        }
        private IdentityUserRole<int> UserRole { get; set; }
        private AppRole Rol { get; set; }
        public int RolId { get; set; }
        public string RolAdi { get; set; }
        public List<SelectListItem> RollerListesi { get; set; }
        public async Task<IdentityUserRole<int>> UserRoleGetirAsync(VeterinerDBContext context, KisiDuzenleViewModel model) 
        {
            UserRole = context.UserRoles.Where(ur => ur.RoleId == model.RolId).FirstOrDefault();
            return UserRole;
        }
        private async Task<List<SelectListItem>> RollerListesiniGetirAsync()
        {
            RollerListesi = new();
            var roller = await _context.Roles.ToListAsync();

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
        private async Task<KisiDuzenleViewModel> SecilenKisiyiGetirAsync()
        {
            var secilenKisi = await _context.Users.Where(u => u.InsanTckn == _model.InsanTckn).FirstOrDefaultAsync();

            Id = secilenKisi.Id;
            InsanAdi = secilenKisi.InsanAdi;
            InsanSoyadi = secilenKisi.InsanSoyadi;
            CalisiyorMu = secilenKisi.CalisiyorMu;
            Email = secilenKisi.Email;
            PhoneNumber = secilenKisi.PhoneNumber;
            DiplomaNo = secilenKisi.DiplomaNo;
            UserName = secilenKisi.UserName;
            Maas = secilenKisi.Maas;
            RollerListesi = await RollerListesiniGetirAsync();
            Rol = await _context.Roles
                .Where(r => r.Id == _context.UserRoles
                      .Where(ur => ur.UserId == secilenKisi.Id)
                      .Select(ur => ur.RoleId)
                      .FirstOrDefault())
                .FirstOrDefaultAsync();
            RolId = this.Rol.Id;
            RolAdi = this.Rol.Name;


            //AccessFailedCount = secilenKisi.AccessFailedCount;
            //ConcurrencyStamp = secilenKisi.ConcurrencyStamp;
            //EmailConfirmed = secilenKisi.EmailConfirmed;
            //Hayvanlar = secilenKisi.Hayvanlar;
            //ImgURL = secilenKisi.ImgURL;
            //LockoutEnabled = secilenKisi.LockoutEnabled;
            //LockoutEnd = secilenKisi.LockoutEnd;
            //MaasOdemeleri = secilenKisi.MaasOdemeleri;
            //Muayeneler = secilenKisi.Muayeneler;
            //NormalizedEmail = secilenKisi.NormalizedEmail;
            //NormalizedUserName = secilenKisi.NormalizedUserName;
            //PasswordHash = secilenKisi.PasswordHash;
            //SecurityStamp = secilenKisi.SecurityStamp;
            //SifreGecerlilikTarihi = secilenKisi.SifreGecerlilikTarihi;
            //SifreOlusturmaTarihi = secilenKisi.SifreOlusturmaTarihi;
            //TermOfUse = secilenKisi.TermOfUse;

            return this;
        }
        public async Task<AppUser> UpdateOlacakKullaniciyiGetirAsync(VeterinerDBContext context, KisiDuzenleViewModel Model)
        {
            var UpdateOlacakKullanici = await context.AppUsers.FindAsync(Model.Id);

            UpdateOlacakKullanici.InsanAdi = Model.InsanAdi.ToUpper();
            UpdateOlacakKullanici.InsanSoyadi = Model.InsanSoyadi.ToUpper();
            UpdateOlacakKullanici.CalisiyorMu = Model.CalisiyorMu;
            UpdateOlacakKullanici.Email = Model.Email.ToLower();
            UpdateOlacakKullanici.PhoneNumber = Model.PhoneNumber;
            UpdateOlacakKullanici.DiplomaNo = Model.DiplomaNo;
            UpdateOlacakKullanici.UserName = Model.UserName.ToUpper();
            UpdateOlacakKullanici.Maas = Model.Maas;

            return UpdateOlacakKullanici;
        }

    }
}


