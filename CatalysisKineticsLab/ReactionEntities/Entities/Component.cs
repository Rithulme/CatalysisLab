
using System;
using System.Collections.Generic;

namespace Reaction.Entities
{
    public class Component
    {
        private string _name;
        private string _chemicalComposition;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string ChemicalComposition
        {
            get { return _chemicalComposition; }
            set { _chemicalComposition = value; }
        }

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
