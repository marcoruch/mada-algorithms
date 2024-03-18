using ProgrammierAufgabe_RSA.Helpers;

using System.Numerics;
using System.Text;

namespace ProgrammierAufgabe_RSA.RSA;

internal class RSADecryptor
{
    public static void Decrypt()
    {
        Console.WriteLine("Reading secret key from sk.txt File...");
        FilesHelper.ReadKeyFromFile("sk.txt", out BigInteger n, out BigInteger d);

        Console.WriteLine("Reading encrypted text from chiffre.txt File...");
        string[] encryptedAsciiCodes = File.ReadAllText("chiffre.txt").Split(',');

        Console.WriteLine("Decrypting char for char...");
        StringBuilder decryptedTextBuilder = new StringBuilder();
        foreach (string encryptedAsciiCode in encryptedAsciiCodes)
        {
            BigInteger encryptedAscii = BigInteger.Parse(encryptedAsciiCode);

            // decrypt to char
            BigInteger decryptedAscii = BigInteger.ModPow(encryptedAscii, d, n);

            decryptedTextBuilder.Append((char)decryptedAscii);
        }

        string decryptedText = decryptedTextBuilder.ToString();
        Console.WriteLine($"Decrypted {decryptedText.Length} characters...");

        File.WriteAllText("text-d.txt", decryptedText.ToString());

        Console.WriteLine("Decryption complete. Decrypted text saved to text-d.txt");
    }
}
