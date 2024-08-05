using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.Account;
using VeterinerApp.Models.ViewModel.Account;
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
        [HttpGet]
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
                    HayvanCinsiyet = h.HayvanCinsiyet,
                    HayvanKilo = h.HayvanKilo,
                    HayvanDogumTarihi = h.HayvanDogumTarihi,
                    HayvanOlumTarihi = h.HayvanOlumTarihi,
                    HayvanAnneAdi = _context.Hayvans.Where(ha => ha.HayvanId == h.HayvanAnneId).Select(ha => ha.HayvanAdi).FirstOrDefault(),
                    HayvanBabaAdi = _context.Hayvans.Where(hb => hb.HayvanId == h.HayvanBabaId).Select(hb => hb.HayvanAdi).FirstOrDefault(),
                    TurAdi = _context.Turs.Where(t => t.Id == h.TurId).Select(t => t.tur).FirstOrDefault(),
                    CinsAdi = _context.Cins.Where(c => c.Id == h.CinsId).Select(c => c.cins).FirstOrDefault(),
                    RenkAdi = _context.Renks.Where(r => r.Id == h.RenkId).Select(r => r.renk).FirstOrDefault(),

                })
                .ToListAsync();

            var tuplle = (user, hayvanlar);
            return View(tuplle);

        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePaswordViewModel model)
        {

            var user = _userManager.GetUserAsync(User).Result;

            ChangePaswordValidators Validator = new ChangePaswordValidators();
            var result = Validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }

            var resultChangePasword = _userManager.ChangePasswordAsync(user,model.OldPassword, model.NewPassword);
            if(!resultChangePasword.Result.Succeeded)
            {
                ModelState.AddModelError("OldPassword", "Eski şifreniz yanlış.");
                return View();
            }
            else
            {
                user.SifreOlusturmaTarihi = System.DateTime.Now;
                user.SifreGecerlilikTarihi = System.DateTime.Now.AddDays(120);
                _userManager.UpdateAsync(user);
                TempData["PasswordChanged"] = $"Sayın {user.UserName}, Şifreniz başarıyla değiştirildi.";
                return View();
            }

        }
    }
}
