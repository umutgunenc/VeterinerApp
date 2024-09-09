using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Calisan;

namespace VeterinerApp.Controllers
{
    [Authorize(Roles = "ÇALIŞAN")]
    public class CalisanController : Controller
    {

        private readonly VeterinerDBContext _context;

        public CalisanController(VeterinerDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult StokEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StokEkle(StokHareketViewModel model)
        {

            return View();
        }
    }
}
