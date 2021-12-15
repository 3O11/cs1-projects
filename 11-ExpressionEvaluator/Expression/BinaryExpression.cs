using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_ExpressionEvaluator
{
    internal abstract class BinaryExpression : IExpression
    {
        public BinaryExpression()
        {
            ArgCount = 2;
            EmptyArgs = 2;
        }

        public int ArgCount { get; private set; }
        public int EmptyArgs { get; private set; }

        public bool CanEvaluate()
        {
            return EmptyArgs == 0;
        }

        public abstract bool Evaluate(out int result);

        public void SetNextOperand(int value)
        {
            if (EmptyArgs == 0) throw new NotSupportedException();

            if (EmptyArgs == 2)
            {
                _op1 = value;
            }
            else if (EmptyArgs == 1)
            {
                _op2 = value;

            }
            EmptyArgs--;
        }

        public string GetErrorMessage()
        {
            return _message;
        }

        protected int _op1;
        protected int _op2;
        protected string _message;
    }
}
