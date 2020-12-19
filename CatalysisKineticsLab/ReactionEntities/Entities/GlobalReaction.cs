using System.Collections.Generic;
using System.Linq;

namespace Reaction.Entities
{
    public class GlobalReaction
    {
        public List<ElementaryReaction> PartialReactions { get; set; }

        public List<Component> GlobalComponentList { get; set; }

        public GlobalReaction()
        {

        }

        public GlobalReaction(List<ElementaryReaction> partialReactions)
        {
            //Make a deep copy of the partial reactions
            PartialReactions = new List<ElementaryReaction>();

            foreach (var partialReaction in partialReactions)
            {
                PartialReactions.Add(new ElementaryReaction(partialReaction.LeftHandSide.ToList(), partialReaction.RightHandSide.ToList(), partialReaction.PreExponentialFactorForward,
                    partialReaction.ActivationEnergyForward, partialReaction.PreExponentialFactorBackward, partialReaction.ActivationEnergyBackward));
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

        private List<Component> ListComponents()
        {
            List<Component> componentList = new List<Component>();

            foreach (var elementaryReaction in PartialReactions)
            {
                foreach (var component in elementaryReaction.ListComponents())
                {
                    if (!componentList.Contains(component,new Component.EqualityComparer()))
                    {
                        componentList.Add(component);
                    }
                }                
            }

            return componentList;
        }
    }
}
