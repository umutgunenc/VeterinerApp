using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Renk
    {
        public Renk()
        {
            Hayvans = new HashSet<Hayvan>();
        }

        public int Id { get; set; }
        public string renk { get; set; }

        public virtual ICollection<Hayvan> Hayvans { get; set; }
    }
}
