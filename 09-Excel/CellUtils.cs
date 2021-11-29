using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace _09_Excel
{
    static class CellUtils
    {
        public static ICell ParseFormulaCell(string formula)
        {
            Match m = _formulaRegex.Match(formula);
            if (!m.Success)
            {
                return new ErrorCell("#MISSOP");
            }
            else
            {
                var left = ParseIndex(m.Groups["left"].Value);
                var right = ParseIndex(m.Groups["right"].Value);

                if (left == null || right == null) return new ErrorCell("#FORMULA");
                else return new FormulaCell(left, right, m.Groups["op"].Value[0]);
            }
        }

        public static ICell ParseCell(string cellValue)
        {
            if (cellValue == "[]" || cellValue == "") return null;

            if (cellValue[0] == '=') return ParseFormulaCell(cellValue);

            if (int.TryParse(cellValue, out int num)) return new IntCell(num);

            return new ErrorCell("#INVVAL");
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
                colNum = (26 * colNum) + (ch - (int)'A');
            }

            return colNum;
        }

        static Regex _indexRegex = new Regex(@"^(?<col>[A-Z]+)(?<row>[0-9])$", RegexOptions.Compiled);
        static Regex _formulaRegex = new Regex(@"^=(?<left>[a-zA-Z0-9]+)(?<op>[+\-*/])(?<right>[a-zA-Z0-9]+)$", RegexOptions.Compiled);
    }
}
