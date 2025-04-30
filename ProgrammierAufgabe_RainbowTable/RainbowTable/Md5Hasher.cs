using System.Security.Cryptography;
using System.Text;

namespace ProgrammierAufgabe_RainbowTable.RainbowTable;

public static class Md5Hasher
{
    /// <summary>
    /// Berechnet den MD5-Hash eines gegebenen Passworts.
    /// </summary>
    /// <param name="passwort">Das zu hashende Passwort.</param>
    /// <returns>Der MD5-Hash als Hex-String.</returns>
    public static string BerechneMD5Hash(string passwort)
    {
        byte[] inputBytes = Encoding.ASCII.GetBytes(passwort);
        byte[] hashBytes = MD5.HashData(inputBytes);

        return string.Join(string.Empty, hashBytes.Select(b => b.ToString("x2")));
    }

    /// <summary>
    /// Testet die Passwort-Hash Paare unter Verwendung der MD5Hash-Funktion: <see cref="BerechneMD5Hash"/>
    /// </summary>
    /// <param name="passwortHashPairs">Die zu testenden Passwort-Hash Paare.</param>
    public static void TesteHashing(Dictionary<string, string> passwortHashPairs)
    {
        foreach (var pair in passwortHashPairs)
        {
            var testResultat = BerechneMD5Hash(pair.Key) == pair.Value;
            Console.WriteLine($"Prüfe {pair.Key} auf {pair.Value}: {testResultat}");
        }

        Console.WriteLine();
    }
}
