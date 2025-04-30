using ProgrammierAufgabe_RainbowTable.RainbowTable;

namespace ProgrammierAufgabe_RainbowTable;

internal class Program
{
    const int PasswortLaenge = 7, AnzahlPasswoerter = 2000, KettenLaenge = 2000;
    const string ZeichenString = "0123456789abcdefghijklmnopqrstuvwxyz";
    static readonly List<char> Zeichen = ZeichenString.ToList();

    static void Main()
    {
        // Generierung
        Console.WriteLine($"1. Generiere {AnzahlPasswoerter} Startpasswörter der Länge {PasswortLaenge}, mögliche Zeichen aus '{ZeichenString}'....");
        Console.WriteLine("   Bei Generierung zuschauen: y/n");
        string? ynPasswoerter = Console.ReadLine();
        var startPasswoerter = PasswortGenerator.GenerierePasswoerter(AnzahlPasswoerter, Zeichen, PasswortLaenge, ynPasswoerter?.ToLower() == "y");
        Console.WriteLine($"2. Generiere die Rainbow-Table mit Kettenlänge {KettenLaenge} für die genertieren Passwörter...");
        Console.WriteLine("   Bei Generierung zuschauen: y/n");
        string? ynTabelle = Console.ReadLine();
        var rainbowTabelle = RainbowTable.RainbowTable.ErzeugeRainbowTabelle(startPasswoerter, KettenLaenge, Zeichen, PasswortLaenge, ynTabelle?.ToLower() == "y");

        // Übungshash prüfen
        Console.WriteLine($"3. Prüfe den Hash-Wert aus der Aufgabenstellung (können wir das zugehörige Passwort rückermitteln)....");
        string hash = "1d56a37fb6b08aa709fe90e12ca59e12";
        Console.WriteLine("   Hash: " + hash);

        string gefundenesPasswort = RainbowTable.RainbowTable.FindePasswortZuHash(hash, rainbowTabelle, Zeichen, PasswortLaenge, KettenLaenge);

        if (!string.IsNullOrEmpty(gefundenesPasswort))
        {
            string rehash = Md5Hasher.BerechneMD5Hash(gefundenesPasswort);
            Console.WriteLine("   Rehash: " + rehash);

            if (hash == rehash)
            {
                Console.WriteLine("   Gefundenes Passwort: " + gefundenesPasswort);
            }
            else
            {
                Console.WriteLine("   Es wurde ein Passwort gefunden, aber der Rehash stimmt nicht überein: " + gefundenesPasswort);
            }
        }

        // Hashing Test
        Console.WriteLine("");
        Console.WriteLine("4. Hashing überprüfen, Passwort-Hash Paare mit 4 positives und 1 negative gegenprüfen....");
        Dictionary<string, string> passwortHashPairs = new()
        {
            { "0000000", "29c3eea3f305d6b823f562ac4be35217" },
            { "87inwgn", "12e2feb5a0feccf82a8d4172a3bd51c3" },
            { "frrkiis", "437988e45a53c01e54d21e5dc4ae658a" },
            { "dues6fg", "c0e9a2f2ae2b9300b6f7ef3e63807e84" },
            { "marco00", "123456e45a53c01e54d21e5dc4ae658a" } // False Beispiel
        };
        Md5Hasher.TesteHashing(passwortHashPairs);

        // Reduktion Test
        Console.WriteLine("");
        Console.WriteLine("5. Reduktion überprüfen, Hash-Passwort Paare mit 3 positives und 1 negative gegenprüfen...");
        Dictionary<string, string> hashPasswortPairs = new()
        {
            { "29c3eea3f305d6b823f562ac4be35217", "87inwgn" },
            { "12e2feb5a0feccf82a8d4172a3bd51c3", "frrkiis" },
            { "437988e45a53c01e54d21e5dc4ae658a", "dues6fg" },
            { "123456e45a53c01e54d21e5dc4ae658a", "marco00" } // False Beispiel
        };
        RainbowTable.RainbowTable.TesteReduktion(hashPasswortPairs, Zeichen, PasswortLaenge);

        Console.WriteLine("Drücke Taste um zu beenden.");
        Console.ReadKey();
    }
}