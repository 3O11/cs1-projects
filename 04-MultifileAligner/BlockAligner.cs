using System.Collections.Generic;
using System.Text;
using System.IO;

namespace _03_BlockAligner {
    abstract class BlockAligner : IWordProcessor {
        public BlockAligner(int lineLength) {
            _lineLength = lineLength;
        }

        public void NextWord(string word) {
            if (!fitsOnLine(word)) {
                if (_wordBuffer.Count != 0) {
                    saveLine(flushBuffer(false) + LineEnding);
                }
            }

            if (word == "") {
                if (!_isSecondNewline) {
                    _isSecondNewline = true;
                    return;
                }

                if (_allowNewParagraph && _isSecondNewline) {
                    if (_wordBuffer.Count > 0) saveLine(flushBuffer(true) + LineEnding);
                    _allowNewParagraph = false;
                    _isSecondNewline = false;
                    _firstParagraph = false;
                    return;
                }
                else {
                    return;
                }
            }

            if (!_allowNewParagraph && !_firstParagraph) saveLine(LineEnding);

            _wordBuffer.Add(word);
            _bufferWordsLength += word.Length;
            _allowNewParagraph = true;
            _isSecondNewline = false;
        }

        public virtual void Finish() {
            if (_wordBuffer.Count > 0) saveLine(flushBuffer(true) + LineEnding);
        }

        public virtual void SetProperties(Dictionary<string, object> properties) {
            foreach (var pair in properties) {
                switch(pair.Key) {
                    case "Space":
                        Space = (char)properties["Space"];
                        break;
                    case "LineEnding":
                        LineEnding = (string)properties["LineEnding"];
                        break;
                    default:
                        break;
                }
            }
        }

        public string LineEnding { private get; set; }
        public char Space { private get; set; }

        protected abstract void saveLine(string line);

        private string flushBuffer(bool endParagraph) {
            StringBuilder line = new StringBuilder();

            if (endParagraph) {
                for (int i = 0; i < _wordBuffer.Count; i++) {
                    line.Append(_wordBuffer[i]);
                    if (i != _wordBuffer.Count - 1) line.Append(Space);
                }
            }
            else if (_wordBuffer.Count == 1) {
                line.Append(_wordBuffer[0]);
            }
            else {
                string space = new string(Space, (_lineLength - _bufferWordsLength) / (_wordBuffer.Count - 1));
                int additionalSpaces = (_lineLength - _bufferWordsLength) % (_wordBuffer.Count - 1);

                for (int i = 0; i < _wordBuffer.Count; i++) {
                    line.Append(_wordBuffer[i]);
                    if (i != _wordBuffer.Count - 1) line.Append(space);
                    if (additionalSpaces --> 0) line.Append(Space);
                }
            }

            _wordBuffer.Clear();
            _bufferWordsLength = 0;

            return line.ToString();
        }

        private bool fitsOnLine(string word) {
            return word.Length + _bufferWordsLength + _wordBuffer.Count <= _lineLength;
        }

        int _lineLength = 0;
        int _bufferWordsLength = 0;
        bool _firstParagraph = true;
        bool _allowNewParagraph = false;
        bool _isSecondNewline = false;
        List<string> _wordBuffer = new List<string>();
    }
}
