using System;
using System.IO;

namespace _09_Excel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //if (args.Length != 2)
            //{
            //    Console.WriteLine("Argument Error");
            //    return;
            //}

            StringReader strr = new StringReader(
                "[] 3 =B1*A2\n" +
                "19 =C1+C2 42\n" +
                "car\n" +
                "=B2/A1 =A1-B4 =C2+A4\n" +
                "=error =A1+bus\n"
            );

            StringWriter strw = new StringWriter();

            SheetEvaluator.Evaluate(strr, strw);

            Console.WriteLine(strw);
        }
    }
}
