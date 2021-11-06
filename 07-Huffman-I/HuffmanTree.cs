using System;
using System.Collections.Generic;

//
// This class might seem redundant for now, but it will keep track of
// encoding (via traversal) in the next assignment.
//

namespace _07_Huffman_I
{
    class HuffmanTree
    {
        public override string ToString()
        {
            return _treeRoot.ToString();
        }

        public static HuffmanTree Construct(int[] nodeValues)
        {
            SortedSet<HuffmanNode> nodes = new SortedSet<HuffmanNode>();

            for (int i = 0; i < nodeValues.Length; i++)
            {
                if (nodeValues[i] > 0)
                {
                    nodes.Add(new HuffmanNode(i, nodeValues[i]));
                }
            }

            while (nodes.Count > 1)
            {
                var left = nodes.Min;
                nodes.Remove(left);
                var right = nodes.Min;
                nodes.Remove(right);

                if (!nodes.Add(new HuffmanNode(left, right))) Console.WriteLine("ALERT");
            }

            return new HuffmanTree(nodes.Min);
        }

        HuffmanTree(HuffmanNode root)
        {
            _treeRoot = root;
        }

        HuffmanNode _treeRoot;
    }
}
