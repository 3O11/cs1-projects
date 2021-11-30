using System;
using System.Collections.Generic;
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
                    sheet.SetCell(CellUtils.CreateCoord(colNum, rowNum), CellUtils.ParseCell(cells[colNum - 1]));
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
                    ICell cell = sheet.GetRawCell(CellUtils.CreateCoord(j, i));
                    if (cell == null) output.Write("[]");
                    else output.Write(cell.GetBoxedValue());

                    if (j < rowLength) output.Write(" ");
                }
                output.WriteLine();
            }
        }

        public static void Evaluate(Sheet sheet)
        {
            var visited = new HashSet<long>();
            Stack<long> evalStack = new Stack<long>();

            int rowCount = sheet.GetRowCount();
            for (int i = 1; i <= rowCount; i++)
            {
                int rowLength = sheet.GetRowLength(i);
                for (int j = 1; j <= rowLength; j++)
                {
                    long index = CellUtils.CreateCoord(j, i);
                    if (sheet.GetCell(index).GetType() == CellType.Formula)
                    {
                        evalCell(index, sheet, visited, evalStack);
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

        // Yes, I'm aware that GOTO isn't a very nice thing to use, especially in C#,
        // but I wanted to be able to jump out of a loop from a switch statement
        // without having to have a separate bool and additional checks for each loop iteration.
        static void evalCell(long index, Sheet sheet, HashSet<long> visited, Stack<long> evalStack)
        {
            evalStack.Push(index);
            while (evalStack.Count > 0)
            {
                var evalIndex = evalStack.Pop();
                visited.Add(evalIndex);
                ICell evalCell = sheet.GetCell(evalIndex);
                if (evalCell.GetType() != CellType.Formula) continue;

                var rightIndex = ((FormulaCell)evalCell).GetRightOperand();
                ICell rightCell = sheet.GetCell(rightIndex);
                int rightValue = 0;

                switch (rightCell.GetType())
                {
                    case CellType.Formula:
                        evalStack.Push(evalIndex);
                        evalStack.Push(rightIndex);
                        if (visited.Contains(rightIndex)) goto Cycle;
                        continue;
                    case CellType.Error:
                        sheet.SetCell(evalIndex, CellUtils.GetErrorCell());
                        continue;
                    case CellType.Int:
                        rightValue = ((IntCell)rightCell).GetValue();
                        break;
                    default:
                        sheet.SetCell(evalIndex, new ErrorCell("#HOW_DID_WE_GET_HERE?"));
                        break;
                }

                var leftIndex = ((FormulaCell)evalCell).GetLeftOperand();
                ICell leftCell = sheet.GetCell(leftIndex);
                int leftValue = 0;

                switch (leftCell.GetType())
                {
                    case CellType.Formula:
                        evalStack.Push(evalIndex);
                        evalStack.Push(leftIndex);
                        if (visited.Contains(leftIndex)) goto Cycle;
                        continue;
                    case CellType.Error:
                        sheet.SetCell(evalIndex, CellUtils.GetErrorCell());
                        continue;
                    case CellType.Int:
                        leftValue = ((IntCell)leftCell).GetValue();
                        break;
                    default:
                        sheet.SetCell(evalIndex, new ErrorCell("#HOW_DID_WE_GET_HERE?"));
                        break;
                }

                updateSheet(sheet, evalIndex, leftValue, rightValue, ((FormulaCell)evalCell).GetOperator());
            }

            Cycle:
            while (evalStack.Count > 0)
            {
                var cycleIndex = evalStack.Pop();
                sheet.SetCell(cycleIndex, CellUtils.GetCycleErrorCell());
            }
        }

        static void updateSheet(Sheet sheet, long index, int left, int right, char op)
        {
            switch (op)
            {
                case '+':
                    sheet.SetCell(index, new IntCell(left + right));
                    break;
                case '-':
                    sheet.SetCell(index, new IntCell(left - right));
                    break;
                case '*':
                    sheet.SetCell(index, new IntCell(left * right));
                    break;
                case '/':
                    if (right == 0) sheet.SetCell(index, CellUtils.GetDiv0ErrorCell());
                    else sheet.SetCell(index, new IntCell(left / right));
                    break;
                default:
                    sheet.SetCell(index, new ErrorCell("#HOW_DID_WE_GET_HERE?"));
                    break;
            }
        }
    }
}
