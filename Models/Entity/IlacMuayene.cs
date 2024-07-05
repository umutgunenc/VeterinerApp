using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class IlacMuayene
    {
        public string IlacIlacBarkod { get; set; }
        public int MuayeneId { get; set; }

        public virtual Ilac IlacIlacBarkodNavigation { get; set; }
        public virtual Muayene Muayene { get; set; }
    }
}
