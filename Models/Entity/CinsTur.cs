using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinerApp.Models.Entity
{
    public class CinsTur
    {
        public CinsTur()
        {
            Hayvanlar = new HashSet<Hayvan>();
        }
        public int Id { get; set; }
        public int CinsId { get; set; }
        public virtual Cins Cins { get; set; }
        public int TurId { get; set; }
        public virtual Tur Tur { get; set; }
        public virtual ICollection<Hayvan> Hayvanlar { get; set; }
    }
}
