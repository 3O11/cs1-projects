using System;

namespace _03_BlockAligner {
    class ConsoleBlockAligner : BlockAligner {
        public ConsoleBlockAligner(int lineLength) : base(lineLength) {

        }

        protected override void saveLine(string line) {
            Console.WriteLine(line);
        }
    }
}
