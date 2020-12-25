using Reaction.Calculators;
using Reaction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemTypes
{
    public class BatchProblemNoDiffusion : IProblemType
    {
        private readonly GlobalReactionCalculator _globalReactionCalculator;

        public GlobalReaction GlobalReaction { get; set; }

        public double ReactionTemperature { get; set; }

        public Dictionary<Component, double> InitialConcentration { get; set; }

        public double Timestep { get; set; }

        public int NumberOfSamples { get; set; }

        public double ResultTimestep { get; set; }

        public double TotalTime { get; set; }

        public BatchProblemNoDiffusion()
        {
            _globalReactionCalculator = new GlobalReactionCalculator();
        }

        public BatchProblemNoDiffusion(double totalTime, int resultTimestep, GlobalReaction globalReaction)
        {
            TotalTime = totalTime;
            GlobalReaction = globalReaction.Copy();
            _globalReactionCalculator = new GlobalReactionCalculator();
        }

        public BatchProblemNoDiffusion(GlobalReaction globalReaction)
        {
            GlobalReaction = globalReaction.Copy();
            _globalReactionCalculator = new GlobalReactionCalculator();
        }

        public Dictionary<Component, List<double>> ResultConcentration { get; set; }

        public Dictionary<Component, double> CurrentConcentration { get; set; }

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

            Timestep = Math.Max(estimatedChange / (estimatedSpeed), 0.005);

            // at least 100  steps per resultstep
            Timestep = Math.Min(Timestep, ResultTimestep / 100);
            
        }

        private void Initialize()
        {
            
            CurrentConcentration = new Dictionary<Component, double>(new Component.EqualityComparer());
            ResultConcentration = new Dictionary<Component, List<double>>(new Component.EqualityComparer());

            if (Math.Abs(ResultTimestep) < 0.0001)
            {
                ResultTimestep = TotalTime / NumberOfSamples;
            }

            foreach (var componentConcentrationPair in InitialConcentration)
            {
                CurrentConcentration.Add(componentConcentrationPair.Key.Copy(), componentConcentrationPair.Value);
                ResultConcentration.Add(componentConcentrationPair.Key.Copy(), new List<double> { componentConcentrationPair.Value });
            }
            
        }
    }
}
