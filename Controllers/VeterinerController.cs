using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Controllers
{
    [Authorize(Roles = "VETERİNER")]
    public class VeterinerController : Controller
    {
        private readonly VeterinerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public VeterinerController(VeterinerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Muayene()
        {
            return View();
        }

    }
}
