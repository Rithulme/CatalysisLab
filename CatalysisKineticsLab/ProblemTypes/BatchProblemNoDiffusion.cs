using Reaction.Calculators;
using Reaction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemTypes
{
    public class BatchProblemNoDiffusion : IProblemType
    {
        private GlobalReaction _globalReaction;
        private double _reactionTemperature;
        private Dictionary<Component, double> _initialConcentration;
        private double _timestep;
        private double _resultTimestep;
        private Dictionary<Component, List<double>> _resultConcentration;
        private Dictionary<Component, double> _currentConcentration;
        private double _totalTime;
        private readonly GlobalReactionCalculator _globalReactionCalculator;

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

        public BatchProblemNoDiffusion()
        {

        }

        public BatchProblemNoDiffusion(double totalTime, double resultTimestep, GlobalReaction globalReaction)
        {
            TotalTime = totalTime;
            GlobalReaction = globalReaction.Copy();
            _globalReactionCalculator = new GlobalReactionCalculator();
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

            for (long i = 0; i < numberOfIterations; i++)
            {
                CurrentConcentration = _globalReactionCalculator.updateConcentrations(GlobalReaction ,Timestep, ReactionTemperature, CurrentConcentration);
                runningTimeCounter = runningTimeCounter + Timestep;

                if (runningTimeCounter >= ResultTimestep)
                {
                    foreach (KeyValuePair<Component, double> keyValue in CurrentConcentration)
                    {
                        ResultConcentration[keyValue.Key].Add(keyValue.Value);
                    }

                    runningTimeCounter = 0.0;
                }
            }
        }

        public List<Component> getComponents()
        {
            return GlobalReaction.GlobalComponentList.ToList();
        }

        private long DetermineNumberOfIterations()
        {
            return (long)Math.Ceiling(TotalTime / Timestep);
        }

        private void DetermineTimesteps()
        {
            var estimatedChange = 1.0;
            var estimatedSpeed = 1.0;
            foreach (var elementaryReaction in GlobalReaction.PartialReactions)
            {
                foreach (var reactionElement in elementaryReaction.LeftHandSide)
                {
                    if(InitialConcentration[reactionElement.ReactionComponent] > 0.0001)
                    {
                        estimatedChange = Math.Min(estimatedChange, Math.Abs(elementaryReaction.ForwardRateCoefficient(ReactionTemperature))
                                * Math.Pow(InitialConcentration[reactionElement.ReactionComponent], reactionElement.Power));
                    }
                }
                estimatedSpeed = Math.Max(Math.Max(elementaryReaction.ForwardRateCoefficient(ReactionTemperature), elementaryReaction.BackwardRateCoefficient(ReactionTemperature)), estimatedSpeed);
            }

            Timestep = estimatedChange / (estimatedSpeed);

            // at least 100  steps per resultstep
            Timestep = Math.Min(Timestep, ResultTimestep / 100);            
        }

        private void Initialize()
        {
            
            CurrentConcentration = new Dictionary<Component, double>(new Component.EqualityComparer());
            ResultConcentration = new Dictionary<Component, List<double>>(new Component.EqualityComparer());

            foreach (var componentConcentrationPair in InitialConcentration)
            {
                CurrentConcentration.Add(componentConcentrationPair.Key.Copy(), componentConcentrationPair.Value);
                ResultConcentration.Add(componentConcentrationPair.Key.Copy(), new List<double> { componentConcentrationPair.Value });
            }
            
        }
    }
}
