using Microsoft.AspNetCore.Mvc;
using VeterinerApp.Models.Validators;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Login;
using FluentValidation.Results;
using System.Linq;
using VeterinerApp.Models.Entity;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;


namespace VeterinerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly VeterinerContext _context;
        public HomeController(VeterinerContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
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

            if (sifreBilgileri == null) { 
                ModelState.AddModelError("sifre", "Kullanıcı adı veya şifre hatalı."); 
                return View(); 
            }

            model.SifreGecerlilikTarihi = sifreBilgileri.SifreGecerlilikTarihi;

            LoginValidators validator = new LoginValidators(_context);
            ValidationResult result = validator.Validate(model);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.ErrorMessage);
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.KullaniciAdi)
            };

            var userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("AdminIndex", "Admin");
        }
    }
}
