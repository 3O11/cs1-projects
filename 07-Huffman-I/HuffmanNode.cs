using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//
// I could've split leaf nodes and inner nodes into different classes
// which would inherit from an interface, but I think that would be
// unnecessary overengineering as there is (most likely) no way more than
// two kinds of nodes would ever be necessary.
//

namespace _07_Huffman_I
{
    class HuffmanNode : IComparable<HuffmanNode>
    {
        // Leaf node constructor
        public HuffmanNode(int value, int weight)
        {
            _value = value;
            _weight = weight;
            _left = null;
            _right = null;
        }

        // Inner node constructor
        public HuffmanNode(HuffmanNode left, HuffmanNode right)
        {
            _value = 2 * left.Value + 2 * (right.Value + 256);
            _weight = left.Weight + right.Weight;
            _left = left;
            _right = right;
        }

        public int CompareTo(HuffmanNode other)
        {
            if (other == null) return _weight;
            if (_weight - other._weight != 0) return _weight - other._weight;
            if (_value - other._value != 0) return _value - other._value;

            return 0;
        }

        public override string ToString()
        {
            return (_left == null) ? "*" + _value + ":" + _weight : _weight + " " + _left + " " + _right;
        }

        public int Weight
        {
            get => _weight;
        }

        public int Value
        {
            get => _value;
        }

        public HuffmanNode Left
        {
            get => _left;
        }

        public HuffmanNode Right
        {
            get => _right;
        }

        int _value;
        int _weight;
        HuffmanNode _left;
        HuffmanNode _right;
    }
}
