using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Sifre
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string sifre { get; set; }
        public DateTime SifreOlusturmaTarihi { get; set; }
        public DateTime SifreGecerlilikTarihi { get; set; }

        public virtual Insan KullaniciAdiNavigation { get; set; }
    }
}
