using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Cins
    {
        public Cins()
        {
            TurCins = new HashSet<TurCins>();
        }

        public int Id { get; set; }
        public string cins { get; set; }

        public virtual ICollection<TurCins> TurCins { get; set; }
    }
}
