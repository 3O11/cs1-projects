using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _28_Huffman_II_Breaker
{
    internal static class Program
    {
        static void Main()
        {
            cs_prg_6.Huffman refImpl = new cs_prg_6.Huffman();

            int size = 2;
            Random r = new Random();
            MemoryStream input = new MemoryStream();
            MemoryStream refOut = new MemoryStream();
            MemoryStream newOut = new MemoryStream();

            while (Equal(refOut, newOut))
            {
                Console.WriteLine($"Size: {size}, still equal.");

                byte[] newBuffer = new byte[size];
                r.NextBytes(newBuffer);
                input = new MemoryStream(newBuffer);


                refImpl.Init(input, refOut);
                refImpl.Run();

                _08_Huffman_II.Huffman.Encode(input, newOut);

                if (size < 100000) size *= 2;
            }

            using (FileStream refFileDump = new FileStream("input.out", FileMode.Create))
            {
                refFileDump.Write(input.ToArray());
            }

            using (FileStream refFileDump = new FileStream("reference.out", FileMode.Create))
            {
                refFileDump.Write(refOut.ToArray());
            }

            using (FileStream newFileDump = new FileStream("newImplem.out", FileMode.Create))
            {
                newFileDump.Write(newOut.ToArray());
            }
        }

        static bool Equal(Stream s1, Stream s2)
        {
            if (s1.Length != s2.Length) return false;

            s1.Seek(0, SeekOrigin.Begin);
            s2.Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < s1.Length; i++)
            {
                if (s1.ReadByte() != s2.ReadByte()) return false;
            }

            return true;
        }
    }
}
