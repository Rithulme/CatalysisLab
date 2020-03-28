using System;
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

        public ElementaryReaction Copy()
        {
            var leftHandSide = new Tuple<Component,int>[LeftHandSide.Length];
            var rightHandSide = new Tuple<Component, int>[RightHandSide.Length];
            int counter = 0;

            foreach (var componentTuple in LeftHandSide)
            {
                leftHandSide[counter] = new Tuple<Component, int>(componentTuple.Item1.Copy(), componentTuple.Item2);
                counter++;
            }

            counter = 0;

            foreach (var componentTuple in RightHandSide)
            {
                rightHandSide[counter] = new Tuple<Component, int>(componentTuple.Item1.Copy(), componentTuple.Item2);
                counter++;
            }

            return new ElementaryReaction(new List<Tuple<Component, int>>(leftHandSide), new List<Tuple<Component, int>>(rightHandSide),
                PreExponentialFactorForward, ActivationEnergyForward, PreExponentialFactorBackward, ActivationEnergyBackward);
        }

        public ElementaryReaction(List<Tuple<Component, int>> leftHandSide, List<Tuple<Component, int>> rightHandSide, double preExponentialFactorForward, double activationEnergyForward,
            double preExponentialFactorBackward, double activationEnergyBackward)
        {
            LeftHandSide = new Tuple<Component, int>[leftHandSide.Count];
            RightHandSide = new Tuple<Component, int>[rightHandSide.Count];
            int counter = 0;

            foreach (var componentTuple in leftHandSide)
            {
                LeftHandSide[counter] = new Tuple<Component, int>(componentTuple.Item1.Copy(), componentTuple.Item2);
                counter++;
            }

            counter = 0;

            foreach (var componentTuple in rightHandSide)
            {
                RightHandSide[counter] = new Tuple<Component, int>(componentTuple.Item1.Copy(), componentTuple.Item2);
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
    }
}
