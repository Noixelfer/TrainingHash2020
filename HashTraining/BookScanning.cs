using System.Collections.Generic;

namespace HashTraining
{
	public class BookScanning
	{
		public int NumberOfBooks { get; set; }
		public int NumberOfLibraries { get; set; }
		public int NumberOfDays { get; set; }
		public List<int> BookScores { get; set; } = new List<int>();
		public List<Library> Libraries { get; set; } = new List<Library>();
	}
}
