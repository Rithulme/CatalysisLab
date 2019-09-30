using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionEntities.Entities
{
    class ElementaryReaction
    {
        private Component[] _leftHandSide;
        private Component[] _rightHandSide;
        private double _preExponentialFactor;
        private double _activationEnergy;

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

        public double PreExponentialFactor
        {
            get { return _preExponentialFactor; }
            set { _preExponentialFactor = value; }
        }

        public double ActivationEnergy
        {
            get { return _activationEnergy; }
            set { _activationEnergy = value; }
        }

        public ElementaryReaction(List<Component> leftHandSide, List<Component> rightHandSide, double preExponentialFactor, double activationEnergy)
        {
            LeftHandSide = new Component[leftHandSide.Count];
            RightHandSide = new Component[rightHandSide.Count];

            LeftHandSide = leftHandSide.ToArray();
            RightHandSide = rightHandSide.ToArray();

            PreExponentialFactor = preExponentialFactor;
            ActivationEnergy = activationEnergy;
        }

        public double RateCoefficient(double temperature)
        {
            return PreExponentialFactor * Math.Exp(-ActivationEnergy / (PhysicalConstants.GASCONSTANT * temperature));
        }

    }
}
