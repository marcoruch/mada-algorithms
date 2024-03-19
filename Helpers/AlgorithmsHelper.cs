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
    }
}