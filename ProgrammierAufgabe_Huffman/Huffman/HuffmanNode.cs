namespace ProgrammierAufgabe_Huffman.Huffman
{
    public class HuffmanNode
    {
        public char Char { get; }
        public int Frequency { get; }
        public HuffmanNode? Left { get; }
        public HuffmanNode? Right { get; }
        public bool IsLeaf() => Left == null && Right == null;

        public HuffmanNode(char c, int frequency)
        {
            Char = c;
            Frequency = frequency;
        }

        public HuffmanNode(char c, int frequency, HuffmanNode left, HuffmanNode right)
        {
            Char = c;
            Frequency = frequency;
            Left = left;
            Right = right;
        }
    }
}
