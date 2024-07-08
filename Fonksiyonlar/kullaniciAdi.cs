﻿using System.Collections.Generic;
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
            return _context.Insans.Select(x => x.KullaniciAdi).ToList();
        }

        public string GenerateUsername(string firstName, string lastName, string mail)
        {
            string username = $"{firstName.ToUpper()}.{lastName.ToUpper()}";
            var kullaniciAdlari = KullaniciAdlariListesi();

            if (!kullaniciAdlari.Contains(username))
            {
                return username;
            }
            else
            {
                username = mail;              
                return username;
            }
        }
    }
}