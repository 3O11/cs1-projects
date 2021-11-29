namespace _09_Excel
{

    internal class ErrorCell : ICell
    {
        public ErrorCell(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        public object GetBoxedValue()
        {
            return _errorMessage;
        }

        public string GetValue()
        {
            return _errorMessage;
        }

        public bool IsValid()
        {
            return false;
        }

        public bool IsValue()
        {
            return false;
        }

        CellType ICell.GetType()
        {
            return CellType.Error;
        }

        string _errorMessage;
    }
}
