using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class TurCinsEkleViewModel
    {
        public int Id { get; set; }
        public int TurId { get; set; }
        public int CinsId { get; set; }

        public List<SelectListItem> Turler { get; set; } 
        public List<SelectListItem> Cinsler { get; set; } 
        public List<SelectListItem> TurlerCinsler { get; set; }
    }
}
