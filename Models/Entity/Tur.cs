using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Tur
    {

        public int TurId { get; set; }
        public string TurAdi { get; set; }

        public virtual ICollection<CinsTur> CinsTur { get; set; }
    }
}
