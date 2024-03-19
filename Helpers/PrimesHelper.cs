using System.Numerics;
using System.Security.Cryptography;

namespace ProgrammierAufgabe_RSA.Helpers;

internal class PrimesHelper
{
    private static readonly RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

    /// <summary>
    /// Methode, um ein Paar von distinkten großen Primzahlen zu erhalten
    /// </summary>
    /// <returns>Ein Set aus 2 distinkten 2048bit Primzahlen</returns>
    public static (BigInteger FirstPrime, BigInteger SecondPrime) GetSetOfDistinctBigPrimes()
    {
        Console.WriteLine($"Generiere Primzahlen und erhalte 2 zufällige, eindeutige Primzahlen");
        int bitLength = 2048;
        var firstPrime = GeneratePrime(bitLength);
        var secondPrime = GeneratePrime(bitLength);

        // Stelle sicher, dass die beiden Primzahlen verschieden sind
        while (firstPrime == secondPrime)
        {
            secondPrime = GeneratePrime(bitLength);
        }

        return (firstPrime, secondPrime);
    }

    /// <summary>
    /// Methode zur Generierung einer großen Primzahl
    /// Notiz: Mehrheitlich aus Java's BigInteger.probablyPrime Ansatz mit Input bitLength & dem probablePrime Test Verfahren.
    /// </summary>
    /// <param name="bitLength">Anzahl bits für Primzahl</param>
    /// <returns>Primzahl mit gewünschter Bit Länge</returns>
    private static BigInteger GeneratePrime(int bitLength)
    {
        while (true)
        {
            byte[] bytes = new byte[(bitLength + 7) / 8]; // Konvertiere Bitlänge in Bytelänge
            randomNumberGenerator.GetBytes(bytes);

            // Stelle sicher, dass das höchstwertigste Bit und das niederstwertigste Bit auf 1 gesetzt sind
            bytes[0] |= 0x01; // Setze das niederstwertigste Bit auf 1
            bytes[bytes.Length - 1] |= 0x80; // Setze das höchstwertigste Bit auf 1

            BigInteger candidate = new BigInteger(bytes);
            candidate = BigInteger.Abs(candidate);

            // Stelle sicher, dass die Kandidatenzahl ungerade ist (um die Wahrscheinlichkeit von Primzahlen zu erhöhen)
            if (candidate.IsEven)
            {
                candidate++;
            }

            // Führe die Primalitätstests durch
            if (IsPrime(candidate))
            {
                return candidate;
            }
        }
    }

    /// <summary>
    /// Methode zur Überprüfung, ob eine Zahl prim ist
    /// </summary>
    /// <param name="n">Zu überprüfender Big Integer</param>
    /// <returns></returns>
    private static bool IsPrime(BigInteger n)
    {
        if (n <= 1)
        {
            return false;
        }

        if (n == 2 || n == 3)
        {
            return true;
        }

        if (n.IsEven)
        {
            return false;
        }

        BigInteger d = n - 1;
        int s = 0;
        while (d.IsEven)
        {
            d >>= 1;
            s++;
        }

        // Anzahl der Iterationen für den Primalitätstest
        // Kann man anpassen je nach Sicherheitsanforderungen
        int k = 20;

        for (int i = 0; i < k; i++)
        {
            BigInteger a = GenerateRandomBigInteger(2, n - 2);
            BigInteger x = BigInteger.ModPow(a, d, n);
            if (x == 1 || x == n - 1)
            {
                continue;
            }

            bool probablePrime = false;
            for (int j = 1; j < s; j++)
            {
                x = BigInteger.ModPow(x, 2, n);
                if (x == n - 1)
                {
                    probablePrime = true;
                    break;
                }
            }

            if (!probablePrime)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Methode zur Generierung einer zufälligen BigInteger-Zahl im angegebenen Bereich
    /// </summary>
    /// <param name="min">Minimum</param>
    /// <param name="max">Maximum</param>
    /// <returns>Neuer Big Integer zwischen den Boundaries</returns>
    private static BigInteger GenerateRandomBigInteger(BigInteger min, BigInteger max)
    {
        byte[] bytes = max.ToByteArray();
        
        // Zufällige Folge von Bytes wird hier generiert in das ByteArray
        randomNumberGenerator.GetBytes(bytes);
        BigInteger value = new BigInteger(bytes);
        
        // Min-Max Prüfung.
        value = BigInteger.Abs(value) % (max - min) + min;
        return value;
    }
}
