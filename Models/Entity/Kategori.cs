using System.Collections.Generic;

namespace VeterinerApp.Models.Entity
{
    public class Kategori
    {

        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }

        public virtual ICollection<Stok> Stoklar { get; set; }
    }
}
