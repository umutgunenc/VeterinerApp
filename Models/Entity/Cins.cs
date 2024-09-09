using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Cins
    {
        public int CinsId { get; set; }
        public string CinsAdi { get; set; }

        public virtual ICollection<CinsTur> CinsTur { get; set; }
    }
}
