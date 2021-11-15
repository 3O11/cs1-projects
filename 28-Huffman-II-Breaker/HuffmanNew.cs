using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace _08_Huffman_II
{
    class Huffman
    {
        public static void Encode(Stream input, Stream output)
        {
            HuffmanTree tree = CreateTree(input);
            output.Write(_huffFileHeader);
            output.Write(tree.Encode());
            for (int i = 0; i < 8; i++) output.WriteByte(0x00);

            bool[][] lookup = tree.GetLookupTable();
            List<bool> buffer = new List<bool>();

            int next;
            while ((next = input.ReadByte()) != -1)
            {
                buffer.AddRange(lookup[next]);

                if (buffer.Count >= 8)
                {
                    byte tempOut = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        if (buffer[i]) tempOut |= (byte)(1 << i);
                    }
                    buffer.RemoveRange(0, 8);
                    output.WriteByte(tempOut);
                }
            }

            while (buffer.Count > 0)
            {
                byte outByte = 0;
                int j = 0;
                while (j < buffer.Count)
                {
                    if (buffer[j]) outByte += (byte)(1 << j);
                    ++j;
                }

                buffer.RemoveRange(0, buffer.Count >= 8 ? 8 : buffer.Count);
                output.WriteByte(outByte);
            }
        }

        public static void Decode(FileStream input, FileStream output)
        {

        }

        public static HuffmanTree CreateTree(Stream input)
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
            input.Seek(0, SeekOrigin.Begin);

            return HuffmanTree.Create(byteCounts);
        }

        static byte[] _huffFileHeader = new byte[] { 0x7B, 0x68, 0x75, 0x7C, 0x6D, 0x7D, 0x66, 0x66 };
    }
}
