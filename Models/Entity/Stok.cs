using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Stok
    {
        public Stok()
        {
            FiyatListesis = new HashSet<FiyatListesi>();
            StokHarekets = new HashSet<StokHareket>();
        }

        public string StokBarkod { get; set; }
        public string StokAdi { get; set; }
        public int StokSayisi { get; set; }

        public virtual Ilac Ilac { get; set; }
        public virtual ICollection<FiyatListesi> FiyatListesis { get; set; }
        public virtual ICollection<StokHareket> StokHarekets { get; set; }
    }
}
