using System;
using System.Collections.Generic;
using VeterinerApp.Models.Enum;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Hayvan
    {
        public Hayvan()
        {
            AnneninCocuklari = new HashSet<Hayvan>();
            BabaninCocuklari = new HashSet<Hayvan>();
            Muayeneler = new HashSet<Muayene>();
            Sahipler = new HashSet<SahipHayvan>();
        }
        public int HayvanId { get; set; }
        public string HayvanAdi { get; set; }
        public Cinsiyet HayvanCinsiyet { get; set; }
        public double HayvanKilo { get; set; }
        public string ImgUrl { get; set; }
        public DateTime HayvanDogumTarihi { get; set; }
        public DateTime? HayvanOlumTarihi { get; set; }
        public int RenkId { get; set; }
        public int CinsTurId { get; set; }

        public int? HayvanAnneId { get; set; }
        public int? HayvanBabaId { get; set; }

        public virtual Hayvan HayvanAnne { get; set; }
        public virtual Hayvan HayvanBaba { get; set; }
        public virtual Renk Renk { get; set; }

        public virtual CinsTur CinsTur { get; set; }
        public virtual ICollection<Hayvan> AnneninCocuklari { get; set; }
        public virtual ICollection<Hayvan> BabaninCocuklari { get; set; }
        public virtual ICollection<Muayene> Muayeneler { get; set; }
        public virtual ICollection<SahipHayvan> Sahipler { get; set; }
    }
}
