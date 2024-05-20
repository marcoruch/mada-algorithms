using ProgrammierAufgabe_Elgamal.ElGamal;

using System.Numerics;

namespace ProgrammierAufgabe_ElGamal;

internal class Program
{
    /// <summary>
    /// Bereitgestelltes "n" aus Aufgabenstellung
    /// </summary>
    private static readonly BigInteger PredefinedN = BigInteger.Parse("00" + "FFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD1" +
        "29024E088A67CC74020BBEA63B139B22514A08798E3404DD" +
        "EF9519B3CD3A431B302B0A6DF25F14374FE1356D6D51C245" +
        "E485B576625E7EC6F44C42E9A637ED6B0BFF5CB6F406B7ED" +
        "EE386BFB5A899FA5AE9F24117C4B1FE649286651ECE45B3D" +
        "C2007CB8A163BF0598DA48361C55D39A69163FA8FD24CF5F" +
        "83655D23DCA3AD961C62F356208552BB9ED529077096966D" +
        "670C354E4ABC9804F1746C08CA18217C32905E462E36CE3B" +
        "E39E772C180E86039B2783A2EC07A28FB5C55DF06F4C52C9" +
        "DE2BCBF6955817183995497CEA956AE515D2261898FA0510" +
        "15728E5A8AACAA68FFFFFFFFFFFFFFFF",
        System.Globalization.NumberStyles.HexNumber);

    /// <summary>
    /// Bereitgestelltes "g" aus Aufgabenstellung
    /// </summary>
    private static readonly BigInteger PredefinedG = 2;


    /// <summary>
    /// In diesem Tool ermöglichen wir es, eine reguläre Text-Datei mithilfe von ElGamal-Verschlüsselung zu chiffrieren und eine chiffrierte Datei zu dechiffrieren.
    /// </summary>
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1 - Generate ElGamal keys and save to pk.txt and sk.txt");
            Console.WriteLine("2 - Encrypt text.txt using pk.txt and save to chiffre.txt");
            Console.WriteLine("3 - Decrypt chiffre.txt using sk.txt and save to text-d.txt");
            Console.WriteLine("4 - Exit");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ElGamalGenerator.GenerateKeys(PredefinedN, PredefinedG, "pk.txt", "sk.txt");
                    Console.WriteLine("ElGamal keys generated successfully.");
                    break;
                case "2":
                    ElGamalEncryptor.EncryptFile(PredefinedN, PredefinedG, "text.txt", "pk.txt", "chiffre.txt");
                    Console.WriteLine("File encrypted successfully.");
                    break;
                case "3":
                    ElGamalDecryptor.DecryptFile(PredefinedN, "chiffre.txt", "sk.txt", "text-d.txt");
                    Console.WriteLine("File decrypted successfully.");
                    break;
                case "4":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                    break;
            }
        }
    }

}