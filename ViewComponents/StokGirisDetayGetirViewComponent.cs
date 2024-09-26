using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.ViewComponents
{
    public class StokGirisDetayGetirViewComponent : ViewComponent
    {
        private readonly VeterinerDBContext _context;

        public StokGirisDetayGetirViewComponent(VeterinerDBContext context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync(StokGirisViewModel model)
        {
            
            if(model == null)
            {
                return View("StokGiris");
            }
            StokGirisKaydetViewModel stokDetay = new();
            var (isSuccess, aramaSonucu) = await stokDetay.AramaSonucunuGetirAsync(model, _context);
            if (!isSuccess)
            {
                ModelState.AddModelError("StokId", "Aradığınız stoğa ait bir kayıt bulunamadı");
                return View("StokGiris", model);
            }


            return View(aramaSonucu);

        }
    }
}
