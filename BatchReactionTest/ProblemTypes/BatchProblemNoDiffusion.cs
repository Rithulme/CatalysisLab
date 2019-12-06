using Reaction.Entities;
using System.Collections.Generic;

namespace ProblemTypes
{
    public class BatchProblemNoDiffusion
    {
        private GlobalReaction _globalReaction;
        private double _reactionTemperature;
        private Dictionary<Component, double> _initialConcentration;

        public GlobalReaction GlobalReaction
        {
            get { return _globalReaction; }
            set { _globalReaction = value; }
        }       

        public double ReactionTemperature
        {
            get { return _reactionTemperature; }
            set { _reactionTemperature = value; }
        }       

        public Dictionary<Component, double> InitialConcentration
        {
            get { return _initialConcentration; }
            set { _initialConcentration = value; }
        }



    }
}
