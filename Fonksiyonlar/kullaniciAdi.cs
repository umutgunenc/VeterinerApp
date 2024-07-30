using System.Collections.Generic;
using System;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using System.Linq;

namespace VeterinerApp.Fonksiyonlar
{
    public class kullaniciAdi
    {
        private readonly VeterinerContext _context;

        public kullaniciAdi(VeterinerContext context)
        {
            _context = context;
        }

        private List<string> KullaniciAdlariListesi()
        {
            return _context.Users.Select(x => x.UserName).ToList();
        }

        public string GenerateUsername(string firstName, string lastName, string mail)
        {
            string username = $"{firstName.ToUpper()}.{lastName.ToUpper()}";
            var kullaniciAdlari = KullaniciAdlariListesi();

            // Kullanıcı adı zaten var mı kontrol et
            if (!kullaniciAdlari.Contains(username))
            {
                return username;
            }
            else
            {
                username = mail;
                if (kullaniciAdlari.Contains(mail))
                {
                    int suffix = 1;
                    string newUsername;
                    do
                    {
                        newUsername = $"{firstName.ToUpper()}.{lastName.ToUpper()}{suffix}";
                        suffix++;
                    }
                    while (kullaniciAdlari.Contains(newUsername));

                    return newUsername;
                }
                return username;
            }
        }
    }
}
