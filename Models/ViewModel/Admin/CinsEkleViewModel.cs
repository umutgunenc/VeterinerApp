using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class CinsEkleViewModel
    {
        public int Id { get; set; }
        public string cins { get; set; }
        public List<SelectListItem> Cinsler { get; set; }
    }
}
