using System;

namespace VeterinerApp.Fonksiyonlar
{
    public static class Convertor
    {
        
        public static byte[] ConvertDoubleArrayToByteArray(double[] doubleArray)
        {
            byte[] byteArray = new byte[doubleArray.Length * sizeof(double)];
            Buffer.BlockCopy(doubleArray, 0, byteArray, 0, byteArray.Length);
            return byteArray;
        }

        public static double[] ConvertByteArrayToDoubleArray(byte[] byteArray)
        {
            double[] doubleArray = new double[byteArray.Length / sizeof(double)];
            Buffer.BlockCopy(byteArray, 0, doubleArray, 0, byteArray.Length);
            return doubleArray;
        }
    }
}
