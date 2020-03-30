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
            foreach (var componentTuple in reaction.LeftHandSide)
            {
                double prodConcentration = 1.0;
                foreach (var componentTupleBis in reaction.LeftHandSide)
                {
                    prodConcentration = prodConcentration * Math.Pow(currentConcentration[componentTupleBis.Item1], componentTupleBis.Item2);
                }
                double concentrationChange = -reaction.ForwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

                prodConcentration = 1.0;
                foreach (var componentTupleBis in reaction.RightHandSide)
                {
                    prodConcentration = prodConcentration * Math.Pow(currentConcentration[componentTupleBis.Item1], componentTupleBis.Item2);
                }
                concentrationChange = concentrationChange + reaction.BackwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

                if (!returnDictionary.ContainsKey(componentTuple.Item1))
                {
                    returnDictionary.Add(componentTuple.Item1, concentrationChange);
                }
                else
                {
                    var tempconc = returnDictionary[componentTuple.Item1];
                    returnDictionary.Remove(componentTuple.Item1);
                    returnDictionary.Add(componentTuple.Item1, tempconc + concentrationChange);
                }
            }

            foreach (var componentTuple in reaction.RightHandSide)
            {
                double prodConcentration = 1.0;
                foreach (var componentTupleBis in reaction.LeftHandSide)
                {
                    prodConcentration = prodConcentration * Math.Pow(currentConcentration[componentTupleBis.Item1], componentTupleBis.Item2);
                }
                double concentrationChange = reaction.ForwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

                prodConcentration = 1.0;
                foreach (var componentTupleBis in reaction.RightHandSide)
                {
                    prodConcentration = prodConcentration * Math.Pow(currentConcentration[componentTupleBis.Item1], componentTupleBis.Item2);
                }
                concentrationChange = concentrationChange - reaction.BackwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

                if (!returnDictionary.ContainsKey(componentTuple.Item1))
                {
                    returnDictionary.Add(componentTuple.Item1, concentrationChange);
                }
                else
                {
                    var tempconc = returnDictionary[componentTuple.Item1];
                    returnDictionary.Remove(componentTuple.Item1);
                    returnDictionary.Add(componentTuple.Item1, tempconc + concentrationChange);
                }
            }

            return returnDictionary;
        }

    }
}
