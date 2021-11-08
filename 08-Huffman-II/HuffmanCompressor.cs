using System;
using System.IO;

namespace _07_Huffman_I
{
    class HuffmanCompressor
    {
        public HuffmanCompressor(string filename)
        {
            _inputFilename = filename;
            _tree = null;
        }

        public bool Init()
        {
            try
            {
                _inputFile = new FileStream(_inputFilename, FileMode.Open);
            }
            catch
            {
                return false;
            }

            long[] byteCounts = new long[256];

            _inputFile.Seek(0, SeekOrigin.Begin);
            long length = _inputFile.Length;
            long pos = 0;
            while (pos < length)
            {
                ++byteCounts[_inputFile.ReadByte()];
                ++pos;
            }

            _tree = HuffmanTree.Construct(byteCounts);
            return true;
        }

        // This will be useful later
        public void Compress()
        {

        }

        public void CleanUp()
        {
            _inputFile.Close();
            _inputFile.Dispose();
        }

        public HuffmanTree Tree
        {
            get
            {
                return _tree;
            }
        }

        string _inputFilename;
        FileStream _inputFile;
        
        HuffmanTree _tree;
    }
}
