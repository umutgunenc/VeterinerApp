using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class CalisanListeleViewModel
    {
        public string InsanTckn { get; set; }
        public string InsanAdi { get; set; }
        public string InsanSoyadi { get; set; }
        public string InsanTel { get; set; }
        public string InsanMail { get; set; }
        public int RolId { get; set; }
        public string DiplomaNo { get; set; }
        public bool? CalisiyorMu { get; set; }
        public double? Maas { get; set; }
        public string KullaniciAdi { get; set; }
        public string RolAdi { get; set; }

    }
}
