using System;

namespace Lista
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Testy jednostkowe poprawne: {0}\n", Tests.RunAllTests());

            Tests.FullSpeedTest();
		}
    }
}