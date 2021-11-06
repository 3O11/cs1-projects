using System;
using System.IO;

//
// From my previous attempts it's painfully obvious that I haven't learned
// anything from the Bug Finding Tale ...
//

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

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("File Error");
                return;
            }

            HuffmanCompressor compressor = new HuffmanCompressor(args[0]);
            compressor.CreateTree();
            Console.WriteLine(compressor.Tree);
        }
    }
}
