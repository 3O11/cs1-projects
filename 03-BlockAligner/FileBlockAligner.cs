using System.IO;

namespace _03_BlockAligner
{
    class FileBlockAligner : BlockAligner
    {
        public FileBlockAligner(int lineLength, string filename) : base(lineLength)
        {
            _out = new StreamWriter(filename);
        }

        public override void Finish()
        {
            base.Finish();

            _out.Close();
            _out.Dispose();
        }

        protected override void saveLine(string line)
        {
            _out.WriteLine(line);
        }

        StreamWriter _out;
    }
}
