using System;
using System.IO;

// Sneaky pre-deadline fix

namespace _07_Huffman_I
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Argument Error");
                return;
            }

            HuffmanCompressor compressor = new HuffmanCompressor(args[0]);

            if (!compressor.Init())
            {
                Console.WriteLine("File Error");
                return;
            }

            Console.WriteLine(compressor.Tree);

            //compressor.Compress();
            compressor.CleanUp();
        }
    }
}
