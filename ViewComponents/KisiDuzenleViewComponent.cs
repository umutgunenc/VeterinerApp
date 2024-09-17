using Microsoft.AspNetCore.Mvc;
using VeterinerApp.Models.ViewModel.Admin;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace VeterinerApp.ViewComponents
{
    public class KisiDuzenleViewComponent : ViewComponent
    {   private readonly VeterinerDBContext _context;

        public KisiDuzenleViewComponent(VeterinerDBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(KisiDuzenleViewModel model)
        {
            
            if (model == null)
            {
                return View("KisiSec");
            }
            //model = await model.SecilenKisiyiGetirAsync(_context, model);
            return View(model);
        }
    }
}
