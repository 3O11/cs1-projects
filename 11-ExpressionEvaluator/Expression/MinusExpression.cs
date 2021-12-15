using System;

namespace _11_ExpressionEvaluator
{
    internal class MinusExpression : UnaryExpression
    {
        public override bool Evaluate(out int result)
        {
            result = 0;

            if (EmptyArgs != 0)
            {
                _message = "Format Error";
                return false;
            }

            result = -_op1;
            return true;
        }
    }
}
