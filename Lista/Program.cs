using System;

namespace Lista
{
    class Program
    {
        static void Main()
        {
            List<int> list = new List<int>();

			list.PushBack(4);
			list.PushBack(8);
			list.PushBack(2);
			list.PushBack(17);
			list.PushBack(11);
            list.PushBack(9);

            Console.WriteLine(list.ToString());
        }
    }
}