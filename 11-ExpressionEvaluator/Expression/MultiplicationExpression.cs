using System;

namespace _11_ExpressionEvaluator
{
    internal class MultiplicationExpression : BinaryExpression
    {
        public override bool Evaluate(out int result)
        {
            result = 0;

            if (EmptyArgs != 0)
            {
                _message = "Format Error";
                return false;
            }

            try
            {
                result = checked(_op1 * _op2);
            }
            catch (OverflowException e)
            {
                _message = "Overflow Error";
                return false;
            }

            return true;
        }
    }
}
