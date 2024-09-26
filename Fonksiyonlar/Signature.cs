using System.Text;
using System;
using System.Security.Cryptography;

namespace VeterinerApp.Fonksiyonlar
{
    public static class Signature
    {
        public static string CreateSignature(string String1, string String2)
        {
            string data = $"{String1}-{String2}";

            var sha256 = SHA256.Create();
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool VerifySignature(string String1, string String2, string signature)
        {
            var expectedSignature = CreateSignature(String1, String2);
            return signature == expectedSignature;
        }
    }
}
