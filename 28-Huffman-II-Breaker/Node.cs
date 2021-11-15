using System;
using System.Collections.Generic;
using System.Text;

namespace cs_prg_6 {
    public class Node<T> {
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public T Data;
    }
}
