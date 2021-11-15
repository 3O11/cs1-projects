using System;
using System.Collections.Generic;
using System.Text;

namespace cs_prg_6 {
    public class Tree : Node<DataPair>, IComparable<Tree> {
        public Tree() {}
        
        public Tree(DataPair head) {
            this.Data = head;
        }

        public bool IsLeaf() {
            if (this.Left == null && this.Right == null) return true;
            return false;
        }

        public int Weight() {
            return this.Data.Value;
        }

        public string buildPrefix() {
            StringBuilder result = new StringBuilder();
            if (IsLeaf()) result.Append("*" + this.Data.Key + ":" + this.Data.Value + " ");
            else {
                result.Append(this.Data.Value + " ");

                Tree leftChild = (Tree)Left;
                Tree rightChild = (Tree)Right;

                result.Append(leftChild.buildPrefix());
                result.Append(rightChild.buildPrefix());
            }

            return result.ToString();
        }

        public int CompareTo(Tree tree) {
            if (this.Data.Value.CompareTo(tree.Data.Value) != 0) return this.Data.Value.CompareTo(tree.Data.Value);
            if (this.Data.Key.CompareTo(tree.Data.Key) != 0) return this.Data.Key.CompareTo(tree.Data.Key);
            return 0;
        }

        //This is why I'm starting to think I'm stupid ...
        //private Tree m_Head;
    }
}
