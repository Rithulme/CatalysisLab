using System;
using System.Collections.Generic;
using System.Linq;

namespace Reaction.Entities
{
    public class ElementaryReaction
    {
        public List<ReactionElement> LeftHandSide { get; set; }

        public List<ReactionElement> RightHandSide { get; set; }

        public double PreExponentialFactorForward { get; set; }

        public double ActivationEnergyForward { get; set; }

        public double PreExponentialFactorBackward { get; set; }

        public double ActivationEnergyBackward { get; set; }

        public ElementaryReaction Copy()
        {
            var leftHandSide = new List<ReactionElement>();
            var rightHandSide = new List<ReactionElement>();

            foreach (var reactionElement in LeftHandSide)
            {
                leftHandSide.Add(new ReactionElement(reactionElement.ReactionComponent.Copy(), reactionElement.Power));
            }

            foreach (var reactionElement in RightHandSide)
            {
                rightHandSide.Add(new ReactionElement(reactionElement.ReactionComponent.Copy(), reactionElement.Power));
            }

            return new ElementaryReaction(leftHandSide, rightHandSide,
                PreExponentialFactorForward, ActivationEnergyForward, PreExponentialFactorBackward, ActivationEnergyBackward);
        }

        public ElementaryReaction()
        {

        }

        public ElementaryReaction(List<ReactionElement> leftHandSide, List<ReactionElement> rightHandSide, double preExponentialFactorForward, double activationEnergyForward,
            double preExponentialFactorBackward, double activationEnergyBackward)
        {
            LeftHandSide = new List<ReactionElement>();
            RightHandSide = new List<ReactionElement>();

            foreach (var reactionElement in leftHandSide)
            {
                LeftHandSide.Add(new ReactionElement(reactionElement.ReactionComponent.Copy(), reactionElement.Power));
            }

            foreach (var reactionElement in rightHandSide)
            {
                RightHandSide.Add(new ReactionElement(reactionElement.ReactionComponent.Copy(), reactionElement.Power));
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
