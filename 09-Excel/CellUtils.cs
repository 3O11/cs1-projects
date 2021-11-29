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
            Match m = _formulaRegex.Match(formula);
            if (!m.Success)
            {
                return GetMissopErrorCell();
            }
            else
            {
                var left = ParseIndex(m.Groups["left"].Value);
                var right = ParseIndex(m.Groups["right"].Value);
                if (left == null || right == null) return GetFormulaErrorCell();
                else return new FormulaCell(left, right, m.Groups["op"].Value[0]);
            }
        }

        public static ICell ParseCell(string cellValue)
        {
            if (cellValue == "[]" || cellValue == "") return null;

            if (cellValue[0] == '=') return ParseFormulaCell(cellValue);

            if (int.TryParse(cellValue, out int num)) return new IntCell(num);

            return GetInvvalErrorCell();
        }

        public static Tuple<int, int> ParseIndex(string cellIndex)
        {
            Match m = _indexRegex.Match(cellIndex);
            if (!m.Success)
            {
                return null;
            }
            else
            {
                return new Tuple<int, int>(parseCol(m.Groups["col"].Value), int.Parse(m.Groups["row"].Value));
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

        static Regex _indexRegex = new Regex(@"^(?<col>[A-Z]+)(?<row>[0-9])$", RegexOptions.Compiled);
        static Regex _formulaRegex = new Regex(@"^=(?<left>\w+)(?<op>[+\-*/])(?<right>\w+)$", RegexOptions.Compiled);

        static ICell _cycleError = new ErrorCell("#CYCLE");
        static ICell _div0Error = new ErrorCell("#DIV0");
        static ICell _invvalError = new ErrorCell("#INVVAL");
        static ICell _formulaError = new ErrorCell("#FORMULA");
        static ICell _missopError = new ErrorCell("#MISSOP");
        static ICell _genericError = new ErrorCell("#ERROR");
    }
}
