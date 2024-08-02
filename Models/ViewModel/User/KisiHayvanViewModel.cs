using System.Collections.Generic;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.User
{
    public class KisiHayvanViewModel
    {
        public AppUser User { get; set; }
        public List<HayvanlarViewModel> hayvanlar { get; set; }
    }
}
