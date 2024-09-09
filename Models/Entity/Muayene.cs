using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Muayene
    {
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
        public virtual ICollection<MuayeneStok> MuayeneStoklar { get; set; }
        public virtual ICollection<TedaviMuayene> TedaviMuayeneler { get; set; }
    }
}
