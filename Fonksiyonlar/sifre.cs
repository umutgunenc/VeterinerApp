using System;
using System.Linq;
using System.Text;

namespace VeterinerApp.Fonksiyonlar
{
    public class sifre
    {
        private static readonly Random random = new Random();

        public string GeneratePassword(int length = 8)
        {
            const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*()_+[]{}|;:,.<>?";

            // Şifrenin her türden en az bir karakter içermesini sağlar
            StringBuilder password = new StringBuilder();
            password.Append(upperCase[random.Next(upperCase.Length)]);
            password.Append(lowerCase[random.Next(lowerCase.Length)]);
            password.Append(digits[random.Next(digits.Length)]);
            password.Append(specialChars[random.Next(specialChars.Length)]);

            // Şifrenin kalan kısmını tüm karakter türlerinden rastgele karakterlerle doldurur
            string allChars = upperCase + lowerCase + digits + specialChars;
            for (int i = password.Length; i < length; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Şifrenin karakterlerini karıştırarak rastgeleliği artır
            return new string(password.ToString().OrderBy(c => random.Next()).ToArray());
        }
    }
}

