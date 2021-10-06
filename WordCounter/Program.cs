using System;
using System.IO;

namespace WordCounter {
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

            int counter = 0;
            WordExtractor words = new WordExtractor(args[0]);
            string word = words.GetNextRawWord();
            while (word != null) {
                Console.WriteLine(word);
                counter++;
                word = words.GetNextRawWord();
            }
            words.Dispose();

            Console.WriteLine(counter);
        }
    }
}
