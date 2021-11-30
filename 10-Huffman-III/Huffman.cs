using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace _08_Huffman_II
{
    class Huffman
    {
        public static void Encode(FileStream input, FileStream output)
        {
            HuffmanTree tree = CreateTree(input);
            output.Write(_huffFileHeader);
            output.Write(tree.Encode());
            for (int i = 0; i < 8; i++) output.WriteByte(0x00);

            bool[][] lookup = tree.GetEncodeLookupTable();
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
            int next;

            {   // Check if the header is correct
                int i = 0;
                while ((next = input.ReadByte()) != -1)
                {
                    if (i < 8 && next != _huffFileHeader[i]) return;
                    if (i >= 8) break;

                    ++i;
                }
            }

            HuffmanTree tree = null;
            {   // Decode the tree
                List<long> encodedNodes = new List<long>();
                long nodeBuffer = 0;
                int i = 0;
                while ((next = input.ReadByte()) != -1)
                {
                    if (i % 8 == 0)
                    {
                        if (nodeBuffer == 0 && next == 0) break;

                        encodedNodes.Add(nodeBuffer);
                        nodeBuffer = next;
                    }
                    else
                    {
                        nodeBuffer = (long)(0x00_00_00_FF & next) | (nodeBuffer << 8);
                    }

                    ++i;
                }
                tree = HuffmanTree.CreateFromEncoding(encodedNodes.ToArray());
            }
            if (tree == null) return;



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
            input.Seek(0, SeekOrigin.Begin);

            return HuffmanTree.CreateFromCounts(byteCounts);
        }

        static byte[] _huffFileHeader = new byte[] { 0x7B, 0x68, 0x75, 0x7C, 0x6D, 0x7D, 0x66, 0x66 };
    }
}
