namespace _11_ExpressionEvaluator
{
    internal interface IExpression
    {
        int ArgCount { get; }
        int EmptyArgs { get; }
        void SetNextOperand(int value);
        bool CanEvaluate();
        bool Evaluate(out int result);
        string GetErrorMessage();
    }
}
