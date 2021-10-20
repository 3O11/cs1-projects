using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace _03_BlockAligner {
    class Program {
        static void Main(string[] args) {
            int lineLength;
            if (args.Length < 3 || !int.TryParse(args[args.Length - 1], out lineLength) || lineLength <= 0) {
                Console.WriteLine("Argument Error");
                return;
            }

            IWordProcessor aligner = new FileBlockAligner(lineLength, args[args.Length - 2]);

            int argIter = 0;
            if (args[0] == "--highlight-spaces") {
                argIter++;
                var props = new Dictionary<string, object>();
                props["LineEnding"] = "<-";
                props["Space"] = '.';
                aligner.SetProperties(props);
            }

            while (argIter < args.Length - 2) {
                if (!File.Exists(args[argIter])) {
                    argIter++;
                    continue;
                }
                IWordExtractor extractor = new WordExtractor(args[argIter]);

                string nextWord = extractor.GetNextRawWord();
                while (nextWord != null)
                {
                    aligner.NextWord(nextWord);
                    nextWord = extractor.GetNextRawWord();
                }

                extractor.Finish();
                argIter++;
            }

            aligner.Finish();
        }
    }
}
