using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class Insan
    {
        public Insan()
        {
            MaasOdemeleris = new HashSet<MaasOdemeleri>();
            Muayenes = new HashSet<Muayene>();
            SahipHayvans = new HashSet<SahipHayvan>();
            Sifres = new HashSet<Sifre>();
        }

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

        public virtual Rol Rol { get; set; }
        public virtual ICollection<MaasOdemeleri> MaasOdemeleris { get; set; }
        public virtual ICollection<Muayene> Muayenes { get; set; }
        public virtual ICollection<SahipHayvan> SahipHayvans { get; set; }
        public virtual ICollection<Sifre> Sifres { get; set; }
    }
}
