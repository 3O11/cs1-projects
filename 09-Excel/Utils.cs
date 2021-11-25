using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace _09_Excel
{
    public static class Utils
    {
        public static ICell CreateCell(string value)
        {

        }

        public static Tuple<int, int> ParseCellIndex(string cellIndex)
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

        }

        static Regex _indexRegex = new Regex(@"^(?<col>[A-Z]+)(?<row>[0-9])$", RegexOptions.Compiled);
    }
}
