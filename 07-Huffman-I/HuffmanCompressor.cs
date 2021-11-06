using System.IO;

namespace _07_Huffman_I
{
    class HuffmanCompressor
    {
        public HuffmanCompressor(string filename)
        {
            _filename = filename;
            _tree = null;
        }

        // This will be useful later
        public void Compress()
        {

        }

        public void CreateTree()
        {
            int[] byteCounts = new int[256];

            using (var file = new FileStream(_filename, FileMode.Open))
            {
                long length = file.Length;
                long pos = 0;
                while (pos < length)
                {
                    ++byteCounts[file.ReadByte()];
                    ++pos;
                }
            }

            _tree = HuffmanTree.Construct(byteCounts);
        }

        public HuffmanTree Tree
        {
            get
            {
                return _tree;
            }
        }

        string _filename;
        HuffmanTree _tree;
    }
}
