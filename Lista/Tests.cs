using System;
using System.Diagnostics;

namespace Lista
{
    public class Tests
    {
        private static readonly Random random = new Random();

        public Tests()
        {
            
        }

        public static bool RunAllTests()
        {
            return ToStringTest() && AddRemoveTest() && AddRemoveTest(50) && PushBackTest() 
                && PushBackTest() && InsertAfterIndexTest();
        }

        public static bool ToStringTest()
        {
			List<int> list = new List<int>();

			list.PushBack(3);
			list.PushBack(24);
			list.PushBack(-4);

            return list.ToString() == "3->24->-4";
		}

        public static bool InsertAfterIndexTest()
        {
			List<int> list = new List<int>();

			list.PushBack(3);
			list.PushBack(24);
			list.PushBack(17);

            list.InsertAfterIndex(1, 8);

            return list.ToString() == "3->24->8->17";
        }

        public static bool PushBackTest()
        {
            int howManyElements = random.Next(300);
            List<int> list = new List<int>();

            for (int i = 0; i < howManyElements; i++)
            {
                list.PushBack(random.Next(99));
            }

            return list.Lenght == howManyElements;
        }

        public static bool RemoveAtIndexTest()
        {
            List<int> list = new List<int>();

			list.PushBack(4);
			list.PushBack(8);
			list.PushBack(2);
			list.PushBack(17);
			list.PushBack(11);
			list.PushBack(9);

            list.RemoveAtIndex(2);

            return (list.ToString() == "4->8->17->11->9" && list.Lenght == 5);
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

        public static void SpeedTest()
        {
            const int howManyTestsPerSires = 1000;
            Tuple<long, long>[] results = new Tuple<long, long>[howManyTestsPerSires];

            const int shortListSize = 2000;
            for (int i = 0; i < howManyTestsPerSires; i++)
            {
                results[i] = MeasureSearchTime(shortListSize);
            }

            foreach (var result in results)
            {
                Console.WriteLine("{0} {1}", result.Item1, result.Item2);
            }
        }

        /// <summary>
        /// Mierzy czas znalezienie losowego elementu w liscie o podanej dlugosci.
        /// Testowane jest przeszukanie iteracyjne i rekurencyjne
        /// </summary>
        /// <returns>
        /// Krotka (czas szukania iteracyjnego, czas szukania rekurencyjnego)
        /// Zwracane wartosci sa w cyklach zegara
        /// </returns>
        private static Tuple<long, long> MeasureSearchTime(int listSize)
        {
            List<int> list = GenerateRandomList(listSize);
            int indexToFind = random.Next(listSize);

            long timeOfInternationalSearch;
            long timeOfRecursiveSearch;

            Stopwatch stopwatch = Stopwatch.StartNew();
            list.FindAtIndex(indexToFind);
            stopwatch.Stop();
            timeOfInternationalSearch = stopwatch.ElapsedTicks;

            stopwatch = Stopwatch.StartNew();
            list.RecurentFindAtIndex(indexToFind);
            stopwatch.Stop();
            timeOfRecursiveSearch = stopwatch.ElapsedTicks;

            return Tuple.Create(timeOfInternationalSearch, timeOfRecursiveSearch);
        }

        /// <summary>
        /// Tworzy liste losowych liczb calokowitych
        /// </summary>
        /// <param name="length">Dlugosc generowanej listy</param>
        public static List<int> GenerateRandomList(int length)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < length; i++)
            {
                list.PushBack(random.Next(1000000));
            }

            return list;
        }
    }
}
