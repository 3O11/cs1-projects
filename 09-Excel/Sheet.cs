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
            _rowLengths = new List<int>();
        }

        public ICell GetCell(Tuple<int, int> cellIndex)
        {
            if (_cells.ContainsKey(cellIndex))
            {
                return _cells[cellIndex];
            }

            return _emptyCell;
        }

        public ICell GetCell(string cellIndex)
        {
            var parsedIndex = CellUtils.ParseIndex(cellIndex);

            // This should never happen, but I'm checking so that it
            // crashes when trying to access later instead of doing
            // weird things.
            if (parsedIndex == null)
            {
                return null;
            }

            return GetCell(parsedIndex);
        }

        public void SetCell(Tuple<int, int> cellIndex, ICell cell)
        {
            // This should never happen, checking this should be the caller's
            // responsibility.
            //if (cellIndex == null)
            //{
            //    return;
            //}

            _cells[cellIndex] = cell;
        }

        public void SetCell(string cellIndex, ICell cell)
        {
            SetCell(CellUtils.ParseIndex(cellIndex), cell);
        }

        public void SetRowLength(int row, int length)
        {
            --row; // The input is expected to be numbered from one
            while (_rowLengths.Count <= row) _rowLengths.Add(0);
            _rowLengths[row] = length;
        }

        public int GetRowLength(int row)
        {
            --row; // The input is expected to be numbered from one
            if (_rowLengths.Count <= row) return 0;
            else return _rowLengths[row];
        }

        public int GetRowCount()
        {
            return _rowLengths.Count;
        }

        Dictionary<Tuple<int, int>, ICell> _cells;
        List<int> _rowLengths;
        ICell _emptyCell = new IntCell(0);
    }
}
