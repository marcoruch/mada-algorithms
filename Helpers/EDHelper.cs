using System.Numerics;

namespace ProgrammierAufgabe_RSA.Helpers;

/// <summary>
/// Eine Hilfsklasse, beinhaltet Kalkulation für D und das statisch gewählte E (=65537), welche beide für RSA benutzt werden.
/// </summary>
internal class EDHelper
{
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
        BigInteger d = ExtendedEuclideanAlgorithm(e, phiN);
        d = (d % phiN + phiN) % phiN;
        return d;
    }

    /// <summary>        
    /// Berechnung des inversen Elements von a modulo b.
    /// </summary>
    /// <param name="a">Zu invertierende Zahl</param>
    /// <param name="b">Modulo Zahl ("Modul")</param>
    /// <returns>Inverses Element von a modulo b</returns>
    private static BigInteger ExtendedEuclideanAlgorithm(BigInteger a, BigInteger b)
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
