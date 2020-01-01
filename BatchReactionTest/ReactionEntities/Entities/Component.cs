
using System;

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
            return new Component(this.Name,this.ChemicalComposition);
        }
    }
}
