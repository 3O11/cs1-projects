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

        //public BitArray[] GetLookupTable()
        //{
        //    BitArray[] lookup = new BitArray[256];

        //    Stack<(HuffmanNode, BitArray)> dftStack = new Stack<(HuffmanNode, BitArray)>();
        //    dftStack.Push((_treeRoot, new BitArray(0)));

        //    while (dftStack.Count > 0)
        //    {
        //        var current = dftStack.Pop();

        //        if (current.Item1.IsLeaf())
        //        {
        //            lookup[current.Item1.Value] = current.Item2;
        //        }
        //        else
        //        {
        //            dftStack.Push((current.Item1.Left, current.Item2.ExtendBy(false)));
        //            dftStack.Push((current.Item1.Right, current.Item2.ExtendBy(true)));
        //        }
        //    }

        //    return lookup;
        //}

        public bool[][] GetLookupTable()
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

        HuffmanTree(HuffmanNode root)
        {
            _treeRoot = root;
        }

        HuffmanNode _treeRoot;
    }
}
