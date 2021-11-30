using System;
using System.Text.RegularExpressions;

namespace _09_Excel
{
    static class CellUtils
    {
        public static ICell GetCycleErrorCell()
        {
            return _cycleError;
        }

        public static ICell GetDiv0ErrorCell()
        {
            return _div0Error;
        }

        public static ICell GetInvvalErrorCell()
        {
            return _invvalError;
        }

        public static ICell GetFormulaErrorCell()
        {
            return _formulaError;
        }

        public static ICell GetMissopErrorCell()
        {
            return _missopError;
        }

        public static ICell GetErrorCell()
        {
            return _genericError;
        }

        public static ICell ParseFormulaCell(string formula)
        {
            string[] operands = formula.Substring(1).Split(new char[] {'+', '-', '*', '/'});
            if (operands.Length < 2) return GetMissopErrorCell();
            if (operands.Length > 2) return GetFormulaErrorCell();

            long left = ParseIndex(operands[0]);
            long right = ParseIndex(operands[1]);
            if (left == -1 || right == -1) return GetFormulaErrorCell();
            else return new FormulaCell(left, right, formula[operands[0].Length + 1]);
        }

        public static ICell ParseCell(string cellValue)
        {
            if (cellValue == "[]" || cellValue == "") return null;

            if (cellValue[0] == '=') return ParseFormulaCell(cellValue);

            if (int.TryParse(cellValue, out int num)) return new IntCell(num);

            return GetInvvalErrorCell();
        }

        public static long ParseIndex(string cellIndex)
        {
            Match m = _indexRegex.Match(cellIndex);
            if (!m.Success)
            {
                return -1;
            }
            else
            {
                return CreateCoord(parseCol(m.Groups["col"].Value), int.Parse(m.Groups["row"].Value));
            }
        }

        static int parseCol(string col)
        {
            int colNum = 0;

            foreach (int ch in col)
            {
                colNum = (26 * colNum) + (ch - (int)'A' + 1);
            }

            return colNum;
        }

        public static long CreateCoord(int col, int row)
        {
            return ((long)col << 32) | (long)row;
        }

        static readonly Regex _indexRegex = new Regex(@"^(?<col>[A-Z]+)(?<row>[0-9]+)$");

        static ICell _cycleError = new ErrorCell("#CYCLE");
        static ICell _div0Error = new ErrorCell("#DIV0");
        static ICell _invvalError = new ErrorCell("#INVVAL");
        static ICell _formulaError = new ErrorCell("#FORMULA");
        static ICell _missopError = new ErrorCell("#MISSOP");
        static ICell _genericError = new ErrorCell("#ERROR");
    }
}
