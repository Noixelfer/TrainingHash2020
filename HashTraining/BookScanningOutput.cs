using System.Collections.Generic;

namespace HashTraining
{
	public class BookScanningOutput
	{
		public int NumberOfScannedLibraries { get; set; }
		public List<(int, List<int>)> ScannedLibraries { get; set; }
		public int Score
		{
			get
			{
				return 0;
			}
		}
	}
}
