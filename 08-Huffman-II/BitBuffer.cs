//using System;
//using System.Collections;
//using System.Collections.Generic;

////
//// - The size of the buffer is specified in bytes, but the possible overflow difference is returned in bits!
//// - The Push method returns the size of possible overflow over the size of the buffer.
////

//namespace _08_Huffman_II
//{
//    class BitBuffer
//    {
//        public BitBuffer(int size)
//        {
//            _buffer = new BitArray(size * 8);
//            _bufferOffset = 0;
//            _byteCount = size;
//        }

//        public bool HasSpace(int size)
//        {
//            return _bufferOffset + size <= _buffer.Length;
//        }

//        public int FreeSpace()
//        {
//            return _buffer.Length - (_bufferOffset - 1);
//        }

//        // I hope this variable soup is correct
//        public int Push(BitArray array, int offset = 0)
//        {
//            int i = 0;
//            while (i + offset < array.Length && i + _bufferOffset < _buffer.Length)
//            {
//                _buffer[_bufferOffset + i] = array[i + offset];
//                ++i;
//            }

//            _bufferOffset += array.Length - offset;
//            return array.Length - (i + offset + 1);
//        }

//        public byte[] GetContent()
//        {
//            int filledBytes = _bufferOffset / 8 + (_bufferOffset / 8 == 0 ? 0 : 1);

//            byte[] bytes = new byte[filledBytes];
//            if (filledBytes == _byteCount) _buffer.CopyTo(bytes, 0);

//            BitArray a = new BitArray();

            

//            return bytes;
//        }

//        public void Clear()
//        {
//            // I could avoid the actual overwrite, but then there would be problems
//            // with calling GetContent() on a buffer which isn't completely full.
//            for (int i = 0; i < _byteCount * 8; i++)
//            {
//                _buffer[i] = false;
//            }
//            _bufferOffset = 0;
//        }

//        BitArray _buffer;
//        int _bufferOffset;
//        int _byteCount;
//    }
//}
