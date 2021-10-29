using System;
using System.IO;
using System.Text;

namespace _04_BlockAligner {
    class WordExtractor : IWordExtractor {
        public WordExtractor(string filename) {
            _fileReader = new StreamReader(filename);
        }

        public string GetNextWord() {
            string next = GetNextRawWord();
            while (next == "" && next != null) {
                next = GetNextRawWord();
            }

            return next;
        }

        public string GetNextRawWord() {
            StringBuilder wordBuffer = new StringBuilder();
            char next;

            while (_fileReader.Peek() >= 0) {
                if (_nextReturnEmpty) {
                    _nextReturnEmpty = false;
                    return "";
                }

                next = (char)_fileReader.Read();

                if (next == '\n') return "";

                if (!char.IsWhiteSpace(next)) {
                    wordBuffer.Append(next);
                    break;
                }
            }

            while (_fileReader.Peek() >= 0) {
                next = (char)_fileReader.Read();

                if (char.IsWhiteSpace(next)) {
                    if (next == '\n') _nextReturnEmpty = true;
                    break;
                }

                wordBuffer.Append(next);
            }

            if (wordBuffer.Length > 0) return wordBuffer.ToString();

            return null;
        }
        public void Finish() {
            _fileReader.Close();
            _fileReader.Dispose();
        }

        StreamReader _fileReader;
        bool _nextReturnEmpty;
    }
}
