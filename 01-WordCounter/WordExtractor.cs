using System;
using System.IO;
using System.Text;

namespace _01_WordCounter {
    class WordExtractor : IDisposable {
        public WordExtractor(string filename) {
            _fileReader = new StreamReader(filename);
        }

        public void Dispose() {
            _fileReader.Close();
            _fileReader.Dispose();
        }

        public string GetNextRawWord() {
            StringBuilder wordBuffer = new StringBuilder();
            char next;

            while (_fileReader.Peek() >= 0) {
                next = (char)_fileReader.Read();
                if (!char.IsWhiteSpace(next)) {
                    wordBuffer.Append(next);
                    break;
                }
            }

            while (_fileReader.Peek() >= 0) {
                next = (char)_fileReader.Read();

                if (char.IsWhiteSpace(next)) break;

                wordBuffer.Append(next);
            }

            if (wordBuffer.Length > 0) return wordBuffer.ToString();

            return null;
        }

        StreamReader _fileReader;
    }
}
