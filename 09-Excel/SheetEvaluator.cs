using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _09_Excel
{
    internal class SheetEvaluator
    {
        public static Sheet Load(TextReader input)
        {
            Sheet sheet = new Sheet();

            int rowNum = 1;
            string line;
            while((line = input.ReadLine()) != null)
            {
                string[] cells = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                sheet.SetRowLength(rowNum, cells.Length);
                int colNum = 1;
                while(colNum - 1 < cells.Length)
                {
                    sheet.SetCell(new Tuple<int, int>(colNum, rowNum), CellUtils.ParseCell(cells[colNum - 1]));
                    ++colNum;
                }
                ++rowNum;
            }

            return sheet;
        }

        public static void Save(TextWriter output, Sheet sheet)
        {
            int rowCount = sheet.GetRowCount();
            for (int i = 1; i <= rowCount; i++)
            {
                int rowLength = sheet.GetRowLength(i);
                for (int j = 1; j <= rowLength; j++)
                {
                    ICell cell = sheet.GetCell(new Tuple<int, int>(j, i));
                    if (cell == null) output.Write("[]");
                    else output.Write(cell.GetBoxedValue());

                    if (j < rowLength) output.Write(" ");
                }
                output.WriteLine();
            }
        }

        public static void Evaluate(Sheet sheet)
        {
            var evalStack = new Stack<FormulaCell>();
            var visited = new HashSet<Tuple<int, int>>();

            int rowCount = sheet.GetRowCount();
            for (int i = 1; i <= rowCount; i++)
            {
                int rowLength = sheet.GetRowLength(i);
                for (int j = 1; j <= rowLength; j++)
                {
                    var index = new Tuple<int, int>(j, i);
                    var cell = sheet.GetCell(index);
                    if (cell.GetType() == CellType.Formula)
                    {
                        // TODO: Finish traversal
                    }
                }
            }
        }

        public static void Evaluate(TextReader input, TextWriter output)
        {
            Sheet sheet = Load(input);
            Evaluate(sheet);
            Save(output, sheet);
        }
    }
}
