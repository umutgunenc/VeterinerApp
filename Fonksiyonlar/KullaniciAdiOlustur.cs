using System.Collections.Generic;
using System;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace VeterinerApp.Fonksiyonlar
{
    public static class KullaniciAdiOlustur
    {
       
        public static async Task<string> GenerateUserNameAsync(string firstName, string lastName, string mail, VeterinerDBContext _context)
        {
            string username = $"{firstName.ToUpper()}.{lastName.ToUpper()}";
            var kullaniciAdlari = await _context.Users.Select(x => x.UserName).ToListAsync();

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
