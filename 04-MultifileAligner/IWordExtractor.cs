namespace _03_BlockAligner {
    interface IWordExtractor {
        // This method returns the next word,
        // which is stripped of any whitespaces.
        // It also never returns an empty string.
        // In case it comes to the end of the file
        // with an empty buffer, it return null.
        string GetNextWord();

        // This method returns a string containing the next word.
        // In case it finds an empty line, it returns an empty string.
        // When the reader comes to the end of the file, it returns null.
        string GetNextRawWord();

        // If there is still work to be done after there is no more input,
        // complete it and dispose of any system resources.
        void Finish();
    }
}
