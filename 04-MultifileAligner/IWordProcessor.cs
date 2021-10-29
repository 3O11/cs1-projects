using System.Collections.Generic;

namespace _04_BlockAligner {
    interface IWordProcessor {
        // Comsumes the next word and does all the neccesary work.
        void NextWord(string word);

        // If there is still work to be done after there is no more input,
        // complete it and dispose of any system resources.
        void Finish();

        // Update aligner state with new settings.
        // In case the properties are invalid, they just get ignored, nothing is
        // thrown.
        void SetProperties(Dictionary<string, object> properties);
    }
}
