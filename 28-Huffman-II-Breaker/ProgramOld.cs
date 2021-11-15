using System;
using System.Collections.Generic;
using System.IO;

namespace cs_prg_6 {
    //class Program {
    //    static void Main(string[] args) {
    //        if (args.Length != 1) {
    //            Console.WriteLine("Argument Error");
    //            return;
    //        }
    //        Huffman runner = new Huffman();
    //        if (!runner.Init(args[0])) {
    //            Console.WriteLine("File Error");
    //            return;
    //        }
    //        runner.Run();
    //        runner.UnInit();
    //    }
    //}

    class Huffman {
        public bool Init(Stream input, Stream output) {
            //if (!File.Exists(filename)) return false;

            InFileHandle = input;
            OutFileHandle = output;
            Counter = new Dictionary<int, int>();
            return true;
        }

        public void Run() {
            CountOccurrences();
            CreateSortedPairs();
            HuffTree = HuffmanTree.Build(SortedPairs);
            
            //Write Header
            OutFileHandle.WriteByte(0x7B);
            OutFileHandle.WriteByte(0x68);
            OutFileHandle.WriteByte(0x75);
            OutFileHandle.WriteByte(0x7C);
            OutFileHandle.WriteByte(0x6D);
            OutFileHandle.WriteByte(0x7D);
            OutFileHandle.WriteByte(0x66);
            OutFileHandle.WriteByte(0x66);
            
            if (HuffTree != null) {
                byte[] serializedTree = HuffTree.GetBinPrefix();
                OutFileHandle.Write(serializedTree, 0, serializedTree.Length);
                for (int i = 0; i < 8; i++) {
                    OutFileHandle.WriteByte(0x00);
                }
                
                Encode();
            }
        }

        private void CountOccurrences() {
            int temp = InFileHandle.ReadByte();

            //m_FileHandle.Length was too expensive, I wonder why ... 
            while(temp != -1) {
                if(Counter.ContainsKey(temp)) {
                    Counter[temp]++;
                }
                else {
                    Counter[temp] = 1;
                }

                temp = InFileHandle.ReadByte();
            }

            // Prep for another traversal
            InFileHandle.Seek(0, SeekOrigin.Begin);
        }

        private void Encode() {
            Dictionary<int, List<bool>> encodings = HuffTree.GetEncodings();
            List<bool> buffer = new List<bool>();

            int nextChar = InFileHandle.ReadByte();
            while (nextChar != -1) {
                buffer.AddRange(encodings[nextChar]);
                WriteBuffer(buffer);
                nextChar = InFileHandle.ReadByte();
            }
            WriteBuffer(buffer);

            if (buffer.Count > 0) {
                byte finalByte = 0;
                int j = 0;
                while (j < buffer.Count) {
                    if (buffer[j]) finalByte += (byte) (1 << j);
                    ++j;
                }

                OutFileHandle.WriteByte(finalByte);
            }
        }

        private void WriteBuffer(List<bool> buffer) {
            while (buffer.Count >= 8) {
                byte outByte = 0;
                for (int i = 0; i < 8; i++) {
                    if (buffer[i]) outByte |= (byte) (1 << i);
                }
                buffer.RemoveRange(0, 8);
                OutFileHandle.WriteByte(outByte);
            }
        }

        private void CreateSortedPairs() {
            SortedPairs = new List<DataPair>();

            foreach (var item in Counter) {
                DataPair temp = new DataPair {
                    Key = item.Key,
                    Value = item.Value
                };

                SortedPairs.Add(temp);
            }
        }

        private Stream InFileHandle;
        private Stream OutFileHandle;
        private Dictionary<int, int> Counter;
        private List<DataPair> SortedPairs;
        private HuffmanTree HuffTree;
    }
}