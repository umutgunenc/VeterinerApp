using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class MaasOdemeleri
    {
        public int Id { get; set; }
        public int CalisanId { get; set; }
        public DateTime OdemeTarihi { get; set; }
        public double OdenenTutar { get; set; }

        public virtual AppUser Calisan { get; set; }
    }
}
