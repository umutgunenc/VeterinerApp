using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class RolSilViewModel :IdentityRole
    {
        public List<SelectListItem> Roller { get; set; }
    }
}
