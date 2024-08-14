using System.Text;
using System;
using System.Security.Cryptography;

namespace VeterinerApp.Fonksiyonlar
{
    public static class Signature
    {
        public static string CreateSignature(int Integer, string String)
        {
            string data = $"{Integer}-{String}";

            var sha256 = SHA256.Create();
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool VerifySignature(int Integer, string String, string signature)
        {
            var expectedSignature = CreateSignature(Integer, String);
            return signature == expectedSignature;
        }
    }
}
