using System.Text;

namespace ProgrammierAufgabe_RainbowTable.RainbowTable;

public static class PasswortGenerator
{
    /// <summary>
    /// Generiert eine Liste von Passwörtern der gewünschten Länge und Anzahl.
    /// </summary>
    /// <param name="anzahl">Anzahl der zu generierenden Passwörter.</param>
    /// <param name="zeichen">Die zu benutzenden Zeichen.</param>
    /// <param name="passwortLaenge">Die Länge der zu generierenden Passwörter.</param>
    /// <param name="zuschauen">Logging falls true.</param>
    /// <returns>Liste der generierten Passwörter.</returns>
    public static List<string> GenerierePasswoerter(int anzahl, List<char> zeichen, int passwortLaenge, bool zuschauen)
    {
        var passwoerter = new List<string>();
        int zeichenAnzahl = zeichen.Count; 
        int[] zeichenPositionen = new int[passwortLaenge];

        for (int i = 0; i < anzahl; i++)
        {
            var password = Enumerable.Range(0, passwortLaenge)
                .Aggregate(new StringBuilder(), (sb, j) => sb.Insert(0, zeichen[zeichenPositionen[j]]))
                .ToString();

            if (zuschauen)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Generierte Password {i + 1}: {password}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            passwoerter.Add(password);

            // Nächste Kombination
            zeichenPositionen[0]++;
            for (int k = 0; k < passwortLaenge - 1; k++)
            {
                if (zeichenPositionen[k] >= zeichenAnzahl)
                {
                    zeichenPositionen[k] = 0;
                    zeichenPositionen[k + 1]++;
                }
            }
        }
        return passwoerter;
    }
}
