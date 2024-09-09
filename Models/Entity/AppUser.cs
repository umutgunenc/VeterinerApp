using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class AppUser : IdentityUser<int>
    {

        public string InsanTckn { get; set; }
        public string InsanAdi { get; set; }
        public string InsanSoyadi { get; set; }
        public string ImgURL { get; set; }
        public DateTime SifreOlusturmaTarihi { get; set; }
        public DateTime SifreGecerlilikTarihi { get; set; }
        public string DiplomaNo { get; set; }
        public bool CalisiyorMu { get; set; }
        public double? Maas { get; set; }
        public bool TermOfUse { get; set; }
        public virtual ICollection<MaasOdemeleri> MaasOdemeleri { get; set; }
        public virtual ICollection<Muayene> Muayeneler { get; set; }
        public virtual ICollection<SahipHayvan> SahipHayvanlar { get; set; }
    }
}
