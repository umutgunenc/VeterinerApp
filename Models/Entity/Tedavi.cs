using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Tedavi
    {

        public int TedaviId { get; set; }
        public string TedaviAdi { get; set; }
        public double TedaviUcreti { get; set; }

        public virtual ICollection<TedaviMuayene> TedaviMuayeneler { get; set; }
    }
}
