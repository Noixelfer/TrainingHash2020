using System.Collections.Generic;

namespace HashTraining
{
	class ClasicSolutionD
	{
		private PizzaModel pizzaModel;
		public ClasicSolutionD(PizzaModel pizzaModel)
		{
			this.pizzaModel = pizzaModel;
		}

		public PizzaOutputModel CreateOuputModel(string fileName)
		{
			//First, create a greedy model
			var remainingSpace = pizzaModel.NumberOfSlices;
			var output = new PizzaOutputModel(fileName);
			output.SelectedPizzaIds = new List<int>();

			for (int i = pizzaModel.Slices.Count - 1; i > 0; i--)
			{
				if (remainingSpace > pizzaModel.Slices[i])
				{
					remainingSpace -= pizzaModel.Slices[i];
					output.SelectedPizzaIds.Add(i);
				}
			}

			output.Score = pizzaModel.NumberOfSlices - remainingSpace;
			return output;
		}
	}
}
