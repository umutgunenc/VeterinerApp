using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class RolEkleViewModel : IdentityRole
    {
        public List<SelectListItem> Roller { get; set; }
    }
}
