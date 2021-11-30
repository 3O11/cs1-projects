#define RECODEX

using System;
using System.IO;

namespace _09_Excel
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if RECODEX
            if (args.Length != 2)
            {
                Console.WriteLine("Argument Error");
                return;
            }

            StreamReader input;
            StreamWriter output;
            try
            {
                input = new StreamReader(args[0]);
                output = new StreamWriter(args[1]);
            }
            catch
            {
                Console.WriteLine("File Error");
                return;
            }

            SheetEvaluator.Evaluate(input, output);

            input.Close();
            input.Dispose();
            output.Close();
            output.Dispose();
#else
            StringReader strr;
            StringWriter strw; ;



            strr = new StringReader(
                "[] 3 =B1*A2\n" +
                "19 =C1+C2 42\n" +
                "car\n" +
                "=B2/A1 =A1-B4 =C2+A4\n" +
                "=error =A1+bus\n"
            );
            strw = new StringWriter();
            SheetEvaluator.Evaluate(strr, strw);
            Console.WriteLine(strw);

            Console.WriteLine("======================");

            strr = new StringReader(
                "[] = =D4-E1 [] 23\n" +
                "=A1+E1 [] =A2*B3 [] [] nibba\n" +
                "=neco+D3 6 [] =G3+E1 [] =C2/A1 =C1+C1\n" +
                "[] [] [] =D3+E1\n" +
                "[] 34 [][] =D4+E1 [] []\n"
            );
            strw = new StringWriter();
            SheetEvaluator.Evaluate(strr, strw);
            Console.WriteLine(strw);

            Console.WriteLine("======================");

            strr = new StringReader(
                "[] = =D4-E1 [] 23\n" +
                "=A1+E1 [] =A2*B3 [] [] nibba\n" +
                "=neco+D3 6 [] =G3+E1 [] =C2/A1 =C1+C1\n" +
                "[] [] [] =D3+E1\n" +
                "[] 34 [][] =D4+E1 [] []\n"
            );
            strw = new StringWriter();
            SheetEvaluator.Evaluate(strr, strw);
            Console.WriteLine(strw);
#endif
        }
    }
}
