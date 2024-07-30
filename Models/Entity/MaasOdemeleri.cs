using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class MaasOdemeleri
    {
        [Key]
        public int Id { get; set; }
        public string CalisanTckn { get; set; }
        public DateTime OdemeTarihi { get; set; }
        public double OdenenTutar { get; set; }

        public virtual AppUser CalisanTcknNavigation { get; set; }
    }
}
