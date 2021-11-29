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
