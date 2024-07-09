using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Admin
{
    public class InsanDuzenleViewModel
    {
        public string InsanTckn { get; set; }
        public string InsanAdi { get; set; }
        public string InsanSoyadi { get; set; }
        public string InsanTel { get; set; }
        public string InsanMail { get; set; }
        public int RolId { get; set; }
        public string RolAdi { get; set; }
        public string DiplomaNo { get; set; }
        public bool CalisiyorMu { get; set; }
        public double? Maas { get; set; }
        public string KullaniciAdi { get; set; }
        public List<SelectListItem> Roller { get; set; }
        public List<SelectListItem> Insanlar { get; set; }

    }
}
