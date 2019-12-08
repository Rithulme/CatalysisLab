﻿using System;
using System.Collections.Generic;

namespace Reaction.Entities
{
    public class ElementaryReaction
    {
        private Tuple<Component, int>[] _leftHandSide;
        private Tuple<Component, int>[] _rightHandSide;
        private double _preExponentialFactorForward;
        private double _activationEnergyForward;
        private double _preExponentialFactorBackward;
        private double _activationEnergyBackward;

        public Tuple<Component, int>[] LeftHandSide
        {
            get { return _leftHandSide; }
            set { _leftHandSide = value; }
        }

        public Tuple<Component, int>[] RightHandSide
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

        public ElementaryReaction(List<Tuple<Component, int>> leftHandSide, List<Tuple<Component, int>> rightHandSide, double preExponentialFactorForward, double activationEnergyForward,
            double preExponentialFactorBackward, double activationEnergyBackward)
        {
            LeftHandSide = new Tuple<Component, int>[leftHandSide.Count];
            RightHandSide = new Tuple<Component, int>[rightHandSide.Count];
            int counter = 0;

            foreach (var componentTuple in leftHandSide)
            {
                LeftHandSide[counter] = new Tuple<Component, int>(new Component(componentTuple.Item1.Name, componentTuple.Item1.ChemicalComposition), componentTuple.Item2);
                counter++;
            }

            counter = 0;

            foreach (var componentTuple in rightHandSide)
            {
                RightHandSide[counter] = new Tuple<Component, int>(new Component(componentTuple.Item1.Name, componentTuple.Item1.ChemicalComposition), componentTuple.Item2);
                counter++;
            }

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
            foreach (var componentTuple in LeftHandSide)
            {
                if (!returnList.Contains(componentTuple.Item1))
                {
                    returnList.Add(componentTuple.Item1);
                }
            }

            foreach (var componentTuple in RightHandSide)
            {
                if (!returnList.Contains(componentTuple.Item1))
                {
                    returnList.Add(componentTuple.Item1);
                }
            }

            return returnList;
        }

        public Dictionary<Component, double> UpdateConcentration(Dictionary<Component, double> currentConcentration, double currentTemperature, double timestep)
        {
            var returnDictionary = new Dictionary<Component, double>;
            foreach (var componentTuple in LeftHandSide)
            {
                double prodConcentration = 1.0;
                foreach (var componentTupleBis in LeftHandSide)
                {
                    prodConcentration = prodConcentration * Math.Pow(currentConcentration[componentTupleBis.Item1],componentTupleBis.Item2);
                }
                double concentrationChange = -ForwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

                prodConcentration = 1.0;
                foreach (var componentTupleBis in RightHandSide)
                {
                    prodConcentration = prodConcentration * Math.Pow(currentConcentration[componentTupleBis.Item1], componentTupleBis.Item2);
                }
                concentrationChange = concentrationChange + BackwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

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

            foreach (var componentTuple in RightHandSide)
            {
                double prodConcentration = 1.0;
                foreach (var componentTupleBis in LeftHandSide)
                {
                    prodConcentration = prodConcentration * Math.Pow(currentConcentration[componentTupleBis.Item1], componentTupleBis.Item2);
                }
                double concentrationChange = ForwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

                prodConcentration = 1.0;
                foreach (var componentTupleBis in RightHandSide)
                {
                    prodConcentration = prodConcentration * Math.Pow(currentConcentration[componentTupleBis.Item1], componentTupleBis.Item2);
                }
                concentrationChange = concentrationChange - BackwardRateCoefficient(currentTemperature) * prodConcentration * timestep;

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
