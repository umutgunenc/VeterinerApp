using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Muayene
    {
        public Muayene()
        {
            IlacMuayenes = new HashSet<IlacMuayene>();
            TedaviMuayenes = new HashSet<TedaviMuayene>();
        }

        public int MuayeneId { get; set; }
        public int MuayeneNo { get; set; }
        public int TedaviId { get; set; }
        public int HayvanId { get; set; }
        public DateTime MuayeneTarihi { get; set; }
        public DateTime? SonrakiMuayeneTarihi { get; set; }
        public string Aciklama { get; set; }
        public string HekimkTckn { get; set; }
        public string IlacBarkod { get; set; }

        public virtual Hayvan Hayvan { get; set; }
        public virtual AppUser HekimkTcknNavigation { get; set; }
        public virtual MuayeneGelirleri MuayeneGelirleri { get; set; }
        public virtual ICollection<IlacMuayene> IlacMuayenes { get; set; }
        public virtual ICollection<TedaviMuayene> TedaviMuayenes { get; set; }
    }
}
