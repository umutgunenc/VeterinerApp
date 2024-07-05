using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class SahipHayvan
    {
        public string SahipTckn { get; set; }
        public int HayvanId { get; set; }
        public DateTime SahiplikTarihi { get; set; }
        public DateTime? SahiplikCikisTarihi { get; set; }

        public virtual Hayvan Hayvan { get; set; }
        public virtual Insan SahipTcknNavigation { get; set; }
    }
}
