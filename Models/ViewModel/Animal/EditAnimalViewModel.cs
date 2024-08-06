using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Animal
{
    public class EditAnimalViewModel : Hayvan
    {
        public string rengi { get; set; }
        public string cinsi { get; set; }
        public string turu { get; set; }
        public bool isDeath { get; set; }
        public List<SelectListItem> TurAdlari { get; set; }
        public List<SelectListItem> CinsAdlari { get; set; }
        public List<SelectListItem> RenkAdlari { get; set; }
        public List<SelectListItem> CinsiyetList { get; set; }
        public AppUser Sahip { get; set; }
        public string SahipTckn { get; set; }
        public string SahipAdSoyad { get; set; }
        public List<SelectListItem> HayvanAnneList { get; set; }
        public List<SelectListItem> HayvanBabaList { get; set; }
    }
}
