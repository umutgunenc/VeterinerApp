using Microsoft.AspNetCore.Mvc;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Animal
{
    public class MuayeneViewModel : Muayene
    {
        public string HekimAdi { get; set; }
    }
}
