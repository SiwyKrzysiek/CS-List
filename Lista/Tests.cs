﻿using System;
using System.Diagnostics;

namespace Lista
{
    public class Tests
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Wykonanie testow jednostkowych
        /// </summary>
        /// <returns><c>true</c> if all tests were passed correctly <c>false</c> otherwise.</returns>
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
        public static void FullSpeedTest()
        {
            const int howManyTestsPerSires = 5000;
            int[] listLengthsToTest = {500, 2000, 6000};
            
            //SingleSpeedTest(6000, howManyTestsPerSires);

            string message = "Seraia pomiarów wyszukiwania elementu o losowo wybranym indeskie w liście\n" +
                             "Testowane jest wyszukiwanie iteracyjne i rekurencyjne\n" +
                             "--------------------------------------------------------------------------\n";
            Console.WriteLine(message);

            foreach (int listLength in listLengthsToTest)
            {
                SingleSpeedTest(listLength, howManyTestsPerSires);
            }
        }

        /// <summary>
        /// Wykonuje serie pomiarow rekurencyjnego i iteracyjnego wyszukania elementu.
        /// Wyświetla kilka przykladowych czasow i srednia wszystkich wynikow
        /// </summary>
        /// <param name="listSize">Dlugosc testowanej listy</param>
        /// <param name="testsAmount">Ilosc testow do przeprowadzenia</param>
        private static void SingleSpeedTest(int listSize, int testsAmount)
        {
            Tuple<long, long>[] results = new Tuple<long, long>[testsAmount];

            for (int i = 0; i < testsAmount; i++)
            {
                results[i] = MeasureSearchTime(listSize);
            }
            Tuple<double, double> means = CalculateMeanOfResults(results);

            int resultsToDisplay = Math.Min(10, testsAmount); //Wyswietlam co najwyzej 10 wynikow

            string beginningMessage = $"Test dla listy dlugosci {listSize}\n\n" + 
                                        "Przykladowe wyniki:\n" +
                                        "<czas iteracyjnego> <czas rekurencyjnego>\n";
            Console.WriteLine(beginningMessage);

            for (int i = 0; i < resultsToDisplay; i++)
            {
                Console.WriteLine($"{results[i].Item1} {results[i].Item2}");
            }

            Console.WriteLine($"\nSrednie wartosci z {testsAmount} próbek:\n{means.Item1} {means.Item2}\n" +
                              "-------------------------------\n");
        }

        /// <summary>
        /// Liczy srednia z wynikow pomiarow
        /// </summary>
        /// <returns>Krotka (srednia iteracji, srednia rekurencji)</returns>
        public static Tuple<double, double> CalculateMeanOfResults(Tuple<long, long>[] results)
        {
            long sumOfIterationTimes = 0;
            long sumOfRecursiveTimes = 0;

            foreach (Tuple<long, long> result in results)
            {
                sumOfIterationTimes += result.Item1;
                sumOfRecursiveTimes += result.Item2;
            }

            double iterationsMean = (double) sumOfIterationTimes / results.Length;
            double recursivesMean = (double) sumOfRecursiveTimes / results.Length;

            return Tuple.Create(iterationsMean, recursivesMean);
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
