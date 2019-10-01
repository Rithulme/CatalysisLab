using System.Collections.Generic;

namespace ReactionEntities.Entities
{
    class GlobalReaction
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

        public GlobalReaction(List<ElementaryReaction> partialReactions)
        {
            PartialReactions = partialReactions.ToArray();
            GlobalComponentList = ListComponents();
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
