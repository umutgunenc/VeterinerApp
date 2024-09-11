using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Animal
{
    public class AddNewSahipViewModel : Hayvan
    {
        private readonly VeterinerDBContext _context;

        public AddNewSahipViewModel(VeterinerDBContext context)
        {
            _context = context;
        }

        public string YeniSahipTCKN { get; set; }
        public string UserTCKN { get; set; }
        public string TurAdi { get; set; }
        public string CinsAdi { get; set; }
        public string RenkAdi { get; set; }
        public string Signature { get; set; }
        public AppUser Sahip { get; set; }

        public AddNewSahipViewModel ViewModelOlustur(Hayvan hayvan, string Signature, AppUser User)
        {
            return new AddNewSahipViewModel(_context)
            {
                YeniSahipTCKN = "",
                HayvanId = hayvan.HayvanId,
                HayvanAdi = hayvan.HayvanAdi,
                ImgUrl = hayvan.ImgUrl,
                HayvanDogumTarihi = hayvan.HayvanDogumTarihi,
                HayvanOlumTarihi = hayvan.HayvanOlumTarihi,
                RenkAdi = hayvan.Renk.RenkAdi,
                CinsAdi = hayvan.CinsTur.Cins.CinsAdi,
                TurAdi = hayvan.CinsTur.Tur.TurAdi,
                HayvanCinsiyet = hayvan.HayvanCinsiyet,
                HayvanKilo = hayvan.HayvanKilo,
                UserTCKN = User.InsanTckn,
                Signature = Signature
            };
        }

 




    }
}
