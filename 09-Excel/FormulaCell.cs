using System;

namespace _09_Excel
{
    internal class FormulaCell : ICell
    {
        public FormulaCell(Tuple<int,int> left, Tuple<int, int> right, char op)
        {
            _leftOperand = left;
            _rightOperand = right;
            _operator = op;
        }

        public object GetBoxedValue()
        {
            return _operator;
        }

        public Tuple<int, int> GetLeftOperand()
        {
            return _leftOperand;
        }

        public Tuple<int, int> GetRightOperand()
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

        Tuple<int, int> _leftOperand;
        Tuple<int, int> _rightOperand;
        char _operator;
    }
}
