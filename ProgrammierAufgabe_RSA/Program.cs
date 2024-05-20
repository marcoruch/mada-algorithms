using ProgrammierAufgabe_RSA.RSA;

namespace ProgrammierAufgabe_RSA;

internal class Program
{
    /// <summary>
    /// Der Benutzer kann zwischen den 3 Funktionen wählen: Generierung eines Schlüsselpaars, Verschlüsselung von Text und Entschlüsselung von der Chiffre.
    /// Die Steuerung der Anwendung erfolgt über Konsoleneingaben anstelle von Argumenten, damit der Benutzer die Anwendung nicht neu starten muss.
    /// Kommentare sind für die Einfachheit für den Leser in Deutsch, damit der Code leichter verstanden werden kann.
    /// Die Applikation ist in Englisch bzw. der Running-Code auch, einfach weil es Standard ist und ich keine Deutsche Variablen schreiben kann :)!
    /// </summary>
    static void Main()
    {
        string generateKeysOption = "1 - Generate RSA keys, sk.txt pk.txt";
        string encryptOption = "2 - Encrypt Input File text.txt using public key from pk.txt";
        string decryptOption = "3 - Decrypt Input File chiffre.txt using private key from sk.txt";
        string invalidInputMessage = "Invalid input. Please enter a number between 1 and 3.";

        while (true)
        {
            Console.WriteLine("Choose an operation by using the provided numbers and pressing <ENTER>:");
            Console.WriteLine(generateKeysOption);
            Console.WriteLine(encryptOption);
            Console.WriteLine(decryptOption);

            string? input = Console.ReadLine();
            Console.Clear();
            switch (input)
            {
                case "1":
                    Console.WriteLine("Chose: " + generateKeysOption);
                    RSAGenerator.GenerateRSA();
                    Console.WriteLine("=========== DONE ===========");
                    break;
                case "2":
                    Console.WriteLine("Chose: " + encryptOption);
                    RSAEncryptor.Encrypt();
                    Console.WriteLine("=========== DONE ===========");
                    break;
                case "3":
                    Console.WriteLine("Chose: " + decryptOption);
                    RSADecryptor.Decrypt();
                    Console.WriteLine("=========== DONE ===========");
                    break;
                default:
                    Console.WriteLine(invalidInputMessage);
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
