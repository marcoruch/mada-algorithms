using Helpers;

using System.Numerics;

namespace ProgrammierAufgabe_RSA.RSA;

internal class RSAGenerator
{
    /// <summary>
    /// Generiert ein RSA Key-Pair und speichert Sie unter pk.txt und sk.txt (Public Key respektive Secret Key aka. Private Key)
    /// </summary>
    public static void GenerateRSA()
    {
        (BigInteger FirstPrime, BigInteger SecondPrime) primesResult = PrimesHelper.GetSetOfDistinctBigPrimes();
        BigInteger p = primesResult.FirstPrime;
        BigInteger q = primesResult.SecondPrime;
        Console.WriteLine($"Generated RSA primes: {p}, {q}");


        // Berechnen von n und phi(n)
        // Phi (φ) von n definiert als Produkt (p - 1) * (q - 1), wenn n das Produkt der beiden Primzahlen p und q ist
        BigInteger n = BigInteger.Multiply(p, q);
        BigInteger pMinus1 = BigInteger.Subtract(p, 1);
        BigInteger qMinus1 = BigInteger.Subtract(q, 1);
        BigInteger phiN = BigInteger.Multiply(pMinus1, qMinus1);
        Console.WriteLine($"n: {n}");

        // Wählen von e und Berechnen von d
        var e = GetE();
        var d = CalculateD(e, phiN);
        Console.WriteLine($"e: {e}, d: {d}");

        // Bereitstellen von public-key und private-key
        Console.WriteLine($"Public key (n, e): ({n}, {e})");
        Console.WriteLine($"Private key (n, d): ({n}, {d})");

        FilesHelper.SaveKeyToFile("pk.txt", n, e);
        FilesHelper.SaveKeyToFile("sk.txt", n, d);
    }

    /// <summary>
    /// Gibt unser statisch gewähltes e zurück, e köntne auch etwas anderes sein aber wir nehmen das Standard e.
    /// </summary>
    /// <returns>Oft als Standard genutztes e mit Wert "65537"</returns>
    public static BigInteger GetE()
    {
        return 65537;
    }

    /// <summary>
    /// Berechnen von d via dem erweiterten eujklidischen algorithmus
    /// </summary>
    /// <param name="e">Öffentlicher Exponent e</param>
    /// <param name="phiN">Phi (φ) von n</param>
    /// <returns>Privater Exponent d</returns>
    public static BigInteger CalculateD(BigInteger e, BigInteger phiN)
    {
        BigInteger d = AlgorithmsHelper.ExtendedEuclideanAlgorithm(e, phiN);
        d = (d % phiN + phiN) % phiN;
        return d;
    }
}
