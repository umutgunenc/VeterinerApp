using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Hayvan
    {
        public Hayvan()
        {
            InverseHayvanAnne = new HashSet<Hayvan>();
            InverseHayvanBaba = new HashSet<Hayvan>();
            Muayenes = new HashSet<Muayene>();
            SahipHayvans = new HashSet<SahipHayvan>();
        }

        public int HayvanId { get; set; }
        public string HayvanAdi { get; set; }
        public string HayvanCinsiyet { get; set; }
        public double HayvanKilo { get; set; }
        public DateTime HayvanDogumTarihi { get; set; }
        public DateTime? HayvanOlumTarihi { get; set; }
        public int RenkId { get; set; }
        public int TurId { get; set; }
        public int CinsId { get; set; }
        public int? HayvanAnneId { get; set; }
        public int? HayvanBabaId { get; set; }

        public virtual Hayvan HayvanAnne { get; set; }
        public virtual Hayvan HayvanBaba { get; set; }
        public virtual Renk Renk { get; set; }
        public virtual TurCins TurCin { get; set; }
        public virtual ICollection<Hayvan> InverseHayvanAnne { get; set; }
        public virtual ICollection<Hayvan> InverseHayvanBaba { get; set; }
        public virtual ICollection<Muayene> Muayenes { get; set; }
        public virtual ICollection<SahipHayvan> SahipHayvans { get; set; }
    }
}
