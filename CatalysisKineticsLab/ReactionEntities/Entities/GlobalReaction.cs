using System.Collections.Generic;
using System.Linq;

namespace Reaction.Entities
{
    public class GlobalReaction
    {
        private ElementaryReaction[] _partialReactions;
        private Component[] _globalComponentList;

        public ElementaryReaction[] PartialReactions
        {
            get { return _partialReactions; }
            set { _partialReactions = value; }
        }
               
        public Component[] GlobalComponentList
        {
            get { return _globalComponentList; }
            set { _globalComponentList = value; }
        }

        public GlobalReaction()
        {

        }

        public GlobalReaction(List<ElementaryReaction> partialReactions)
        {
            //Make a deep copy of the partial reactions
            PartialReactions = new ElementaryReaction[partialReactions.Count];
            int counter = 0;

            foreach (var partialReaction in partialReactions)
            {
                PartialReactions[counter] = new ElementaryReaction(partialReaction.LeftHandSide.ToList(), partialReaction.RightHandSide.ToList(), partialReaction.PreExponentialFactorForward,
                    partialReaction.ActivationEnergyForward, partialReaction.PreExponentialFactorBackward, partialReaction.ActivationEnergyBackward);
                counter++;
            }

            GlobalComponentList = ListComponents();           
        }

        public GlobalReaction Copy()
        {
            var partialReactions = new List<ElementaryReaction>();

            foreach (var partialReaction in PartialReactions)
            {
                partialReactions.Add(partialReaction.Copy());
            }

            return new GlobalReaction(partialReactions);
        }

        private Component[] ListComponents()
        {
            List<Component> componentList = new List<Component>();

            foreach (var elementaryReaction in PartialReactions)
            {
                componentList.AddRange(elementaryReaction.ListComponents());
            }

            return componentList.ToArray();
        }
    }
}
