using System;
using System.Collections.Generic;

namespace ReactionEntities.Entities
{
    public class ElementaryReaction
    {
        private Component[] _leftHandSide;
        private Component[] _rightHandSide;
        private double _preExponentialFactorForward;
        private double _activationEnergyForward;
        private double _preExponentialFactorBackward;
        private double _activationEnergyBackward;

        public Component[] LeftHandSide
        {
            get { return _leftHandSide; }
            set { _leftHandSide = value; }
        }

        public Component[] RightHandSide
        {
            get { return _rightHandSide; }
            set { _rightHandSide = value; }
        }        

        public double PreExponentialFactorForward
        {
            get { return _preExponentialFactorForward; }
            set { _preExponentialFactorForward = value; }
        }

        public double ActivationEnergyForward
        {
            get { return _activationEnergyForward; }
            set { _activationEnergyForward = value; }
        }

        public double PreExponentialFactorBackward
        {
            get { return _preExponentialFactorBackward; }
            set { _preExponentialFactorBackward = value; }
        }

        public double ActivationEnergyBackward
        {
            get { return _activationEnergyBackward; }
            set { _activationEnergyBackward = value; }
        }

        public ElementaryReaction(List<Component> leftHandSide, List<Component> rightHandSide, double preExponentialFactorForward, double activationEnergyForward,
            double preExponentialFactorBackward, double activationEnergyBackward)
        {
            LeftHandSide = new Component[leftHandSide.Count];
            RightHandSide = new Component[rightHandSide.Count];

            LeftHandSide = leftHandSide.ToArray();
            RightHandSide = rightHandSide.ToArray();

            PreExponentialFactorForward = preExponentialFactorForward;
            ActivationEnergyForward = activationEnergyForward;
            PreExponentialFactorBackward = preExponentialFactorBackward;
            ActivationEnergyBackward = activationEnergyBackward;
        }

        public double ForwardRateCoefficient(double temperature)
        {
            return PreExponentialFactorForward * Math.Exp(-ActivationEnergyForward / (PhysicalConstants.GASCONSTANT * temperature));
        }

        public double BackwardRateCoefficient(double temperature)
        {
            return PreExponentialFactorBackward * Math.Exp(-ActivationEnergyBackward / (PhysicalConstants.GASCONSTANT * temperature));
        }


        public List<Component> ListComponents()
        {
            List<Component> returnList = new List<Component>();
            foreach (var component in LeftHandSide)
            {
                if (!returnList.Contains(component))
                {
                    returnList.Add(component);
                }
            }

            foreach (var component in RightHandSide)
            {
                if (!returnList.Contains(component))
                {
                    returnList.Add(component);
                }
            }

            return returnList;
        }

        public Dictionary<Component, double> UpdateConcentration(Dictionary<Component, double> currentConcentration, double currentTemperature)
        {
            var returnDictionary = new Dictionary<Component, double>;
            foreach (var component in LeftHandSide)
            {
                double prodConcentration = 1.0;
                foreach (var componentBis in LeftHandSide)
                {
                    prodConcentration = prodConcentration * currentConcentration[componentBis];
                }
                double concentrationChange = -ForwardRateCoefficient(currentTemperature) * prodConcentration;

                prodConcentration = 1.0;
                foreach (var componentBis in RightHandSide)
                {
                    prodConcentration = prodConcentration * currentConcentration[componentBis];
                }
                concentrationChange = concentrationChange + BackwardRateCoefficient(currentTemperature) * prodConcentration;

                if (!returnDictionary.ContainsKey(component))
                {
                    returnDictionary.Add(component, currentConcentration[component] + concentrationChange);
                }
                else
                {
                    var tempconc = returnDictionary[component];
                    returnDictionary.Remove(component);
                    returnDictionary.Add(component, tempconc + concentrationChange);
                }
            }

            foreach (var component in RightHandSide)
            {
                double prodConcentration = 1.0;
                foreach (var componentBis in LeftHandSide)
                {
                    prodConcentration = prodConcentration * currentConcentration[componentBis];
                }
                double concentrationChange = ForwardRateCoefficient(currentTemperature) * prodConcentration;

                prodConcentration = 1.0;
                foreach (var componentBis in RightHandSide)
                {
                    prodConcentration = prodConcentration * currentConcentration[componentBis];
                }
                concentrationChange = concentrationChange - BackwardRateCoefficient(currentTemperature) * prodConcentration;

                if (!returnDictionary.ContainsKey(component))
                {
                    returnDictionary.Add(component, currentConcentration[component] + concentrationChange);
                }
                else
                {
                    var tempconc = returnDictionary[component];
                    returnDictionary.Remove(component);
                    returnDictionary.Add(component, tempconc + concentrationChange);
                }
            }

            return returnDictionary;
        }


    }
}
