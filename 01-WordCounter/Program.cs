using System;
using System.IO;

namespace _01_WordCounter {
    class Program {
        static void Main(string[] args) {
            if (args.Length != 1) {
                Console.WriteLine("Argument Error");
                return;
            }

            if (!File.Exists(args[0])) {
                Console.WriteLine("File Error");
                return;
            }

            WordExtractor words = new WordExtractor(args[0]);
            int counter = 0;
            while (words.GetNextRawWord() != null) {
                counter++;
            }
            words.Dispose();

            Console.WriteLine(counter);
        }
    }
}
