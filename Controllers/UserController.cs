using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.ViewModel.User;

namespace VeterinerApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly VeterinerContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserController(VeterinerContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Information()
        {
            var user = await _userManager.GetUserAsync(User);

            // Kullanıcının sahip olduğu hayvanların ID'lerini al
            var hayvanIdler = _context.SahipHayvans
                .Where(s => s.SahipTckn == user.InsanTckn)
                .Select(h => h.HayvanId)
                .ToList();

            // Hayvanların detaylarını al
            List<HayvanlarViewModel> hayvanlar = await _context.Hayvans
                .Where(h => hayvanIdler.Contains(h.HayvanId))
                .Select(h => new HayvanlarViewModel
                {
                    HayvanId = h.HayvanId,
                    HayvanAdi = h.HayvanAdi,
                    TurAdi = _context.Turs.Where(t => t.Id == h.TurId).Select(t => t.tur).FirstOrDefault(),
                    CinsAdi = _context.Cins.Where(c => c.Id == h.CinsId).Select(c => c.cins).FirstOrDefault(),
                    RenkAdi = _context.Renks.Where(r => r.Id == h.RenkId).Select(r => r.renk).FirstOrDefault()
                })
                .ToListAsync();

            var model = (user, hayvanlar);
            return View(model);

        }
    }
}
