using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammierAufgabe_RainbowTable.RainbowTable;

public static class RainbowTable
{
    /// <summary>
    /// Erzeugt die Rainbow-Table aus einer Liste von Startpasswörtern unter Verwendung von <see cref="ErzeugeKettenEndHash"/> für jedes Startpasswort.
    /// </summary>
    /// <param name="startPasswoerter">Liste der Startpasswörter.</param>
    /// <param name="zeichen">Die möglichen Zeichen.</param>
    /// <param name="passwortLaenge">Die Länge der Passwörter.</param>
    /// <param name="zuschauen">Logging wenn wahr.</param>
    /// <returns>Die Rainbow-Table als Dictionary.</returns>
    public static Dictionary<string, string> ErzeugeRainbowTabelle(List<string> startPasswoerter, int kettenLaenge, List<char> zeichen, int passwortLaenge, bool zuschauen) => 
        startPasswoerter.ToDictionary(startPasswort => ErzeugeKettenEndHash(startPasswort, kettenLaenge, zeichen, passwortLaenge, zuschauen), startPasswort => startPasswort);

    /// <summary>
    /// Sucht das Klartextpasswort zu einem gegebenen Hashwert in der Rainbow-Table.
    /// </summary>
    /// <param name="hashwert">Der zu suchende Hashwert.</param>
    /// <param name="rainbowTabelle">Die Rainbow-Table als Dictionary.</param>
    /// <param name="zeichen">Die möglichen Zeichen.</param>
    /// <param name="passwortLaenge">Die Länge der Passwörter.</param>
    /// <param name="kettenLaenge">Die Kettenlänge.</param>
    /// <returns>Das gefundene Passwort oder ein leerer String.</returns>
    public static string FindePasswortZuHash(string hashwert, Dictionary<string, string> rainbowTabelle, List<char> zeichen, int passwortLaenge, int kettenLaenge)
    {
        for (int position = kettenLaenge - 1; position >= 0; position--)
        {
            string tempHash = hashwert;

            // Reduziere und hashe rückwärts durch die Kette
            for (int i = position; i < kettenLaenge; i++)
            {
                string passwort = ReduziereHashZuPasswort(tempHash, i, zeichen, passwortLaenge);
                tempHash = Md5Hasher.BerechneMD5Hash(passwort);
            }

            // Prüfe, ob der EndHash in der Rainbow-Table ist
            if (rainbowTabelle.TryGetValue(tempHash, out string? startPasswort))
            {
                // Rekonstruiere die Kette ab dem Startpasswort
                string gefunden = RekonstruierePasswortAusKette(startPasswort, hashwert, zeichen, passwortLaenge, kettenLaenge);
                if (!string.IsNullOrEmpty(gefunden))
                {
                    return gefunden;
                }
            }
        }
        return "";
    }

    /// <summary>
    /// Testet die Reduktionsfunktion mit Beispieldaten.
    /// </summary>
    /// <param name="reduktionen">Die zu prüfenden Reduktionen.</param>
    /// <param name="zeichen">Die möglichen Zeichen.</param>
    /// <param name="passwortLaenge">Die Länge der Passwörter.</param>
    public static void TesteReduktion(Dictionary<string, string> reduktionen, List<char> zeichen, int passwortLaenge)
    {
        int position = 0;

        foreach (var reduktion in reduktionen)
        {
            var testResultat = ReduziereHashZuPasswort(reduktion.Key, position++, zeichen, passwortLaenge) == reduktion.Value;
            Console.WriteLine($"Prüfe: {reduktion.Key} auf {reduktion.Value}: {testResultat}");
        }
        Console.WriteLine();
    }

    #region Supporting Methoden

    /// <summary>
    /// Erzeugt das End-Hash einer Kette ausgehend von einem Startpasswort.
    /// </summary>
    /// <param name="startPasswort">Das Startpasswort der Kette.</param>
    /// <param name="kettenLaenge">Die Kettenlänge.</param>
    /// <param name="zeichen">Die möglichen Zeichen.</param>
    /// <param name="passwortLaenge">Die Länge der Passwörter.</param>
    /// <param name="zuschauen">Logging wenn wahr.</param>
    /// <returns>Das End-Hash der Kette.</returns>
    private static string ErzeugeKettenEndHash(string startPasswort, int kettenLaenge, List<char> zeichen, int passwortLaenge, bool zuschauen)
    {
        string passwort = startPasswort;
        for (int i = 0; i < kettenLaenge; i++)
        {
            string hash = Md5Hasher.BerechneMD5Hash(passwort);
            passwort = ReduziereHashZuPasswort(hash, i, zeichen, passwortLaenge);
        }
        // Das letzte Hash ist das End-Hash
        var endHash = Md5Hasher.BerechneMD5Hash(passwort);

        if (zuschauen)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Endhash für Passwort {startPasswort} ist: {endHash}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        return endHash;
    }

    /// <summary>
    /// Reduziert einen Hashwert auf ein neues Passwort.
    /// </summary>
    /// <param name="hash">Der zu reduzierende Hashwert.</param>
    /// <param name="position">Die aktuelle Position in der Kette.</param>
    /// <param name="zeichen">Die möglichen Zeichen.</param>
    /// <param name="passwortLaenge">Die Länge der Passwörter.</param>
    /// <returns>Das reduzierte Passwort.</returns>
    private static string ReduziereHashZuPasswort(string hash, int position, List<char> zeichen, int passwortLaenge)
    {
        BigInteger hashWert = BigInteger.Parse("0" + hash, System.Globalization.NumberStyles.HexNumber);
        hashWert += position;
        int zeichenAnzahl = zeichen.Count;
        char[] passwort = new char[passwortLaenge];
        for (int i = passwortLaenge - 1; i >= 0; i--)
        {
            int index = (int)(hashWert % zeichenAnzahl);
            passwort[i] = zeichen[index];
            hashWert /= zeichenAnzahl;
        }
        return new string(passwort);
    }

    /// <summary>
    /// Rekonstruiert das Passwort aus einer Kette, das zu einem gegebenen Hashwert führt.
    /// </summary>
    /// <param name="startPasswort">Das Startpasswort der Kette.</param>
    /// <param name="zielHash">Der gesuchte Hashwert.</param>
    /// <param name="zeichen">Die möglichen Zeichen.</param>
    /// <param name="passwortLaenge">Die Länge der Passwörter.</param>
    /// <param name="kettenLaenge">Die Kettenlänge.</param>
    /// <returns>Das gefundene Passwort oder ein leerer String.</returns>
    private static string RekonstruierePasswortAusKette(string startPasswort, string zielHash, List<char> zeichen, int passwortLaenge, int kettenLaenge)
    {
        string passwort = startPasswort;
        for (int i = 0; i < kettenLaenge; i++)
        {
            string hash = Md5Hasher.BerechneMD5Hash(passwort);
            if (hash == zielHash)
                return passwort;
            passwort = ReduziereHashZuPasswort(hash, i, zeichen, passwortLaenge);
        }
        return "";
    }

    #endregion
}