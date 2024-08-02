using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.ViewModel.User;

namespace VeterinerApp.Controllers
{
    [Authorize]
    public class AnimalController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly VeterinerContext _context;
        public AnimalController(UserManager<AppUser> usermanager, VeterinerContext context)
        {
            _userManager = usermanager;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Information(int hayvanId)
        {
            var user = await _userManager.GetUserAsync(User);
            var userAnimalsIds = _context.SahipHayvans.Where(s => s.SahipTckn == user.InsanTckn).Select(h => h.HayvanId).ToList();
            var animal = await _context.Hayvans.FindAsync(hayvanId);

            if (userAnimalsIds.Contains(hayvanId))
            {
                var UserAnimal = new HayvanlarViewModel
                {
                    HayvanId = animal.HayvanId,
                    HayvanAdi = animal.HayvanAdi,
                    HayvanCinsiyet = animal.HayvanCinsiyet,
                    HayvanKilo = animal.HayvanKilo,
                    HayvanDogumTarihi = animal.HayvanDogumTarihi,
                    HayvanOlumTarihi = animal.HayvanOlumTarihi,
                    HayvanAnneAdi = _context.Hayvans.Where(ha => ha.HayvanId == animal.HayvanAnneId).Select(ha => ha.HayvanAdi).FirstOrDefault(),
                    HayvanBabaAdi = _context.Hayvans.Where(hb => hb.HayvanId == animal.HayvanBabaId).Select(hb => hb.HayvanAdi).FirstOrDefault(),
                    TurAdi = _context.Turs.Where(t => t.Id == animal.TurId).Select(t => t.tur).FirstOrDefault(),
                    CinsAdi = _context.Cins.Where(c => c.Id == animal.CinsId).Select(c => c.cins).FirstOrDefault(),
                    RenkAdi = _context.Renks.Where(r => r.Id == animal.RenkId).Select(r => r.renk).FirstOrDefault(),
                    Muayenes = _context.Muayenes.Where(m => m.HayvanId == animal.HayvanId).ToList(),

                };

                return View(UserAnimal);
            }
            else
                return View("BadRequest");
        }
    }
}
