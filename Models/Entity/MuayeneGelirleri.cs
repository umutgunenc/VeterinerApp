using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class MuayeneGelirleri
    {
        public int MuayeneNo { get; set; }
        public double Gelir { get; set; }

        public virtual Muayene MuayeneNoNavigation { get; set; }
    }
}
