using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class BigIntegerHelper
    {
        /// <summary>
        /// Methode zur Generierung einer zufälligen BigInteger-Zahl im angegebenen Bereich
        /// </summary>
        /// <param name="min">Minimum</param>
        /// <param name="max">Maximum</param>
        /// <returns>Neuer Big Integer zwischen den Boundaries</returns>
        public static BigInteger GenerateRandomBigInteger(BigInteger min, BigInteger max, RandomNumberGenerator randomNumberGenerator)
        {
            byte[] bytes = max.ToByteArray();

            // Zufällige Folge von Bytes wird hier generiert in das ByteArray
            randomNumberGenerator.GetBytes(bytes);
            BigInteger value = new BigInteger(bytes);

            // Min-Max Prüfung.
            value = BigInteger.Abs(value) % (max - min) + min;
            return value;
        }

        public static BigInteger GenerateRandomBigInteger(long bitLength, RandomNumberGenerator? randomNumberGenerator)
        {
            byte[] bytes = new byte[bitLength / 8];

            if (randomNumberGenerator != null)
            {
                randomNumberGenerator.GetBytes(bytes);
            }
            else
            {
                using RandomNumberGenerator rng = RandomNumberGenerator.Create();
                rng.GetBytes(bytes);
            }

            bytes[bytes.Length - 1] &= 0x7F;

            return new BigInteger(bytes);
        }
    }
}
