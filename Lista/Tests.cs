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

        /// <summary>
        /// Sprawdza przechodzenie listy i wypisanie jej elementow
        /// </summary>
        /// <returns><c>true</c> if test was passed correctly<c>false</c> otherwise.</returns>
        public static bool ToStringTest()
        {
			List<int> list = new List<int>();

			list.PushBack(3);
			list.PushBack(24);
			list.PushBack(-4);

            return list.ToString() == "3->24->-4";
		}

		/// <summary>
		/// Test wstawiania elementu do listy
		/// </summary>
		/// <returns><c>true</c> if test was passed correctly<c>false</c> otherwise.</returns>
		public static bool InsertAfterIndexTest()
        {
			List<int> list = new List<int>();

			list.PushBack(3);
			list.PushBack(24);
			list.PushBack(17);

            list.InsertAfterIndex(1, 8);

            return list.ToString() == "3->24->8->17";
        }

		/// <summary>
		/// Test wstawiania elementu na koniec listy
		/// </summary>
		/// <returns><c>true</c> if test was passed correctly<c>false</c> otherwise.</returns>
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

		/// <summary>
		/// Test usuwania elementu z listy
		/// </summary>
		/// <returns><c>true</c> if test was passed correctly<c>false</c> otherwise.</returns>
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

		/// <summary>
		/// Test zapelnienia i oproznienia listy
		/// </summary>
		/// <returns><c>true</c> if test was passed correctly<c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Sprawdza ktore przeszukiwanie listy jest szybsze
        /// </summary>
        public static void SpeedTest()
        {
            const int howManyTestsPerSieres = 100;
            Tuple<long, long>[] results = new Tuple<long, long>[howManyTestsPerSieres];

            const int shortListSize = 2000;
            for (int i = 0; i < howManyTestsPerSieres; i++)
            {
                List<int> list = GenerateRandomList(shortListSize);
                int indexToFind = random.Next(shortListSize);

                Stopwatch stopwatch = Stopwatch.StartNew();
                list.FindAtIndex(indexToFind);
                stopwatch.Stop();
                long timeOfIterationalSearch = stopwatch.ElapsedTicks;

                stopwatch = Stopwatch.StartNew();
                list.RecurentFindAtIndex(indexToFind);
                stopwatch.Stop();
                long timeOfRecursiveSearch = stopwatch.ElapsedTicks;

                results[i] = Tuple.Create(timeOfIterationalSearch, timeOfRecursiveSearch);
            }

            foreach (var result in results)
            {
                Console.WriteLine("{0} {1}", result.Item1, result.Item2);
            }
        }

        /// <summary>
        /// Generates the random list.
        /// </summary>
        /// <returns>The random list.</returns>
        /// <param name="length">Length</param>
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
