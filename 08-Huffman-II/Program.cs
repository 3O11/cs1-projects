using System;
using System.IO;

// This solution isn't good, I'll fix it later if I have time.

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
            FileStream output;
            try
            {
                input = new FileStream(args[0], FileMode.Open);
                output = new FileStream(args[0] + ".huff", FileMode.OpenOrCreate);
            }
            catch
            {
                Console.WriteLine("File Error");
                return;
            }

            Huffman.Encode(input, output);

            input.Close();
            input.Dispose();
            output.Close();
            output.Dispose();
        }
    }
}
