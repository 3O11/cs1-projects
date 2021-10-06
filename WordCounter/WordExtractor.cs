using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WordCounter {
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

            while (char.IsWhiteSpace(next = (char)_fileReader.Read()) && !_fileReader.EndOfStream);
            if (!char.IsWhiteSpace(next)) wordBuffer.Append(next);
            
            while (!char.IsWhiteSpace(next = (char)_fileReader.Read()) && !_fileReader.EndOfStream) {
                wordBuffer.Append(next);
            }

            if (wordBuffer.Length == 0) return null;

            return wordBuffer.ToString();
        }

        StreamReader _fileReader;
    }
}
