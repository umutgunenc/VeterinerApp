using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Fonksiyonlar;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.ViewComponents
{
    public class StokDetayGetirViewComponent : ViewComponent
    {
        
        public async Task<IViewComponentResult> InvokeAsync(StokDuzenleKaydetViewModel model)
        {
            if (model == null)
            {
                return View("StokDuzenle");
            }            

            return View(model);
        }
    }
}
