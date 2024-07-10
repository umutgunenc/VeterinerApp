using Microsoft.AspNetCore.Mvc;
using VeterinerApp.Models.Validators;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Login;
using FluentValidation.Results;
using System.Linq;
using VeterinerApp.Models.Entity;
using FluentValidation;


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
        public IActionResult Login(LoginViewModel model)
        {
            string kullaniciAdi = model.KullaniciAdi;
            Sifre sifreBilgileri = _context.Sifres.FirstOrDefault(x => x.KullaniciAdi == kullaniciAdi);

            model.SifreGecerlilikTarihi = sifreBilgileri.SifreGecerlilikTarihi;


            LoginValidators validator = new LoginValidators(_context);
            ValidationResult result = validator.Validate(model);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.ErrorMessage);
            }

            return RedirectToAction("AdminIndex", "Admin");
        }
    }
}
