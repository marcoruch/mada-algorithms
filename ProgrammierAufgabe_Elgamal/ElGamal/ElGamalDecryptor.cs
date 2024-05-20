using System.Numerics;
using System.Text;

namespace ProgrammierAufgabe_Elgamal.ElGamal;

internal class ElGamalDecryptor
{
    /// <summary>
    /// Aus dem verschlüsselten File können wir anhand des predefinedN "n" und dem PK den original-Text / Nachricht des Benutzers wiederherstellen
    /// </summary>
    /// <param name="predefinedN">Vordefiniertes n aus Aufgabe</param>
    /// <param name="inputFilePath">Relativer Filepath zu verschlüsseltem Text</param>
    /// <param name="privateKeyFilePath">Relativer Filepath zu Privatem Schlüssel</param>
    /// <param name="outputFilePath">Relativer Filepath wohin die entschlüsselte Nachricht hingespeichert werden soll</param>
    public static void DecryptFile(BigInteger predefinedN, string inputFilePath, string privateKeyFilePath, string outputFilePath)
    {
        BigInteger n = predefinedN;

        string privateKeyString = File.ReadAllText(privateKeyFilePath);
        BigInteger x = BigInteger.Parse(privateKeyString);

        string encryptedText = File.ReadAllText(inputFilePath);
        string[] encryptedPairs = encryptedText.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        // Jedes verschlüsselte Paar entschlüsseln
        StringBuilder decryptedText = new();
        foreach (string pair in encryptedPairs)
        {
            string[] values = pair.Trim('(', ')').Split(',');
            BigInteger y1 = BigInteger.Parse(values[0]);
            BigInteger y2 = BigInteger.Parse(values[1]);

            // Berechnung von s und sInverse für die Entschlüsselung
            BigInteger s = BigInteger.ModPow(y1, x, n);
            BigInteger sInverse = BigInteger.ModPow(s, n - 2, n);
            BigInteger m = y2 * sInverse % n;

            // Entschlüsselten Buchstaben hinzufügen
            decryptedText.Append((char)(int)m);
        }

        File.WriteAllText(outputFilePath, decryptedText.ToString());
    }
}
