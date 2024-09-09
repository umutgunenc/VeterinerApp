using System;
using System.Collections;
using System.Collections.Generic;

namespace VeterinerApp.Models.Entity
{
    public class SahipHayvan
    {
        public int Id { get; set; }
        public int HayvanId { get; set; }
        public int SahipId { get; set; }
        public string SahipTckn { get; set; }
        public DateTime SahiplikTarihi { get; set; }
        public DateTime? SahiplikCikisTarihi { get; set; }
        public virtual Hayvan Hayvan{ get; set; }
        public virtual AppUser Sahip { get; set; }
    }
}
