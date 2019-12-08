using Reaction.Entities;
using System.Collections.Generic;

namespace ProblemTypes
{
    public class BatchProblemNoDiffusion
    {
        private GlobalReaction _globalReaction;
        private double _reactionTemperature;
        private Dictionary<Component, double> _initialConcentration;
        private double _timestep;
        private double _resultTimestep;

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
               
        public double Timestep
        {
            get { return _timestep; }
            set { _timestep = value; }
        }        

        public double ResultTimestep
        {
            get { return _resultTimestep; }
            set { _resultTimestep = value; }
        }

        public BatchProblemNoDiffusion(double timestep, double resultTimestep, GlobalReaction globalReaction)
        {
            Timestep = timestep;
            ResultTimestep = resultTimestep;
        }





    }
}
