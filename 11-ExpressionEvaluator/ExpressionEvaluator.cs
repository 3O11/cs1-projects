using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace _11_ExpressionEvaluator
{
    internal class ExpressionEvaluator
    {
        public static void Evaluate(TextReader input, TextWriter output)
        {
            int result = 0;
            Stack<IExpression> expressions = new Stack<IExpression>();

            string token;
            while((token = readNextToken(input)) != null)
            {
                switch(token)
                {
                    case "+":
                        expressions.Push(new AdditionExpression());
                        continue;
                    case "-":
                        expressions.Push(new SubtractionExpression());
                        continue;
                    case "*":
                        expressions.Push(new MultiplicationExpression());
                        continue;
                    case "/":
                        expressions.Push(new DivisionExpression());
                        continue;
                    case "~":
                        expressions.Push(new MinusExpression());
                        continue;
                }

                if (int.TryParse(token, out int tokenValue) && tokenValue >= 0)
                {
                    if (expressions.Count > 0)
                    {
                        expressions.Peek().SetNextOperand(tokenValue);
                        while (expressions.Peek().EmptyArgs == 0 && expressions.Count > 0)
                        {
                            if (expressions.Peek().Evaluate(out result))
                            {
                                expressions.Pop();
                                if (expressions.Count == 0) break;
                                expressions.Peek().SetNextOperand(result);
                            }
                            else
                            {
                                output.WriteLine(expressions.Peek().GetErrorMessage());
                                return;
                            }
                        }
                    }
                    else break;
                }
                else break;
            }

            if (expressions.Count == 0 && input.Peek() == -1)
            {
                output.WriteLine(result);
            }
            else
            {
                output.WriteLine("Format Error");
            }
        }

        static string readNextToken(TextReader input)
        {
            while (char.IsWhiteSpace((char)input.Peek())) input.Read();
            StringBuilder token = new StringBuilder();
            int next;
            while(input.Peek() != -1 && !char.IsWhiteSpace((char)input.Peek()))
            {
                token.Append((char)input.Read());
            }
            return token.Length > 0 ? token.ToString() : null;
        }
    }
}
