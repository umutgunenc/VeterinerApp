using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Rol 
    {
        public Rol()
        {
            Insans = new HashSet<Insan>();
        }

        public int RolId { get; set; }
        public string RolAdi { get; set; }

        public virtual ICollection<Insan> Insans { get; set; }
    }
}
