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
        var secilenKisi = await _veterinerDbContext.Users
            .Where(x => x.InsanTckn == model.InsanTckn)
            .Select(x => new CalisanListeleViewModel
            {
                InsanAdi = x.InsanAdi,
                InsanSoyadi = x.InsanSoyadi,
                InsanTckn = x.InsanTckn,
                Email = x.Email.ToLower(),
                PhoneNumber = x.PhoneNumber,
                DiplomaNo = x.DiplomaNo,
                UserName = x.UserName,
                CalisiyorMu = x.CalisiyorMu,
                Maas = x.Maas,
                RolId = _veterinerDbContext.UserRoles
                        .Where(u => u.UserId == x.Id)
                        .Select(r => r.RoleId)
                        .ToList(),
                RolAdi = _veterinerDbContext.Roles
                        .Where(r => r.Id == _veterinerDbContext.UserRoles
                                            .Where(ur => ur.UserId == x.Id)
                                            .Select(ur => ur.RoleId)
                                            .FirstOrDefault())
                        .Select(r => r.Name)
                        .FirstOrDefault()
            }).FirstOrDefaultAsync();

        return View(secilenKisi);
    }
}
