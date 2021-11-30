using System;
using System.Collections.Generic;

namespace _09_Excel
{
    internal class Sheet
    {
        public Sheet()
        {
            _cells = new Dictionary<long, ICell>();
            _rowLengths = new List<int>();
        }

        public ICell GetRawCell(long cellIndex)
        {
            if (_cells.ContainsKey(cellIndex))
            {
                return _cells[cellIndex];
            }

            return null;
        }

        public ICell GetCell(long cellIndex)
        {
            if (_cells.ContainsKey(cellIndex))
            {
                return _cells[cellIndex];
            }

            return _emptyCell;
        }

        public void SetCell(long cellIndex, ICell cell)
        {
            // This should never happen, checking this should be the caller's
            // responsibility.
            //if (cellIndex == null)
            //{
            //    return;
            //}

            if (cell == null)
            {
                _cells.Remove(cellIndex);
                return;
            }

            _cells[cellIndex] = cell;
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

        Dictionary<long, ICell> _cells;
        List<int> _rowLengths;
        ICell _emptyCell = new IntCell(0);
    }
}
