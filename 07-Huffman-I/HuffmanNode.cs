using System;

namespace _07_Huffman_I
{
    class HuffmanNode : IComparable<HuffmanNode>
    {
        // Leaf node constructor
        public HuffmanNode(long value, long weight)
        {
            Value = value;
            Weight = weight;
            _left = null;
            _right = null;
        }

        // Inner node constructor
        public HuffmanNode(HuffmanNode left, HuffmanNode right)
        {
            Value = 2 * left.Value + 2 * (right.Value + 256);
            Weight = left.Weight + right.Weight;
            _left = left;
            _right = right;
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
            return (_left == null) ? "*" + Value + ":" + Weight : Weight + " " + _left + " " + _right;
        }

        public long Weight { get; }

        public long Value { get; }

        public HuffmanNode Left
        {
            get => _left;
        }

        public HuffmanNode Right
        {
            get => _right;
        }

        HuffmanNode _left;
        HuffmanNode _right;
    }
}
