using Reaction.Entities;
using System.Collections.Generic;

namespace ReactorSolvers
{
    public class BatchSolver : ISolver
    {
        public void Solve(GlobalReaction reaction)
        {

        }

        private void SetStart(GlobalReaction reaction)
        {
            foreach (var component in reaction.GlobalComponentList)
            {
                double initialConcentration;
                reaction.InitialConcentration.TryGetValue(component, out initialConcentration);
                var concentrationList = new List<double> { initialConcentration };
                reaction.ConcentrationEvolution.Add(component, concentrationList);
            }
        }
    }
}
