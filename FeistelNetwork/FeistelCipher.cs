namespace Feistel
{
    using System;

    public static class FeistelCipher
    {
        public static byte[] Process<T>(
            Func<byte[], T, byte[]> f, T[] keys, byte[] input, int iterations)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            
            if (iterations < 0)
                throw new ArgumentException("The number of iterations cannot be negative.");

            if ((keys != null) && (keys.Length != iterations))
            {
                throw new ArgumentException(
                    "Keys must be null or the number of keys must be equal to iterations.");
            }

            byte[] result = input;

            if (input.Length % 2 == 1)
            {
                result = new byte[input.Length + 1];

                input.CopyTo(result, 0);
            }
            
            for(int i = 0; i < iterations; ++i)
            {
                T key = default(T);

                if(keys != null)
                {
                    int index = i % keys.Length;

                    key = keys[index];
                }

                result = InternalProcess(f, result, key);
            }

            var flipped = ArrayHelper.Flip(result);

            return flipped;
        }

        private static byte[] InternalProcess<T>(
            Func<byte[], T, byte[]> f, byte[] input, T key)
        {
            int count = input.Length;
            int half = count / 2;

            byte[] left = new byte[half];
            byte[] right = new byte[count - left.Length];
            
            Array.Copy(input, left, left.Length);
            Array.Copy(input, left.Length, right, 0, input.Length - left.Length);
            
            byte[] hash = Round(f, right, key, right.Length);
            
            byte[] xor = ArrayHelper.Xor(left, hash);

            byte[] result = ArrayHelper.Add(right, xor);
            
            return result;
        }

        private static byte[] Round<T>(
            Func<byte[], T, byte[]> f, byte[] input, T key, int byteCount)
        {
            byte[] hash = f(input, key);
            byte[] result = new byte[byteCount];

            int toCopy = byteCount;
            
            while (toCopy  > 0)
            {
                int length = Math.Min(hash.Length, toCopy);

                Array.Copy(hash, 0, result, byteCount - toCopy, length);

                toCopy -= length;
            }

            return result;
        }
    }
}
