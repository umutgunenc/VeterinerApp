using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Rol :IdentityRole
    {
        public Rol()
        {
            Insans = new HashSet<Insan>();
        }

        public virtual ICollection<Insan> Insans { get; set; }
    }
}
