using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class StokHareket
    {
        public int StokHareketId { get; set; }
        public DateTime? StokHareketTarihi { get; set; }
        public int StokId { get; set; }
        public DateTime? SatisTarihi { get; set; }
        public double? SatisFiyati { get; set; }
        public DateTime? AlisTarihi { get; set; }
        public double? AlisFiyati { get; set; }
        public int CalisanId{ get; set; }
        public double? StokGirisAdet { get; set; }
        public double? StokCikisAdet { get; set; }

        public virtual AppUser Calisan { get; set; }
        public virtual Stok Stok { get; set; }
    }
}
