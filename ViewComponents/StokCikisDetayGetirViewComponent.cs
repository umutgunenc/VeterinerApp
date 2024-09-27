using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.ViewComponents
{
    public class StokCikisDetayGetirViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(StokCikisKaydetViewModel model)
        {

            if (model == null)
            {
                return View("StokCikis");
            }

            return View(model);

        }
    }
}
