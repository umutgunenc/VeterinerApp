using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinerApp.Models.Entity
{
    public class SahipHayvan
    {
        [ForeignKey(nameof(HayvanId))]
        public int HayvanId { get; set; }
        public virtual Hayvan Hayvan { get; set; }

        [ForeignKey(nameof(SahipId))]
        public int SahipId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public DateTime SahiplenmeTarihi { get; set; }
        public DateTime? SahiplenmeCikisTarihi { get; set; }
    }
}
