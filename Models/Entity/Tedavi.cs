using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Tedavi
    {
        public Tedavi()
        {
            Muayeneler = new HashSet<Muayene>();
        }
        public int TedaviId { get; set; }
        public string TedaviAdi { get; set; }
        public double TedaviUcreti { get; set; }

        public virtual ICollection<Muayene> Muayeneler { get; set; }
    }
}
