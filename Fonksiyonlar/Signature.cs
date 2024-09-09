using System.Text;
using System;
using System.Security.Cryptography;

namespace VeterinerApp.Fonksiyonlar
{
    public static class Signature
    {
        public static string CreateSignature(int Integer, int Id)
        {
            string data = $"{Integer}-{Id}";

            var sha256 = SHA256.Create();
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool VerifySignature(int Integer, int Id, string signature)
        {
            var expectedSignature = CreateSignature(Integer, Id);
            return signature == expectedSignature;
        }
    }
}
