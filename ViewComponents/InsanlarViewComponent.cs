using Microsoft.AspNetCore.Mvc;
using VeterinerApp.Models.ViewModel.Admin;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace VeterinerApp.ViewComponents
{
    public class InsanlarViewComponent : ViewComponent
    {
        private readonly VeterinerDBContext _veterinerDbContext;

        public InsanlarViewComponent(VeterinerDBContext veterinerDbContext)
        {
            _veterinerDbContext = veterinerDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(InsanDuzenleViewModel model)
        {
            if (model == null)
            {
                return View("CalisanSec");
            }

            var secilenKisiTCKN = model.InsanTckn;

            var secilenKisi = _veterinerDbContext.Users
                .Where(x => x.InsanTckn == secilenKisiTCKN)
                .Select(x => new InsanDuzenleViewModel
                {
                    Id = x.Id,
                    InsanAdi = x.InsanAdi,
                    InsanSoyadi = x.InsanSoyadi,
                    InsanTckn = x.InsanTckn,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    DiplomaNo = x.DiplomaNo,
                    UserName = x.UserName,
                    CalisiyorMu = x.CalisiyorMu,
                    Maas = x.Maas,
                    rolId = _veterinerDbContext.UserRoles.Where(r => r.UserId == x.Id)
                        .Select(r => r.RoleId)
                        .FirstOrDefault(),
                    Roller = _veterinerDbContext.Roles.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList(),
                    rolAdi = _veterinerDbContext.Roles
                        .Where(r => r.Id == _veterinerDbContext.UserRoles.Where(r => r.UserId == x.Id)
                        .Select(r => r.RoleId)
                        .FirstOrDefault())
                        .Select(r => r.Name)
                        .FirstOrDefault()
                }).FirstOrDefault();


            return View(secilenKisi);
        }
    }
}
