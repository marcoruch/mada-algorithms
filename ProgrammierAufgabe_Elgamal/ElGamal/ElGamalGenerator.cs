using System.Numerics;
using System.Security.Cryptography;

namespace ProgrammierAufgabe_Elgamal.ElGamal;

internal class ElGamalGenerator
{
    private static readonly RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

    /// <summary>
    /// Privaten und öffentlichen Schlüssel generieren aus vordefiniertem n sowie g.
    /// </summary>
    /// <param name="predefinedN">Vordefiniertes N</param>
    /// <param name="predefinedG">Vordefiniertes G</param>
    /// <param name="publicKeyFile">Relativer Pfad zum öffentlichen Schlüssel</param>
    /// <param name="privateKeyFile">Relativer Pfad zum privaten Schlüssel</param>
    public static void GenerateKeys(BigInteger predefinedN, BigInteger predefinedG, string publicKeyFile, string privateKeyFile)
    {
        BigInteger n = predefinedN;
        BigInteger g = predefinedG;

        BigInteger x;
        do
        {
            x = Helpers.BigIntegerHelper.GenerateRandomBigInteger(n.GetBitLength(), randomNumberGenerator);
        } while (x <= 0 || x >= n - 1);

        BigInteger y = BigInteger.ModPow(g, x, n);

        File.WriteAllText(publicKeyFile, y.ToString());
        File.WriteAllText(privateKeyFile, x.ToString());
    }
}
