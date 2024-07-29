using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class InsanDuzenleViewModel : Insan
    {
        public string RolId { get; set; }
        public string RolAdi { get; set; }
        public List<SelectListItem> Roller { get; set; }
        public List<SelectListItem> Insanlar { get; set; }

    }
}
