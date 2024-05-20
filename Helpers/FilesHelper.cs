using System.Numerics;

namespace Helpers;

public class FilesHelper
{
    /// <summary>
    /// Liest das n-key pair aus der Datei im Format (n, key)
    /// Wobei key = d bzw. e
    /// <param name="filename">Dateiname</param>
    /// <param name="n">Das ausgelesene n</param>
    /// <param name="key">Das ausgelesene d bzw. e</param>
    public static void ReadKeyFromFile(string filename, out BigInteger n, out BigInteger key)
    {
        using StreamReader reader = new(filename);
        string? line = reader.ReadLine() ?? throw new InvalidOperationException("Cannot perform reading from file as key-file is empty.");
        string[] parts = line.Split(',', '(', ')');
        n = BigInteger.Parse(parts[1].Trim());
        key = BigInteger.Parse(parts[2].Trim());
    }

    /// <summary>
    /// Speichert das n-key pair im Format (n, key) in die gewünschte Datei
    /// Wobei key = d bzw. e
    /// </summary>
    /// <param name="filename">Dateiname</param>
    /// <param name="n">Das zu speichernde n</param>
    /// <param name="key">Das zu speichernde key Element</param>
    public static void SaveKeyToFile(string filename, BigInteger n, BigInteger key)
    {
        using (StreamWriter writer = new(filename))
        {
            writer.WriteLine($"({n}, {key})");
        }
        Console.WriteLine($"Key saved to {filename}");
    }

    /// <summary>
    /// ASCII-Code von Zeichen1:Code von Zeichen1-ASCII-Code von Zeichen2:Code von Zeichen2-
    /// Das letzte Zeichen erhält keinen Abschluss-Separator "-".
    /// </summary>
    /// <param name="huffmanCode">{ASCII-Zeichencode:Code-} Mapping</param>
    /// <param name="fileName"></param>
    public static void SaveCodeTable(Dictionary<char, string> huffmanCode, string fileName)
    {
        using StreamWriter writer = new StreamWriter(fileName);
        var count = huffmanCode.Count;
        for (int i = 0; i < count; i++)
        {
            string separator = i == count - 1 ? "" : "-";
            var entry = huffmanCode.Skip(i).Take(1).First();
            writer.Write($"{(int)entry.Key}:{entry.Value}{separator}");
        }
    }
}
