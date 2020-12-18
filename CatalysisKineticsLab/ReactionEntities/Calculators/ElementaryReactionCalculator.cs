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

            //calculate rate
            double prodConcentration = 1.0;
            foreach (var reactionElement in reaction.LeftHandSide)
            {
                prodConcentration = prodConcentration * Math.Pow(currentConcentration[reactionElement.ReactionComponent], reactionElement.Power);
            }
            var forwardRate = reaction.ForwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

            prodConcentration = 1.0;
            foreach (var reactionElement in reaction.RightHandSide)
            {
                prodConcentration = prodConcentration * Math.Pow(currentConcentration[reactionElement.ReactionComponent], reactionElement.Power);
            }
            var backwardRate = reaction.BackwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

            //calculate concentration changes associated to rates
            foreach (var element in reaction.LeftHandSide)
            {
                if (!returnDictionary.ContainsKey(element.ReactionComponent))
                {
                    returnDictionary.Add(element.ReactionComponent.Copy(), (backwardRate - forwardRate) * element.Power);
                }
                else
                {
                    returnDictionary[element.ReactionComponent] += (backwardRate - forwardRate) * element.Power;
                }
            }

            foreach (var element in reaction.RightHandSide)
            {
                if (!returnDictionary.ContainsKey(element.ReactionComponent))
                {
                    returnDictionary.Add(element.ReactionComponent.Copy(), (-backwardRate + forwardRate) * element.Power);
                }
                else
                {
                    returnDictionary[element.ReactionComponent] += (-backwardRate + forwardRate) * element.Power;
                }
            }
                                   
            return returnDictionary;
        }

    }
}
