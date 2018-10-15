using System;

namespace Lista
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello world");
            //string userIput = Console.ReadLine();
            //Console.WriteLine(userIput);

            Node node = new Node(5);

            if (node.Next == null)
                Console.WriteLine("node is not null");
        }
    }
}