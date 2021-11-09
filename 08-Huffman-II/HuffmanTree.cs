using System;
using System.Collections.Generic;

namespace _08_Huffman_II
{
    class HuffmanTree
    {
        public override string ToString()
        {
            return _treeRoot.ToString();
        }

        public static HuffmanTree Create(long[] nodeValues)
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
