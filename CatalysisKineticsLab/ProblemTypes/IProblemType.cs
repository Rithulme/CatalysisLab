using Reaction.Entities;
using System.Collections.Generic;

namespace ProblemTypes
{
    public interface IProblemType
    {
        void Solve();
        List<Component> getComponents();
    }
}
