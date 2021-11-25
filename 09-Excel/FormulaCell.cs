using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_Excel
{
    internal class FormulaCell : ICell
    {
        public FormulaCell(string formula)
        {
            _formula = formula;
        }

        public object GetBoxedValue()
        {
            return _formula;
        }

        public string GetValue()
        {
            return _formula;
        }

        public bool IsValid()
        {
            // This might not necessarily hold, but
            // an invalid formula shouldn't even be
            // saved as such.
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

        string _formula;
    }
}
