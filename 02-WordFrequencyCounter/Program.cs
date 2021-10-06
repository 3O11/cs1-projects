using System;
using System.IO;
using System.Collections.Generic;

namespace _02_WordFrequencyCounter {
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
            SortedDictionary<string, int> counter = new SortedDictionary<string, int>();
            string word;
            while ((word = words.GetNextRawWord()) != null) {
                if (counter.ContainsKey(word)) {
                    counter[word]++;
                }
                else {
                    counter.Add(word, 1);
                }
            }
            words.Dispose();

            foreach(var pair in counter) {
                Console.WriteLine(pair.Key + ": " + pair.Value);
            }
        }
    }
}
