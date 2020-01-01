using Reaction.Entities;
using System;
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
        private Dictionary<Component, List<double>> _resultConcentration;
        private Dictionary<Component, double> _currentConcentration;
        private double _totalTime;

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

        public double TotalTime
        {
            get { return _totalTime; }
            set { _totalTime = value; }
        }

        public BatchProblemNoDiffusion(double totalTime, double resultTimestep, GlobalReaction globalReaction)
        {
            TotalTime = totalTime;
            GlobalReaction = globalReaction.Copy();
            
        }

        public Dictionary<Component, List<double>> ResultConcentration
        {
            get { return _resultConcentration; }
            set { _resultConcentration = value; }
        }

        public Dictionary<Component, double> CurrentConcentration
        {
            get { return _currentConcentration; }
            set { _currentConcentration = value; }
        }

        public void Solve()
        {
            Initialize();
            DetermineTimesteps();
            var numberOfIterations = DetermineNumberOfIterations();
            double runningTimeCounter = 0.0;

            for (int i = 0; i < numberOfIterations; i++)
            {
                CurrentConcentration = GlobalReaction.updateConcentrations(Timestep, ReactionTemperature, CurrentConcentration);
                runningTimeCounter = runningTimeCounter + Timestep;

                if (runningTimeCounter >= ResultTimestep)
                {

                }
            }


        }

        private int DetermineNumberOfIterations()
        {
            return (int)Math.Ceiling(TotalTime / Timestep);
        }

        private void DetermineTimesteps()
        {
            throw new NotImplementedException();
        }

        private void Initialize()
        {
            
            CurrentConcentration = new Dictionary<Component, double>();
            ResultConcentration = new Dictionary<Component, List<double>>();

            foreach (var componentConcentrationPair in InitialConcentration)
            {
                CurrentConcentration.Add(componentConcentrationPair.Key.Copy(), componentConcentrationPair.Value);
                ResultConcentration.Add(componentConcentrationPair.Key.Copy(), new List<double> { componentConcentrationPair.Value });
            }
            
        }
    }
}
