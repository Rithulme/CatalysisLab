using Reaction.Entities;
using System.Collections.Generic;

namespace ProblemTypes
{
    public interface IProblemType
    {
        Dictionary<Component, double> InitialConcentration { get; set; }

        void Solve();
        List<Component> getComponents();

    }
}
