using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class FiyatListesi
    {
        public int FiyatListesiId { get; set; }
        public int StokId { get; set; }
        public DateTime FiyatSatisGecerlilikBaslangicTarihi { get; set; }
        public DateTime? FiyatSatisGecerlilikBitisTarihi { get; set; }
        public double SatisFiyati { get; set; }
        public virtual Stok Stok { get; set; }
    }
}
