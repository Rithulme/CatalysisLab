using System.Collections.Generic;

namespace ReactionEntities.Entities
{
    public class GlobalReaction
    {
        private ElementaryReaction[] _partialReactions;
        private Component[] _globalComponentList;
        private Dictionary<Component, double> _initialConcentration;
        private Dictionary<Component, List<double>> _concentrationEvolution;
        private List<double> _sampleTimes;

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

        public Dictionary<Component, double> InitialConcentration
        {
            get { return _initialConcentration; }
            set { _initialConcentration = value; }
        }

        public Dictionary<Component, List<double>> ConcentrationEvolution
        {
            get { return _concentrationEvolution; }
            set { _concentrationEvolution = value; }
        }

        public List<double> SampleTimes
        {
            get { return _sampleTimes; }
            set { _sampleTimes = value; }
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
