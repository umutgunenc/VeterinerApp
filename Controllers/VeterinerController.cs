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
        private readonly VeterinerDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public VeterinerController(VeterinerDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult MuayeneEt()
        {
            return View();
        }

    }
}
