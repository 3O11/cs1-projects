using System;

namespace OHledaniBugu
{
	class Program
	{
		static void Main(string[] args)
		{
			if (!long.TryParse(Console.ReadLine(), out long a) || a < 0)
            {
                Console.WriteLine("Error!");
				return;
            }

			if (!long.TryParse(Console.ReadLine(), out long b) || b < 0)
			{
				Console.WriteLine("Error!");
				return;
			}

			Console.WriteLine("Result: " + Math.Abs(a-b));	
		}
	}
}