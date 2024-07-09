using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Admin;

public class CalisanlarListeleViewComponent : ViewComponent
{
    private readonly VeterinerContext _veterinerDbContext;

    public CalisanlarListeleViewComponent(VeterinerContext veterinerDbContext)
    {
        _veterinerDbContext = veterinerDbContext;
    }

    public async Task<IViewComponentResult> InvokeAsync(CalisanListeleViewModel model)
    {

        var secilenKisi = await _veterinerDbContext.Insans
            .Where(x => x.InsanTckn == model.InsanTckn)
            .Select(x => new CalisanListeleViewModel
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
                RolAdi = _veterinerDbContext.Rols
                    .Where(r => r.RolId == x.RolId)
                    .Select(r => r.RolAdi)
                    .FirstOrDefault()
            }).FirstOrDefaultAsync();

        return View(secilenKisi);
    }
}
