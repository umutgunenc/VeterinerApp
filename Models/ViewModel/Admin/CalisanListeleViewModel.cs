using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class CalisanListeleViewModel : AppUser
    {
        public object RolId { get; internal set; }
        public object RolAdi { get; internal set; }
    }
}
