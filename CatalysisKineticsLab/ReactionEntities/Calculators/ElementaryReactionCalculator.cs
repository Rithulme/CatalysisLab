using Reaction.Entities;
using System;
using System.Collections.Generic;

namespace Reaction.Calculators
{
    public class ElementaryReactionCalculator
    {
        public ElementaryReactionCalculator()
        {

        }

        public Dictionary<Component, double> UpdateConcentration(ElementaryReaction reaction ,Dictionary<Component, double> currentConcentration, double currentTemperature, double timestep)
        {
            var returnDictionary = new Dictionary<Component, double>(new Component.EqualityComparer());
            foreach (var reactionElement in reaction.LeftHandSide)
            {
                double prodConcentration = 1.0;
                foreach (var reactionElementBis in reaction.LeftHandSide)
                {
                    prodConcentration = prodConcentration * Math.Pow(currentConcentration[reactionElementBis.ReactionComponent], reactionElementBis.Power);
                }
                double concentrationChange = -reaction.ForwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

                prodConcentration = 1.0;
                foreach (var reactionElementBis in reaction.RightHandSide)
                {
                    prodConcentration = prodConcentration * Math.Pow(currentConcentration[reactionElementBis.ReactionComponent], reactionElementBis.Power);
                }
                concentrationChange = concentrationChange + reaction.BackwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

                if (!returnDictionary.ContainsKey(reactionElement.ReactionComponent))
                {
                    returnDictionary.Add(reactionElement.ReactionComponent, concentrationChange);
                }
                else
                {
                    var tempconc = returnDictionary[reactionElement.ReactionComponent];
                    returnDictionary.Remove(reactionElement.ReactionComponent);
                    returnDictionary.Add(reactionElement.ReactionComponent, tempconc + concentrationChange);
                }
            }

            foreach (var reactionElement in reaction.RightHandSide)
            {
                double prodConcentration = 1.0;
                foreach (var reactionElementBis in reaction.LeftHandSide)
                {
                    prodConcentration = prodConcentration * Math.Pow(currentConcentration[reactionElementBis.ReactionComponent], reactionElementBis.Power);
                }
                double concentrationChange = reaction.ForwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

                prodConcentration = 1.0;
                foreach (var reactionElementBis in reaction.RightHandSide)
                {
                    prodConcentration = prodConcentration * Math.Pow(currentConcentration[reactionElementBis.ReactionComponent], reactionElementBis.Power);
                }
                concentrationChange = concentrationChange - reaction.BackwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

                if (!returnDictionary.ContainsKey(reactionElement.ReactionComponent))
                {
                    returnDictionary.Add(reactionElement.ReactionComponent, concentrationChange);
                }
                else
                {
                    var tempconc = returnDictionary[reactionElement.ReactionComponent];
                    returnDictionary.Remove(reactionElement.ReactionComponent);
                    returnDictionary.Add(reactionElement.ReactionComponent, tempconc + concentrationChange);
                }
            }

            return returnDictionary;
        }

    }
}
