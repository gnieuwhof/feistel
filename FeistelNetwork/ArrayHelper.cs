namespace Feistel
{
    using System;

    internal static class ArrayHelper
    {
        public static byte[] Xor(byte[] a, byte[] b)
        {
            if (a == null)
                throw new ArgumentNullException(nameof(a));

            if (b == null)
                throw new ArgumentNullException(nameof(b));

            if (a.Length != b.Length)
                throw new ArgumentException("Arguments must have the same length.");

            byte[] result = new byte[a.Length];

            for (int i = 0; i < a.Length; ++i)
            {
                result[i] = (byte)(a[i] ^ b[i]);
            }

            return result;
        }

        public static byte[] Flip(byte[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int half = array.Length / 2;

            byte[] result = Flip(array, half);

            return result;
        }

        public static byte[] Flip(byte[] array, int splitIndex)
        {
            if(array == null)
                throw new ArgumentNullException(nameof(array));

            if((splitIndex < 0) || (splitIndex >= array.Length))
                throw new ArgumentOutOfRangeException(nameof(splitIndex));

            byte[] result = new byte[array.Length];

            Array.Copy(array, splitIndex, result, 0, splitIndex);
            Array.Copy(array, 0, result, splitIndex, splitIndex);

            return result;
        }

        public static byte[] Add(byte[] first, byte[] second)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));

            if (second == null)
                throw new ArgumentNullException(nameof(second));

            var result = new byte[first.Length + second.Length];

            first.CopyTo(result, 0);
            second.CopyTo(result, first.Length);

            return result;
        }
    }
}
