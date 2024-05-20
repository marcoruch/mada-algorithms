using Helpers;

using System.Numerics;
using System.Text;

namespace ProgrammierAufgabe_RSA.RSA;

internal class RSAEncryptor
{
    public static void Encrypt()
    {
        Console.WriteLine("Reading public key from pk.txt...");
        FilesHelper.ReadKeyFromFile("pk.txt", out BigInteger n, out BigInteger e);

        Console.WriteLine("Reading text from text.txt...");
        string inputText = File.ReadAllText("text.txt");

        Console.WriteLine("Starting encryption...");
        StringBuilder encryptedText = new();
        foreach (char asciiCode in inputText)
        {
            BigInteger encryptedAscii = AlgorithmsHelper.FastModularExponentiation(asciiCode, e, n);
            encryptedText.Append(encryptedAscii);
            encryptedText.Append(',');
        }

        encryptedText.Length--;

        Console.WriteLine("Writing to output file chiffre.txt...");
        File.WriteAllText("chiffre.txt", encryptedText.ToString());

        Console.WriteLine("Encryption complete. Encrypted text saved to chiffre.txt");
    }
}
