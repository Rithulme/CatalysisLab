using System;
using System.Collections.Generic;

namespace Reaction.Entities
{
    public class ElementaryReaction
    {
        private ReactionElement[] _leftHandSide;
        private ReactionElement[] _rightHandSide;
        private double _preExponentialFactorForward;
        private double _activationEnergyForward;
        private double _preExponentialFactorBackward;
        private double _activationEnergyBackward;

        public ReactionElement[] LeftHandSide
        {
            get { return _leftHandSide; }
            set { _leftHandSide = value; }
        }

        public ReactionElement[] RightHandSide
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
            var leftHandSide = new ReactionElement[LeftHandSide.Length];
            var rightHandSide = new ReactionElement[RightHandSide.Length];
            int counter = 0;

            foreach (var reactionElement in LeftHandSide)
            {
                leftHandSide[counter] = new ReactionElement(reactionElement.ReactionComponent.Copy(), reactionElement.Power);
                counter++;
            }

            counter = 0;

            foreach (var reactionElement in RightHandSide)
            {
                rightHandSide[counter] = new ReactionElement(reactionElement.ReactionComponent.Copy(), reactionElement.Power);
                counter++;
            }

            return new ElementaryReaction(new List<ReactionElement>(leftHandSide), new List<ReactionElement>(rightHandSide),
                PreExponentialFactorForward, ActivationEnergyForward, PreExponentialFactorBackward, ActivationEnergyBackward);
        }

        public ElementaryReaction()
        {

        }

        public ElementaryReaction(List<ReactionElement> leftHandSide, List<ReactionElement> rightHandSide, double preExponentialFactorForward, double activationEnergyForward,
            double preExponentialFactorBackward, double activationEnergyBackward)
        {
            LeftHandSide = new ReactionElement[leftHandSide.Count];
            RightHandSide = new ReactionElement[rightHandSide.Count];
            int counter = 0;

            foreach (var reactionElement in leftHandSide)
            {
                LeftHandSide[counter] = new ReactionElement(reactionElement.ReactionComponent.Copy(), reactionElement.Power);
                counter++;
            }

            counter = 0;

            foreach (var reactionElement in rightHandSide)
            {
                RightHandSide[counter] = new ReactionElement(reactionElement.ReactionComponent.Copy(), reactionElement.Power);
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
            foreach (var reactionElement in LeftHandSide)
            {
                if (!returnList.Contains(reactionElement.ReactionComponent))
                {
                    returnList.Add(reactionElement.ReactionComponent);
                }
            }

            foreach (var reactionElement in RightHandSide)
            {
                if (!returnList.Contains(reactionElement.ReactionComponent))
                {
                    returnList.Add(reactionElement.ReactionComponent);
                }
            }

            return returnList;
        }     
    }
}
