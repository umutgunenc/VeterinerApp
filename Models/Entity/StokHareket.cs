using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class StokHareket
    {
        public int StokHareketId { get; set; }
        public DateTime? StokHareketTarihi { get; set; }
        public string StokBarkod { get; set; }
        public DateTime? SatisTarihi { get; set; }
        public double? SatisFiyati { get; set; }
        public DateTime? AlisTarihi { get; set; }
        public double? AlisFiyati { get; set; }

        public virtual Stok StokBarkodNavigation { get; set; }
    }
}
