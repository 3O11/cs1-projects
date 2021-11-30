using System;
using System.Collections;
using System.Collections.Generic;

namespace _08_Huffman_II
{
    class HuffmanTree
    {
        public override string ToString()
        {
            return _treeRoot.ToString();
        }

        public static HuffmanTree CreateFromCounts(long[] nodeValues)
        {
            SortedSet<HuffmanNode> nodes = new SortedSet<HuffmanNode>();

            for (int i = 0; i < nodeValues.Length; i++)
            {
                if (nodeValues[i] > 0)
                {
                    nodes.Add(new HuffmanNode((ulong)i, (ulong)nodeValues[i]));
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

        public static HuffmanTree CreateFromEncoding(long[] encodedNodes)
        {
            
        }

        public byte[] Encode()
        {
            List<byte> bytes = new List<byte>();
            Stack<HuffmanNode> dftStack = new Stack<HuffmanNode>();
            if (_treeRoot != null) dftStack.Push(_treeRoot);

            while (dftStack.Count > 0)
            {
                HuffmanNode currentNode = dftStack.Pop();

                bytes.AddRange(currentNode.Encode());

                if (!currentNode.IsLeaf())
                {
                    dftStack.Push(currentNode.Right);
                    dftStack.Push(currentNode.Left);
                }
            }

            return bytes.ToArray();
        }

        public bool[][] GetEncodeLookupTable()
        {
            void recursiveGetLookup(bool[][] lookup, HuffmanNode node, List<bool> currentPath)
            {
                if (node.IsLeaf())
                {
                    lookup[node.Value] = currentPath.ToArray();
                }
                else
                {
                    List<bool> leftPath = new List<bool>(currentPath);
                    leftPath.Add(false);
                    recursiveGetLookup(lookup, node.Left, leftPath);

                    List<bool> rightPath = new List<bool>(currentPath);
                    rightPath.Add(true);
                    recursiveGetLookup(lookup, node.Right, rightPath);
                }
            }

            bool[][] lookup = new bool[256][];

            if (_treeRoot != null) recursiveGetLookup(lookup, _treeRoot, new List<bool>());

            return lookup;
        }

        public Dictionary<int, byte> GetDecodeLookupTable()
        {
            void recursiveGetLookup(Dictionary<int, byte> lookup, HuffmanNode node, int currentPath)
            {
                if (node.IsLeaf())
                {
                    lookup[currentPath] = (byte)node.Value;
                }
                else
                {
                    recursiveGetLookup(lookup, node.Left, currentPath << 1);
                    recursiveGetLookup(lookup, node.Right, 1 | currentPath << 1);
                }
            }

            var lookup = new Dictionary<int, byte>();

            if (_treeRoot != null) recursiveGetLookup(lookup, _treeRoot, 0);

            return lookup;
        }

        HuffmanTree(HuffmanNode root)
        {
            _treeRoot = root;
        }

        HuffmanNode _treeRoot;
    }
}
