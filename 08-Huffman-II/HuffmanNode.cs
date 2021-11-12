using System;
using System.Collections;
using System.Collections.Generic;

namespace _08_Huffman_II
{
    class HuffmanNode : IComparable<HuffmanNode>
    {
        // Leaf node constructor
        public HuffmanNode(long value, long weight)
        {
            Value = value;
            Weight = weight;
            Left = null;
            Right = null;
        }

        // Inner node constructor
        public HuffmanNode(HuffmanNode left, HuffmanNode right)
        {
            Value = 2 * left.Value + 2 * (right.Value + 256);
            Weight = left.Weight + right.Weight;
            Left = left;
            Right = right;
        }

        public int CompareTo(HuffmanNode other)
        {
            if (other == null) return (int)Weight;
            if (Weight - other.Weight != 0) return (int)(Weight - other.Weight);
            if (Value - other.Value != 0) return (int)(Value - other.Value);

            return 0;
        }

        public override string ToString()
        {
            return IsLeaf() ? "*" + Value + ":" + Weight : Weight + " " + Left + " " + Right;
        }

        public bool IsLeaf()
        {
            return Left == null && Right == null;
        }

        // TODO: Move the traversal to HuffmanTree.Encode(), return only the CURRENT node encoded
        public byte[] Encode()
        {
            if (IsLeaf())
            {
                long result = 1 | (0x00FFFFFFFFFFFFFE & (Weight << 1)) | (Value << 56);
                return BitConverter.GetBytes(result);
            }
            else
            {
                long result = 0x00FFFFFFFFFFFFFE & (Weight << 1);
                return BitConverter.GetBytes(result);
            }
        }

        public long Weight { get; }

        public long Value { get; }

        public HuffmanNode Left { get; }

        public HuffmanNode Right { get; }
    }
}
