namespace _03_BlockAligner {
    interface IWordProcessor {
        // Comsumes the next word and does all the neccesary work.
        void NextWord(string word);

        // If there is still work to be done after there is no more input,
        // complete it and dispose of any system resources.
        void Finish();
    }
}
