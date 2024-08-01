using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.User
{
    public class HayvanlarViewModel :Hayvan
    {
        public string TurAdi { get; set; }
        public string CinsAdi { get; set; }

        public string RenkAdi { get; set; }
        public List<SelectListItem> Hayvanlar { get; set; }

    }
}
