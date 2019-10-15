using ReactionEntities.Entities;
using ReactorSolvers;
using System.Collections.Generic;

namespace BatchReactionTest
{
    class FullProblem
    {
        private GlobalReaction _reaction;

        public GlobalReaction Reaction
        {
            get { return _reaction; }
            set { _reaction = value; }
        }

        private ISolver _reactionSolver;

        public ISolver ReactionSolver
        {
            get { return _reactionSolver; }
            set { _reactionSolver = value; }
        }

        public void setReactionSamples(List<double> sampleTimes)
        {
            Reaction.SampleTimes = new List<double>(sampleTimes);
        }


    }
}
