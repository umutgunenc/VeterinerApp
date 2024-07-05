using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class RenkEkleViewModel
    {
        public int Id { get; set; }
        public string renk { get; set; }
        public List<SelectListItem> Renkler { get; set; }
    }
}
