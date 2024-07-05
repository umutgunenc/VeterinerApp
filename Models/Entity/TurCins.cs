using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class TurCins
    {
        public TurCins()
        {
            Hayvans = new HashSet<Hayvan>();
        }

        public int Id { get; set; }
        public int TurId { get; set; }
        public int CinsId { get; set; }

        public virtual Cins Cins { get; set; }
        public virtual Tur Tur { get; set; }
        public virtual ICollection<Hayvan> Hayvans { get; set; }
    }
}
