using System.Collections.Generic;

namespace ReactionEntities.Entities
{
    class GlobalReaction
    {
        private ElementaryReaction[] _partialReactions;

        public ElementaryReaction[] PartialReactions
        {
            get { return _partialReactions; }
            set { _partialReactions = value; }
        }

        public GlobalReaction(List<ElementaryReaction> partialReactions)
        {
            PartialReactions = partialReactions.ToArray();
        }

    }
}
