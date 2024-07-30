using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Insan :IdentityUser
    {
        public Insan() 
        {
            MaasOdemeleris = new HashSet<MaasOdemeleri>();
            Muayenes = new HashSet<Muayene>();
            SahipHayvans = new HashSet<SahipHayvan>();
        }

        public string InsanTckn { get; set; }
        public string InsanAdi { get; set; }
        public string InsanSoyadi { get; set; }
        public string ImgURL { get; set; }
        public DateTime SifreOlusturmaTarihi { get; set; }
        public DateTime SifreGecerlilikTarihi { get; set; }

        public string DiplomaNo { get; set; }
        public bool CalisiyorMu { get; set; }
        public double? Maas { get; set; }

        public virtual ICollection<MaasOdemeleri> MaasOdemeleris { get; set; }
        public virtual ICollection<Muayene> Muayenes { get; set; }
        public virtual ICollection<SahipHayvan> SahipHayvans { get; set; }

    }
}
