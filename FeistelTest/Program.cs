namespace FeistelTest
{
    using Feistel;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    class Program
    {
        static byte[][] keys = new[]
        {
            new byte[] { 127, 108, 216, 208, 31, 75, 7, 20, 116, 95, 156, 105, 247, 120, 225, 151 },
            new byte[] { 125, 11, 37, 237, 110, 164, 105, 94, 128, 96, 79, 220, 42, 12, 184, 77 },
            new byte[] { 249, 230, 29, 247, 20, 111, 68, 32, 156, 15, 96, 70, 186, 10, 177, 7 }
        };

        static void Main(string[] args)
        {
            string message = "The brown fox is jumping over the lazy dog.";

            byte[] input = Encoding.ASCII.GetBytes(message);

            //byte[] encrypted = FeistelCipher.Process(Xor, keys, input, 3);

            //byte[] decrypted = FeistelCipher.Process(
            //    Xor, keys.Reverse().ToArray(), encrypted, 3);

            byte[] encrypted = FeistelCipher.Process<object>(MD5, null, input, 3);

            byte[] decrypted = FeistelCipher.Process<object>(MD5, null, encrypted, 3);

            byte[] result = new byte[input.Length];

            Array.Copy(decrypted, result, result.Length);

            string foo = Encoding.ASCII.GetString(result);

            Console.WriteLine(foo);

            Console.ReadKey();
        }

        static byte[] Xor(byte[] input, byte[] key)
        {
            byte[] temp = new byte[key.Length];

            Array.Copy(input, temp, key.Length);

            byte[] result = new byte[temp.Length];

            for (int i = 0; i < temp.Length; ++i)
            {
                result[i] = (byte)(temp[i] ^ key[i]);
            }

            return result;
        }

        static byte[] MD5(byte[] input, object _)
        {
            var md5 = new MD5CryptoServiceProvider();

            byte[] result = md5.ComputeHash(input);

            return result;
        }
    }
}
