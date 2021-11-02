using System;
using System.IO;

namespace _05_Nezarka.Net
{
    class Program
    {

        static void Main(string[] args)
        {
            ModelStore model = ModelStore.LoadFrom(Console.In);
            ViewStore view = new ViewStore();
            ControlStore controller = new ControlStore(model, view, Console.In, Console.Out);

            if (model == null)
            {
                Console.WriteLine("Data error.");
                return;
            }

            controller.Run();
        }
    }
}
