using System;

namespace _09_Excel
{
    internal class FormulaCell : ICell
    {
        public FormulaCell(long left, long right, char op)
        {
            _leftOperand = left;
            _rightOperand = right;
            _operator = op;
        }

        public object GetBoxedValue()
        {
            return _operator;
        }

        public long GetLeftOperand()
        {
            return _leftOperand;
        }

        public long GetRightOperand()
        {
            return _rightOperand;
        }

        public char GetOperator()
        {
            return _operator;
        }

        public bool IsValid()
        {
            // This might not necessarily hold, but
            // an invalid formula shouldn't even be
            // saved as being valid.
            return true;
        }

        public bool IsValue()
        {
            return false;
        }

        CellType ICell.GetType()
        {
            return CellType.Formula;
        }

        long _leftOperand;
        long _rightOperand;
        char _operator;
    }
}
