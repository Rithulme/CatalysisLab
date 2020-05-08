using Reaction.Entities;
using System.Collections.Generic;

namespace ProblemTypes
{
    public interface IProblemType
    {
        Dictionary<Component, double> InitialConcentration { get; set; }
        double ReactionTemperature { get; set; }
        double ResultTimestep { get; set; }
        Dictionary<Component, List<double>> ResultConcentration { get; set; }

        void Solve();
        List<Component> getComponents();

    }
}
