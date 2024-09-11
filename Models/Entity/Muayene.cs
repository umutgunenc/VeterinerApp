using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Muayene
    {
        public Muayene()
        {
            Stoklar = new HashSet<Stok>();
            Tedaviler = new HashSet<Tedavi>();
        }
        public int MuayeneId { get; set; }
        public int TedaviId { get; set; }
        public int HayvanId { get; set; }
        public DateTime MuayeneTarihi { get; set; }
        public DateTime? SonrakiMuayeneTarihi { get; set; }
        public string Aciklama { get; set; }
        public int HekimId { get; set; }
        public int StokId { get; set; }
        public double Gelir { get; set; }
        public virtual Hayvan Hayvan { get; set; }
        public virtual AppUser Hekim { get; set; }
        public virtual ICollection<Stok> Stoklar { get; set; }
        public virtual ICollection<Tedavi> Tedaviler { get; set; }
    }
}
