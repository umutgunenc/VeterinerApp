using Microsoft.AspNetCore.Mvc;
using VeterinerApp.Models.Validators;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Login;
using FluentValidation.Results;


namespace VeterinerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly VeterinerContext _context;
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
            if(model==null)
            {
                return BadRequest();
            }
            LoginValidators validator = new LoginValidators(_context);
            ValidationResult result = validator.Validate(model);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.ErrorMessage);
            }

            return View();
        }
    }
}
