using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Stok
    {
        public Stok()
        {
            FiyatListeleri = new HashSet<FiyatListesi>();
            StokHareketleri = new HashSet<StokHareket>();
            Muayeneler = new HashSet<Muayene>();
        }
        public int Id { get; set; }
        public string StokBarkod { get; set; }
        public string StokAdi { get; set; }
        public int StokSayisi { get; set; }
        public int BirimId { get; set; }
        public string Aciklama { get; set; }
        public bool AktifMi { get; set; }
        public int KategoriId { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual Birim Birim { get; set; }    

        public virtual ICollection<FiyatListesi> FiyatListeleri { get; set; }
        public virtual ICollection<StokHareket> StokHareketleri { get; set; }
        public virtual ICollection<Muayene> Muayeneler { get; set; }
    }
}
