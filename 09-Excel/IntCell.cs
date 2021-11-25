using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_Excel
{
    internal class IntCell : ICell
    {
        public IntCell(int value)
        {
            _value = value;
        }

        public object GetBoxedValue()
        {
            return _value;
        }

        public int GetValue()
        {
            return _value;
        }

        public bool IsValid()
        {
            return true;
        }

        public bool IsValue()
        {
            return true;
        }

        CellType ICell.GetType()
        {
            return CellType.Int;
        }

        int _value;
    }
}
