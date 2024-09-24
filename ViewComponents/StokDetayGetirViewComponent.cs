using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Fonksiyonlar;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.ViewComponents
{
    public class StokDetayGetirViewComponent : ViewComponent
    {
        private readonly VeterinerDBContext _context;

        public StokDetayGetirViewComponent(VeterinerDBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(StokDuzenleViewModel model)
        {
            if (model == null)
            {
                return View("StokDuzenle");
            }

            StokDetayGetirViewModel stokDetay = new(_context);
            await stokDetay.ModeliOlusturAsync(model);

            stokDetay.Signature = Signature.CreateSignature(stokDetay.Id, stokDetay.Id.ToString());

            return View(stokDetay);
        }
    }
}
