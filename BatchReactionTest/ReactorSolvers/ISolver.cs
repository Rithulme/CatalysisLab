using Reaction.Entities;

namespace ReactorSolvers
{
    public interface ISolver
    {
        void Solve(GlobalReaction reaction);
    }
}
