using System.Collections.Generic;
using System;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace VeterinerApp.Fonksiyonlar
{
    public class KullaniciAdiOlustur
    {
        private readonly VeterinerDBContext _context;

        public KullaniciAdiOlustur(VeterinerDBContext context)
        {
            _context = context;
        }

        private async Task<List<string>> KullaniciAdlariListesi()
        {
            return await _context.Users.Select(x => x.UserName).ToListAsync();
        }

        public  async Task<string> GenerateUsernameAsync(string firstName, string lastName, string mail)
        {
            string username = $"{firstName.ToUpper()}.{lastName.ToUpper()}";
            var kullaniciAdlari = await KullaniciAdlariListesi();

            // Kullanıcı adı zaten var mı kontrol et
            if (!kullaniciAdlari.Contains(username))
            {
                return username.ToUpper();
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

                    return newUsername.ToUpper();
                }
                return username.ToUpper();
            }
        }
    }
}
