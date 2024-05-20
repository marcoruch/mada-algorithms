using System.Numerics;
using System.Text;

namespace ProgrammierAufgabe_Elgamal.ElGamal
{
    internal class ElGamalEncryptor
    {
        /// <summary>
        /// Datei verschlüsseln anhand von vordefiniertem n und g sowie pk/input Datei.
        /// </summary>
        /// <param name="predefinedN">Vordefiniertes N</param>
        /// <param name="predefinedG">Vordefiniertes G</param>
        /// <param name="inputFilePath">Relativer Pfad zu Textfile mit zu verschlüsselnden Text</param>
        /// <param name="publicKeyFilePath">Relativer Pfad zum öffentlichen Schlüssel</param>
        /// <param name="outputFilePath">Relativer Pfad wohin das neue verschlüsselte Textfile kommen soll</param>
        public static void EncryptFile(BigInteger predefinedN, BigInteger predefinedG, string inputFilePath, string publicKeyFilePath, string outputFilePath)
        {
            BigInteger n = predefinedN;
            BigInteger g = predefinedG;

            string publicKeyString = File.ReadAllText(publicKeyFilePath);
            BigInteger y = BigInteger.Parse(publicKeyString);

            string text = File.ReadAllText(inputFilePath);
            Random random = new();
            StringBuilder encryptedText = new();

            foreach (char c in text)
            {
                BigInteger m = new(c);
                BigInteger k;
                do
                {
                    byte[] bytes = new byte[256]; // 2048 bits
                    random.NextBytes(bytes);
                    k = new BigInteger(bytes);
                } while (k <= 0 || k >= n - 1);

                BigInteger y1 = BigInteger.ModPow(g, k, n);
                BigInteger y2 = (m * BigInteger.ModPow(y, k, n)) % n;

                encryptedText.Append($"({y1},{y2});");
            }

            File.WriteAllText(outputFilePath, encryptedText.ToString());
        }
    }
}
