using System.Collections.Generic;

namespace HashTraining
{
	class PizzaModel
	{
		public int NumberOfSlices { get; set; }
		public int NumberOfTypes { get; set; }
		public IEnumerable<int> Slices { get; set; }
	}
}
