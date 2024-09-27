using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.ViewComponents
{
    public class StokGirisDetayGetirViewComponent : ViewComponent
    {

  
        public async Task<IViewComponentResult> InvokeAsync(StokGirisKaydetViewModel model)
        {
            
            if(model == null)
            {
                return View("StokGiris");
            }

            return View(model);

        }
    }
}
