using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.ViewModel.Login;
using VeterinerApp.Models.Validators;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;

namespace VeterinerApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly VeterinerContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, VeterinerContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            string kullaniciAdi = model.KullaniciAdi;
            Sifre sifreBilgileri = _context.Sifres.FirstOrDefault(x => x.KullaniciAdi == kullaniciAdi);

            if (sifreBilgileri == null)
            {
                ModelState.AddModelError("sifre", "Kullanıcı adı veya şifre hatalı.");
                return View();
            }

            model.SifreGecerlilikTarihi = sifreBilgileri.SifreGecerlilikTarihi;

            LoginValidators validator = new LoginValidators(_context);
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }

            var user = await _userManager.FindByNameAsync(kullaniciAdi);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, model.sifre, model.RememberMe, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                return View();
            }

            var insan = await _context.Insans.Include(i => i.Rol).FirstOrDefaultAsync(i => i.KullaniciAdi == user.UserName);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("FullName", insan.InsanAdi + " " + insan.InsanSoyadi),
                new Claim(ClaimTypes.Role, insan.Rol.RolAdi)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
