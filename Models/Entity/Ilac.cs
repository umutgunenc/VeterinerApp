using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Ilac
    {
        public Ilac()
        {
            IlacMuayenes = new HashSet<IlacMuayene>();
        }

        public string IlacAdi { get; set; }
        public string IlacBarkod { get; set; }

        public virtual Stok IlacBarkodNavigation { get; set; }
        public virtual ICollection<IlacMuayene> IlacMuayenes { get; set; }
    }
}
