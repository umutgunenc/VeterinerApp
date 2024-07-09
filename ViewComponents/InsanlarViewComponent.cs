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
        private readonly VeterinerContext _veterinerDbContext;

        public InsanlarViewComponent(VeterinerContext veterinerDbContext)
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

            var secilenKisi = await _veterinerDbContext.Insans
                .Where(x => x.InsanTckn == secilenKisiTCKN)
                .Select(x => new InsanDuzenleViewModel
                {
                    InsanAdi = x.InsanAdi,
                    InsanSoyadi = x.InsanSoyadi,
                    InsanTckn = x.InsanTckn,
                    InsanMail = x.InsanMail.ToLower(),
                    InsanTel = x.InsanTel,
                    DiplomaNo = x.DiplomaNo,
                    KullaniciAdi = x.KullaniciAdi,
                    CalisiyorMu = x.CalisiyorMu,
                    Maas = x.Maas,
                    RolId = x.RolId,
                    Roller = _veterinerDbContext.Rols.Select(r => new SelectListItem
                    {
                        Value = r.RolId.ToString(),
                        Text = r.RolAdi
                    }).ToList(),
                    RolAdi = _veterinerDbContext.Rols
                        .Where(r => r.RolId == x.RolId)
                        .Select(r => r.RolAdi)
                        .FirstOrDefault()
                }).FirstOrDefaultAsync();


            return View(secilenKisi);
        }
    }
}
