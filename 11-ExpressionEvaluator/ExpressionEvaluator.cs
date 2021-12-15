using System;
using System.Text;
using System.IO;

namespace _11_ExpressionEvaluator
{
    internal class ExpressionEvaluator
    {
        public static void Evaluate(TextReader input, TextWriter output)
        {
            string token;
            while((token = readNextToken(input)) != null)
            {
                switch(token)
                {
                    case "+":
                        break;
                    case "-":
                        break;
                    case "*":
                        break;
                    case "/":
                        break;
                    default:
                        break;
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
