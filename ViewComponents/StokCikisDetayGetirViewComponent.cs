using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.ViewComponents
{
    public class StokCikisDetayGetirViewComponent : ViewComponent
    {
        private readonly VeterinerDBContext _context;

        public StokCikisDetayGetirViewComponent(VeterinerDBContext context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync(StokCikisViewModel model)
        {
            
            if(model == null)
            {
                return View("StokGiris");
            }
            StokCikisKaydetViewModel stokDetay = new();
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
