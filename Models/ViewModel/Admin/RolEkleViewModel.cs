using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class RolEkleViewModel
    {
        public int RolId { get; set; }
        public string RolAdi { get; set; }
        public List<SelectListItem> Roller { get; set; }
    }
}
