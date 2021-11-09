using System;
using System.IO;

// Sneaky pre-deadline fix

namespace _08_Huffman_II
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
            FileStream input;
            try
            {
                input = new FileStream(args[0], FileMode.Open);
            }
            catch
            {
                Console.WriteLine("File Error");
                return;
            }

            Console.WriteLine(Huffman.CreateTree(input));
        }
    }
}
