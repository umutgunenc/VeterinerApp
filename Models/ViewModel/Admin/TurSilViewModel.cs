using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class TurSilViewModel
    {
        public int Id { get; set; }
        public string tur { get; set; }
        public List<SelectListItem> Turler { get; set; }
    }
}
