using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_Excel
{
    enum CellType
    {
        Int,
        Formula,
        Error,
    }

    internal interface ICell
    {
        bool IsValue();

        bool IsValid();

        object GetBoxedValue();

        CellType GetType();
    }
}
