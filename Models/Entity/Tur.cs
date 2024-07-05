using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Tur
    {
        public Tur()
        {
            TurCins = new HashSet<TurCins>();
        }

        public int Id { get; set; }
        public string tur { get; set; }

        public virtual ICollection<TurCins> TurCins { get; set; }
    }
}
