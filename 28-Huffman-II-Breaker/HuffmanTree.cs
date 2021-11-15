using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cs_prg_6 {
    public class HuffmanTree : Tree {
        private HuffmanTree() {

        }
        private HuffmanTree(DataPair head) {
            this.Data = head;
        }

        public static HuffmanTree Build(List<DataPair> pairs) {
            var forest = new SortedSet<HuffmanTree>();

            foreach (var pair in pairs) {
                HuffmanTree temp = new HuffmanTree(pair);
                forest.Add(temp);
            }

            while (forest.Count > 1) {
                huffReduce(forest);
            }

            return forest.Min;
        }

        private static void huffReduce(SortedSet<HuffmanTree> forest) {
            HuffmanTree lower = forest.Min;
            forest.Remove(lower);
            HuffmanTree higher = forest.Min;
            forest.Remove(higher);
            
            HuffmanTree merged = huffMerge(lower, higher);

            forest.Add(merged);
        }

        private static HuffmanTree huffMerge(HuffmanTree lower, HuffmanTree higher) {
            HuffmanTree merged = new HuffmanTree();

            merged.Data = new DataPair();
            merged.Data.Value = lower.Data.Value + higher.Data.Value;
            merged.Data.Key = lower.Data.Key*2 + 2*(higher.Data.Key + 256);

            merged.Left = lower;
            merged.Right = higher;

            return merged;
        }
        
        //this will need its own class at some point
        public byte[] GetBinPrefix() {
            List<byte> binPrefix = new List<byte>();

            ulong node = 0;

            if (IsLeaf()) {
                //set node encoding to leaf
                node = 1;
                //bits 1-55 are the weight
                node += ( 0x00FFFFFFFFFFFFFE & ((ulong)Data.Value << 1));
                //bits 56-63 are the char code
                node += ( 0xFF00000000000000 & ((ulong)Data.Key << 56));

                for (int i = 0; i < 8; i++) {
                    binPrefix.Add((byte)node);
                    node = node >> 8;
                }
            }
            else {
                //bits 1-55 are the weight
                node += ( 0x00FFFFFFFFFFFFFE & ((ulong)Data.Value << 1));
                for (int i = 0; i < 8; i++) {
                    binPrefix.Add((byte)node);
                    node = node >> 8;
                }
                
                HuffmanTree leftChild = (HuffmanTree) Left;
                HuffmanTree rightChild = (HuffmanTree) Right;

                binPrefix.AddRange(leftChild.GetBinPrefix());
                binPrefix.AddRange(rightChild.GetBinPrefix());
            }

            return binPrefix.ToArray();
        }

        public Dictionary<int, List<bool>> GetEncodings() {
            var encodings = new Dictionary<int, List<bool>>();

            GetEncodingsRecursive(encodings, this);
            
            return encodings;
        }
        
        private static void GetEncodingsRecursive(Dictionary<int, List<bool>> encodings, HuffmanTree tree, List<bool> path = null) {
            if (path == null) path = new List<bool>();
            
            if (tree.IsLeaf()) {
                encodings[tree.Data.Key] = path;
            }
            else {
                var leftPath = new List<bool>(path);
                leftPath.Add(false);
                var rightPath = new List<bool>(path);
                rightPath.Add(true);
                
                GetEncodingsRecursive(encodings, (HuffmanTree)tree.Left, leftPath);
                GetEncodingsRecursive(encodings, (HuffmanTree)tree.Right, rightPath);
            }
        }
    }
}