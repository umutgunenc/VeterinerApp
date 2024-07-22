using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace VeterinerApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Email { get; set; }
        public string sifre { get; set; }
        public bool RememberMe { get; set; }
        public bool CalisiyorMu { get; set; }
        public DateTime SifreOlusturmaTarihi { get; set; }
        public DateTime SifreGecerlilikTarihi { get; set; }
    }
}
