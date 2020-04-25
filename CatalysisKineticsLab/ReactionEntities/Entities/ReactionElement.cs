using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reaction.Entities
{
    public class ReactionElement
    {
        private Component _reactionComponent;
        private int _power;

        public Component ReactionComponent
        {
            get { return _reactionComponent; }
            set { _reactionComponent = value; }
        }

        public int Power
        {
            get { return _power; }
            set { _power = value; }
        }

        public ReactionElement(Component component, int power)
        {
            _power = power;
            _reactionComponent = component;
        }
    }
}
