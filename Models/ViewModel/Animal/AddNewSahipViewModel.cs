using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Animal
{
    public class AddNewSahipViewModel:Hayvan
    {
        public string yeniSahipTCKN { get; set; }

        public string turAdi { get; set; }
        public string cinsAdi { get; set; }

        public string renkAdi { get; set; }

    }
}
