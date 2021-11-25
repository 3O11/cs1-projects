using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_Excel
{
    internal class Sheet
    {
        public Sheet()
        {
            _cells = new Dictionary<Tuple<int, int>, ICell>();
        }

        public ICell GetCell(Tuple<int, int> cellIndex)
        {
            if (_cells.ContainsKey(cellIndex))
            {
                return _cells[cellIndex];
            }

            return _emptyCell;
        }

        public void SetCell(Tuple<int, int> cellIndex, ICell cell)
        {
            _cells[cellIndex] = cell;
        }

        Dictionary<Tuple<int, int>, ICell> _cells;
        ICell _emptyCell = new IntCell(0);
    }
}
