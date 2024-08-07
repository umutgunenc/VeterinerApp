using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Animal
{
    public class HayvanlarViewModel
    {
        public string imgURL { get; set; }
        public int HayvanId { get; set; }
        public string HayvanAdi { get; set; }
        public string HayvanCinsiyet { get; set; }
        public DateTime HayvanDogumTarihi { get; set; }
        public DateTime? HayvanOlumTarihi { get; set; }
        public double HayvanKilo { get; set; }
        public string TurAdi { get; set; }
        public string CinsAdi { get; set; }
        public string HayvanAnneAdi { get; set; }
        public string HayvanBabaAdi { get; set; }
        public string RenkAdi { get; set; }
        public List<MuayeneViewModel> Muayeneler { get; set; }
        public DateTime SahiplikTarihi { get; set; }
        public DateTime? SahiplikCikisTarihi { get; set; }


    }
}
