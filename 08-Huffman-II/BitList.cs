//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

////
//// - This List will never shrink automatically!
////

//namespace _08_Huffman_II
//{
//    class BitList
//    {
//        public BitList()
//        {
//            Capacity = 8;
//            _array = new BitArray(Capacity);
//        }

//        void Add(bool bit)
//        {
//            if (Count < _array.Length)
//            {
//                _array[Count] = bit;
//                ++Count;
//            }
//            else
//            {
//                Capacity *= 2;
//                BitArray old = _array;
//                _array = new BitArray(Capacity);
//                for (int i = 0; i < old.Length; i++) _array[i] = old[i];

//                _array[Count] = bit;
//                ++Count;
//            }
//        }

//        void AddRange(BitArray array)
//        {
//            if (Count + array.Length < _array.Length)
//            {
//                for (int i = 0; i < array.Length; i++)
//                {
//                    _array[Count] = array[i];
//                    ++Count;
//                }
//            }
//            else
//            {
//                Capacity += array.Length;
//                BitArray old = _array;
//                _array = new BitArray(Capacity);
//                for (int i = 0; i < old.Length; i++) _array[i] = old[i];

//                for (int i = 0; i < array.Length; i++)
//                {
//                    _array[Count] = array[i];
//                    ++Count;
//                }
//            }
//        }

//        byte[] PopBytesFront()
//        {
//            return new byte[0];
//        }

//        byte PopByteFront()
//        {
//            return new byte();
//        }

//        public int Count { get; private set; }
//        public int Capacity { get; private set; }

//        BitArray _array;

//    }
//}
