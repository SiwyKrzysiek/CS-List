using System;
namespace Lista
{
    public class Tests
    {
        private static Random random = new Random();

        public Tests()
        {
            
        }

        public static bool RunAllTests()
        {
            return AddRemoveTest() && AddRemoveTest(50) && pushBackTest();
        }

        public static bool pushBackTest()
        {
            int howManyElements = random.Next(300);
            List<int> list = new List<int>();

            for (int i = 0; i < howManyElements; i++)
            {
                list.PushBack(random.Next(99));
            }

            return list.Lenght == howManyElements;
        }

        public static bool AddRemoveTest(int howManyElementsToAdd = 1)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < howManyElementsToAdd; i++)
            {
                list.PushBack(random.Next(100));
            }

			for (int i = 0; i < howManyElementsToAdd; i++)
			{
				list.PopBack();
			}

            return list.Lenght == 0;
        }
    }
}
