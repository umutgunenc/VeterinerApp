using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.ViewModel.User;

namespace VeterinerApp.Controllers
{
    [Authorize]
    public class AnimalController : Controller
    {

        [HttpPost]
        public IActionResult Information(HayvanlarViewModel hayvan)
        {


            return View();
        }
    }
}
