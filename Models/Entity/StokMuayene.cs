using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class StokMuayene
    {
        public int Id { get; set; }
        public string IlacBarkod { get; set; }
        public int MuayeneId { get; set; }


        public virtual Stok Stok { get; set; }
        public virtual Muayene Muayene { get; set; }
    }
}
