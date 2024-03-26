using System.Numerics;

namespace ProgrammierAufgabe_RSA.Helpers
{
    internal static class AlgorithmsHelper
    {
        /// <summary>        
        /// Berechnung des inversen Elements von a modulo b.
        /// </summary>
        /// <param name="a">Zu invertierende Zahl</param>
        /// <param name="b">Modulo Zahl ("Modul")</param>
        /// <returns>Inverses Element von a modulo b</returns>
        public static BigInteger ExtendedEuclideanAlgorithm(BigInteger a, BigInteger b)
        {
            BigInteger x0 = 1, x1 = 0, y0 = 0, y1 = 1;

            while (b != 0)
            {
                BigInteger q = a / b;
                BigInteger temp = b;
                b = a % b;
                a = temp;

                temp = x0 - q * x1;
                x0 = x1;
                x1 = temp;

                temp = y0 - q * y1;
                y0 = y1;
                y1 = temp;
            }

            return x0;
        }

        /// <summary>
        /// Schnelle modulo exponentation, für grosse Zahlen implementiert, in C# auch direkt möglich durch BigIntegers ModPow-Methode
        /// </summary>
        /// <param name="baseValue">Basis-Wert grösser 0</param>
        /// <param name="exponent">Exponent des Basis-Werts, muss grösser 0 sein</param>
        /// <param name="modulus">Modulus</param>
        /// <returns>Das Resultat des Algorithmus als BigInteger</returns>
        public static BigInteger FastModularExponentiation(BigInteger baseValue, BigInteger exponent, BigInteger modulus)
        {
            if (modulus == 1) return 0;
            if (exponent == 1) return baseValue % modulus;

            // h
            BigInteger result = 1;

            // Reduce initial value with modulus directly as this can reduce follow up operations significantly (performance) and modulus can always be applied to base value..
            baseValue %= modulus;

            while (exponent > 0)
            {
                // Check the most right bit against the bitmask 1 to see if we should perform this step
                if ((exponent & 1) == 1)
                {
                    // h = h*k mod x
                    result = result * baseValue % modulus;
                }

                // k = k^2 mod x
                baseValue = baseValue * baseValue % modulus;

                // Move to next bit of exponent by shifting the most right bit away
                exponent >>= 1;
            }

            return result;
        }
    }
}