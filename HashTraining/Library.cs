using System.Collections.Generic;

namespace HashTraining
{
	public class Library
	{
		public int NumberOfBooks { get; set; }
		public int SigningTime { get; set; }
		public int BooksShippedPerDay { get; set; }
		public HashSet<int> Books { get; set; }
	}
}
