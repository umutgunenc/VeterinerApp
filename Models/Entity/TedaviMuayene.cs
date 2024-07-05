using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class TedaviMuayene
    {
        public int TedaviTedaviId { get; set; }
        public int MuayeneId { get; set; }

        public virtual Muayene Muayene { get; set; }
        public virtual Tedavi TedaviTedavi { get; set; }
    }
}
