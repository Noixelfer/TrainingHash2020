using System.Collections.Generic;

namespace HashTraining
{
	public class Scoring
	{
		private static HashSet<int> scannedBooks = new HashSet<int>();
		private static int startingDay;
		private static int libraryId;
		private static int booksPerDay;
		public static int GetScore(BookScanning bookScanning, BookScanningOutput bookScanningOutput)
		{
			scannedBooks.Clear();
			startingDay = 0;
			int score = 0;

			for (int i = 0; i < bookScanningOutput.NumberOfScannedLibraries; i++)
			{
				libraryId = bookScanningOutput.ScannedLibraries[i].Item1;
				startingDay += bookScanning.Libraries[libraryId].SigningTime;
				booksPerDay = bookScanning.Libraries[libraryId].BooksShippedPerDay;
				var enumerator = bookScanningOutput.ScannedLibraries[i].Item2.GetEnumerator();
				enumerator.MoveNext();

				bool valid = true;

				for (int j = 0; j < bookScanning.NumberOfDays - startingDay; j++)
				{
					if (!valid)
						break;

					for (int k = 0; k < booksPerDay; k++)
					{
						if (!valid)
							break;

						int book = enumerator.Current;
						if (!scannedBooks.Contains(book))
						{
							scannedBooks.Add(book);
						}
						if (!enumerator.MoveNext())
						{
							valid = false;
						}
					}
				}
			}

			foreach (var book in scannedBooks)
			{
				score += bookScanning.BookScores[book];
			}
			return score;
		}
	}
}