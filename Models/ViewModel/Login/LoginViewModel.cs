using System;

namespace VeterinerApp.Models.ViewModel.Login
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Email { get; set; }
        public string sifre { get; set; }
        public DateTime SifreOlusturmaTarihi { get; set; }
        public DateTime SifreGecerlilikTarihi { get; set; }
    }
}
