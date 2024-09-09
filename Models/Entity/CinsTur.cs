using System.Collections.Generic;

namespace VeterinerApp.Models.Entity
{
    public class CinsTur
    {
        public int Id { get; set; }
        public int CinsId { get; set; }
        public virtual Cins Cins { get; set; }
        public int TurId { get; set; }
        public virtual Tur Tur { get; set; }
        public virtual ICollection<Hayvan> Hayvanlar { get; set; }
    }
}
