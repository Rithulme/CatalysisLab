using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reaction.Entities
{
    public class ReactionElement
    {
        public Component ReactionComponent { get; set; }

        public int Power { get; set; }

        public ReactionElement(Component component, int power)
        {
            Power = power;
            ReactionComponent = component;
        }

        public ReactionElement()
        {

        }
    }
}
