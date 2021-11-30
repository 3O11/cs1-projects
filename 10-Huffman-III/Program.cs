using System;
using System.IO;

namespace _08_Huffman_II
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1 || !args[0].EndsWith(".huff"))
            {
                Console.WriteLine("Argument Error");
                return;
            }
            FileStream input;
            FileStream output;
            try
            {
                input = new FileStream(args[0], FileMode.Open);
                output = new FileStream(args[0].Substring(0, args[0].Length - 5), FileMode.Create);
            }
            catch
            {
                Console.WriteLine("File Error");
                return;
            }

            Huffman.Decode(input, output);

            input.Close();
            input.Dispose();
            output.Close();
            output.Dispose();
        }
    }
}
