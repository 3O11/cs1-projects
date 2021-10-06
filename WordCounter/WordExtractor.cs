using System;
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
            int next;
            bool wordStarted = false;

            while ((next = _fileReader.Read()) != -1) {
                if (!char.IsWhiteSpace((char)next) && !wordStarted) {
                    wordStarted = true;
                    wordBuffer.Append((char)next);
                }
                else if (char.IsWhiteSpace((char)next) && wordStarted) {
                    break;
                }
                else {
                    wordBuffer.Append((char)next);
                }
            }

            if (wordBuffer.Length > 0) return wordBuffer.ToString();

            return null;
        }

        StreamReader _fileReader;
    }
}
