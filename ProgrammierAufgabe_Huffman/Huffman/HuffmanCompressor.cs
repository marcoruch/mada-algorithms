using Helpers;

using System.Text;

namespace ProgrammierAufgabe_Huffman.Huffman
{
    internal class HuffmanCompressor
    {
        /// <summary>
        /// Methode die basierend auf Text von Input-File den Output-Table (Meta) und den Output der Komprimierten Daten erstellt.
        /// </summary>
        /// <param name="inputFileName">Relative Filepath containing Text</param>
        /// <param name="codeTableFileName">Relative Filename to output table to</param>
        /// <param name="outputFileName">Relative Filename to output compressed data to</param>
        public static void Compress(string inputFileName, string codeTableFileName, string outputFileName)
        {
            string text = File.ReadAllText(inputFileName);
            
            // Generate Tree & Save Code-Table for Compression aswell as decompression ofcourse.. :)
            int[] frequencyTable = BuildFrequencyTable(text);
            HuffmanNode root = BuildHuffmanTree(frequencyTable);
            Dictionary<char, string> huffmanCode = BuildHuffmanCode(root);
            FilesHelper.SaveCodeTable(huffmanCode, codeTableFileName);

            // Compress the file based on that tree/table
            byte[] compressed = Compress(text, huffmanCode);
            File.WriteAllBytes(outputFileName, compressed);
        }

        #region HuffmanTable

        private static Dictionary<char, string> BuildHuffmanCode(HuffmanNode root)
        {
            Dictionary<char, string> map = new Dictionary<char, string>();
            BuildCode(map, root, "");
            return map;
        }

        private static void BuildCode(Dictionary<char, string> map, HuffmanNode node, string code)
        {
            if (!node.IsLeaf())
            {
                BuildCode(map, node.Left!, code + '0');
                BuildCode(map, node.Right!, code + '1');
            }
            else
            {
                map[node.Char] = code;
            }
        }

        private static int[] BuildFrequencyTable(string text)
        {
            int[] freq = new int[128];
            foreach (char c in text)
            {
                freq[c]++;
            }
            return freq;
        }

        private static HuffmanNode BuildHuffmanTree(int[] frequencyTable)
        {
            PriorityQueue<HuffmanNode, int> pq = new();
            for (char i = (char)0; i < 128; i++)
            {
                if (frequencyTable[i] > 0)
                {
                    pq.Enqueue(new HuffmanNode(i, frequencyTable[i]), frequencyTable[i]);
                }
            }

            // Alle Elemente mit .Dequeue() aus der Queue nehmen (via Priority) und die HuffmanNode Baumstruktur fertigbilden.
            while (pq.Count > 1)
            {
                HuffmanNode left = pq.Dequeue();
                HuffmanNode right = pq.Dequeue();
                pq.Enqueue(new HuffmanNode('\0', left.Frequency + right.Frequency, left, right), left.Frequency + right.Frequency);
            }

            return pq.Dequeue();
        }

        #endregion

        #region Compression

        /// <summary>
        /// Zu kompressierender text und huffmancode Tabelle werden gemäss Spezifikation gebildet.
        /// Zuerst nehmen wir alle Bit-Codes und werfen sie nacheinander 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="huffmanCode"></param>
        /// <returns></returns>
        private static byte[] Compress(string text, Dictionary<char, string> huffmanCode)
        {
            StringBuilder bitString = new();
            foreach (char c in text)
            {
                bitString.Append(huffmanCode[c]);
            }

            bitString.Append('1');

            // Doppel-Modulo einmal für Length da dies bereits ein Multiple von 8 sein kann und ein 8-64 bspw. -56 gäbe...
            int paddingLength = (8 - bitString.Length % 8) % 8;
            for (int i = 0; i < paddingLength; i++)
            {
                bitString.Append('0');
            }

            int byteArrayLength = bitString.Length / 8;
            byte[] byteArray = new byte[byteArrayLength];
            for (int i = 0; i < byteArrayLength; i++)
            {
                string byteString = bitString.ToString().Substring(i * 8, 8);
                byteArray[i] = Convert.ToByte(byteString, 2);
            }

            return byteArray;
        }

        #endregion
    }
}
