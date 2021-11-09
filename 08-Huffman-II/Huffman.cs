using System;
using System.IO;

namespace _08_Huffman_II
{
    class Huffman
    {
        public static void Encode(FileStream input, FileStream output)
        {
            //HuffmanTree tree = HuffmanTree.Construct();
        }
        
        public static void Decode(FileStream input, FileStream output)
        {

        }

        public static HuffmanTree CreateTree(FileStream input)
        {
            long[] byteCounts = new long[256];
            byte[] buffer = new byte[4096];
            long length = input.Length;
            long pos = 0;

            input.Seek(0, SeekOrigin.Begin);
            while (pos < length)
            {
                int bufferLength = input.Read(buffer);
                pos += bufferLength;
                for (int i = 0; i < bufferLength; ++i)
                {
                    ++byteCounts[buffer[i]];
                }
            }

            return HuffmanTree.Create(byteCounts);
        }
    }
}
