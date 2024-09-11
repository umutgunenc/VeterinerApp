using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Renk
    {
        public Renk()
        {
            Hayvanlar = new HashSet<Hayvan>();
        }
        public int RenkId { get; set; }
        public string RenkAdi { get; set; }

        public virtual ICollection<Hayvan> Hayvanlar { get; set; }
    }
}
