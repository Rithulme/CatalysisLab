
using System;
using System.Collections.Generic;

namespace Reaction.Entities
{
    public class Component
    {

        public string Name { get; set; }

        public string ChemicalComposition { get; set; }
                
        public Component()
        {

        }

        public Component(string name, string composition)
        {
            Name = name;
            ChemicalComposition = composition;
        }

        public bool Equals(Component comparedComponent)
        {
            return string.Equals(Name, comparedComponent.Name);
        }

        public Component Copy()
        {
            return new Component(this.Name, this.ChemicalComposition);
        }

        public class EqualityComparer : IEqualityComparer<Component>
        {
            public bool Equals(Component componentRef, Component comparedComponent)
            {
                return string.Equals(componentRef.Name, comparedComponent.Name);
            }

            public int GetHashCode(Component componentRef) //todo: dit moeten we nog eens bekijken.
            {
                return 0;
            }
        }                  
    }
}
