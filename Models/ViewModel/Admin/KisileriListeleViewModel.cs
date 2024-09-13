using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class KisileriListeleViewModel : AppUser
    {
        private readonly VeterinerDBContext _context;
        public KisileriListeleViewModel()
        {

        }
        public KisileriListeleViewModel(VeterinerDBContext context)
        {
            _context = context;
        }
        public IQueryable<KisileriListeleViewModel> KisilerListesi { get; set; }
        public string RolId { get; set; }
        public string RolAdi { get; set; }

        public KisileriListeleViewModel SecilenKisi(string tckn)
        {
            return _context.AppUsers.Where(x => x.InsanTckn == tckn).Select(kisi=>new KisileriListeleViewModel
            {
                InsanAdi = kisi.InsanAdi,
                InsanSoyadi = kisi.InsanSoyadi,
                InsanTckn = kisi.InsanTckn,
                Email = kisi.Email.ToLower(),
                PhoneNumber = kisi.PhoneNumber,
                DiplomaNo = kisi.DiplomaNo,
                UserName = kisi.UserName,
                CalisiyorMu = kisi.CalisiyorMu,
                Maas = kisi.Maas,
                RolAdi = _context.Roles
                        .Where(r => r.Id == _context.UserRoles
                                            .Where(ur => ur.UserId == kisi.Id)
                                            .Select(ur => ur.RoleId)
                                            .FirstOrDefault())
                        .Select(r => r.Name)
                        .FirstOrDefault()
            }).FirstOrDefault();
        }

        public IQueryable<KisileriListeleViewModel> KisiListesiniGetir()
        {
            var kisiler = _context.Users.Select(kisi => new KisileriListeleViewModel
            {
                InsanTckn = kisi.InsanTckn,
                InsanAdi = kisi.InsanAdi,
                InsanSoyadi = kisi.InsanSoyadi,
                CalisiyorMu = kisi.CalisiyorMu,
                Email = kisi.Email,
                PhoneNumber = kisi.PhoneNumber,
                RolAdi = _context.Roles
                        .Where(r => r.Id == _context.UserRoles
                                            .Where(ur => ur.UserId == kisi.Id)
                                            .Select(ur => ur.RoleId)
                                            .FirstOrDefault())
                        .Select(r => r.Name)
                        .FirstOrDefault()
            });

            return kisiler;

        }
    }
}
