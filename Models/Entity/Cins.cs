using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Cins
    {
        public Cins()
        {
            Turler = new HashSet<CinsTur>();
        }
        public int CinsId { get; set; }
        public string CinsAdi { get; set; }

        public virtual ICollection<CinsTur> Turler { get; set; }
    }
}
