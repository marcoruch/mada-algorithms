using System.Text;

namespace ProgrammierAufgabe_Huffman.Huffman
{
    /// <summary>
    /// Mit einer Codetable & Compressfile können wir den originalen Text wiederherstellen
    /// </summary>
    internal class HuffmanDecompressor
    {
        /// <summary>
        /// Via dem Code-Table File und dem Compressed File können wir das originalfile wiederherstellen.
        /// </summary>
        /// <param name="compressedFileName">Compressed file</param>
        /// <param name="codeTableFileName">Code Table matching the compressed file</param>
        /// <param name="outputFileName">Result relative filename</param>
        public static void Decompress(string compressedFileName, string codeTableFileName, string outputFileName)
        {
            byte[] compressed = File.ReadAllBytes(compressedFileName);
            Dictionary<string, char> huffmanCode = ReadCodeTable(codeTableFileName);

            string bitString = BytesToBitString(compressed);

            string decodedText = DecodeBitString(bitString, huffmanCode);

            File.WriteAllText(outputFileName, decodedText);
        }

        private static Dictionary<string, char> ReadCodeTable(string fileName)
        {
            Dictionary<string, char> map = new Dictionary<string, char>();
            string huffmanCode = string.Empty;
            using (StreamReader reader = new StreamReader(fileName))
            {
                huffmanCode += reader.ReadLine();
            }

            foreach (string pair in huffmanCode.Split("-"))
            {
                string[] parts = pair.Split(':');
                char ch = (char)int.Parse(parts[0]);
                string code = parts[1];
                map[code] = ch;
            }

            return map;
        }

        private static string BytesToBitString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        private static string DecodeBitString(string bitString, Dictionary<string, char> huffmanCode)
        {
            int lastIndex = bitString.LastIndexOf('1');
            bitString = bitString.Substring(0, lastIndex);

            StringBuilder decodedText = new StringBuilder();
            StringBuilder currentCode = new StringBuilder();
            foreach (char bit in bitString)
            {
                currentCode.Append(bit);
                if (huffmanCode.ContainsKey(currentCode.ToString()))
                {
                    decodedText.Append(huffmanCode[currentCode.ToString()]);
                    currentCode.Clear();
                }
            }

            return decodedText.ToString();
        }
    }
}
