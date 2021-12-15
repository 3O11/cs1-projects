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
                            if(expressions.Peek().Evaluate(out int propagate))
                            {
                                expressions.Pop();

                                if (expressions.Count == 0)
                                {
                                    if (input.Peek() == -1)
                                    {
                                        output.WriteLine(propagate);
                                        return;
                                    }

                                    output.WriteLine("Format Error");
                                    return;
                                }

                                expressions.Peek().SetNextOperand(propagate);
                            }
                            else
                            {
                                output.WriteLine(expressions.Peek().GetErrorMessage());
                                return;
                            }
                        }
                    }
                    else
                    {
                        output.WriteLine(token);
                        return;
                    }
                }
                else
                {
                    output.WriteLine("Format Error");
                    return;
                }
            }
        }

        static string readNextToken(TextReader input)
        {
            StringBuilder token = new StringBuilder();
            int next;
            while((next = input.Read()) != -1 && !char.IsWhiteSpace((char)next))
            {
                token.Append((char)next);
            }
            return token.Length > 0 ? token.ToString() : null;
        }
    }
}
