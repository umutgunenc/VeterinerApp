using System.Collections.Generic;

namespace VeterinerApp.Models.Entity
{
    public class Birim
    {

        public int BirimId { get; set; }
        public string BirimAdi { get; set; }
        public virtual ICollection<Stok> Stoklar { get; set; }

    }
}
