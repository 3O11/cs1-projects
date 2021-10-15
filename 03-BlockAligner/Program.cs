using System;
using System.IO;
using System.Diagnostics;

namespace _03_BlockAligner {
    class Program {
        static void Main(string[] args) {
            // Argument checking and parsing
            int lineLength;
            if (args.Length != 3 || !int.TryParse(args[2], out lineLength) || lineLength <= 0) {
                Console.WriteLine("Argument Error");
                return;
            }

            if (!File.Exists(args[0])) {
                Console.WriteLine("File Error");
                return;
            }

            // Initialization
            IWordProcessor aligner = new FileBlockAligner(lineLength, args[1]);
            IWordExtractor extractor = new WordExtractor(args[0]);

            // Running
            string nextWord = extractor.GetNextRawWord();
            while (nextWord != null) {
                aligner.NextWord(nextWord);
                nextWord = extractor.GetNextRawWord();
            }

            // Cleanup
            aligner.Finish();
            extractor.Finish();
        }
    }
}
