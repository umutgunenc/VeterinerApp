using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerApp.Models.Entity
{
    public partial class MaasOdemeleri
    {
        public string CalisanTckn { get; set; }
        public DateTime OdemeTarihi { get; set; }
        public double OdenenTutar { get; set; }

        public virtual Insan CalisanTcknNavigation { get; set; }
    }
}
