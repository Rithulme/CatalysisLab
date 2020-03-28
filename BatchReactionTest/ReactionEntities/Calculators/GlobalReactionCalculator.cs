using Reaction.Entities;
using System.Collections.Generic;

namespace Reaction.Calculators
{
    public class GlobalReactionCalculator
    {
        private readonly ElementaryReactionCalculator _elementaryReactionCalculator;

        public GlobalReactionCalculator()
        {
            _elementaryReactionCalculator = new ElementaryReactionCalculator();
        }

        public Dictionary<Component, double> updateConcentrations(GlobalReaction globalReaction ,double timestep, double temperature, Dictionary<Component, double> currentConcentration)
        {
            var returnConcentrationChange = new Dictionary<Component, double>();

            foreach (var component in currentConcentration.Keys)
            {
                returnConcentrationChange.Add(component, 0.0);
            }

            foreach (var partialReaction in globalReaction.PartialReactions)
            {
                var concentrationChange = _elementaryReactionCalculator.UpdateConcentration(partialReaction, currentConcentration, temperature, timestep);
                foreach (var component in concentrationChange.Keys)
                {
                    returnConcentrationChange[component] = returnConcentrationChange[component] + concentrationChange[component];
                }
            }

            return returnConcentrationChange;
        }
    }
}
