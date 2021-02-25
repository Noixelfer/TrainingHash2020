using System.Collections.Generic;

namespace HashTraining
{
	public class OutputModel
	{
		public int NumberOfScannedLibraries { get; set; }
		public List<(int, IEnumerable<int>)> ScannedLibraries { get; set; } = new List<(int, IEnumerable<int>)>();
		public int Score
		{
			get
			{
				return 0;
			}
		}
	}
}
