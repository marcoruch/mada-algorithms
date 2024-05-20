using ProgrammierAufgabe_Huffman.Huffman;

namespace ProgrammierAufgabe_Huffman;

internal class Program
{
    /// <summary>
    /// In diesem Tool ermöglichen wir es eine reguläre Text-Datei mithilfe von lossless compression à la Huffman zu einer .dat zu kompressieren.
    /// Auch möglich wiederum ist es diese .dat lossless wiederherzustellen. Viel Spass.
    /// </summary>
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1 - Generate Huffman coding from text.txt and save to dec_tab.txt and output.dat");
            Console.WriteLine("2 - Decompress output.dat using dec_tab.txt and save to decompress.txt");
            Console.WriteLine("3 - Exit");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    HuffmanCompressor.Compress("text.txt", "dec_tab.txt", "output.dat");
                    Console.WriteLine("Huffman coding generated successfully.");
                    break;
                case "2":
                    HuffmanDecompressor.Decompress("output.dat", "dec_tab.txt", "decompress.txt");
                    Console.WriteLine("File decompressed successfully.");
                    break;
                case "3":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
                    break;
            }
        }
    }
}
