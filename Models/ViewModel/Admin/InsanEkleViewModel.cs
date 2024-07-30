using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class InsanEkleViewModel : Insan
    {
        public string rolId { get; set; }
        public List<SelectListItem> Roller { get; set; }

    }
}
