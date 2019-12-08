using System.Collections.Generic;
using System.Linq;

namespace Reaction.Entities
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

        private Component[] ListComponents()
        {
            List<Component> componentList = new List<Component>();

            foreach (var elementaryReaction in PartialReactions)
            {
                componentList.AddRange(elementaryReaction.ListComponents());
            }

            return componentList.ToArray();
        }

        public Dictionary<Component, double> updateConcentrations(double timestep, double temperature, Dictionary<Component, double> currentConcentration)
        {
            var returnConcentrationChange = new Dictionary<Component, double>;

            foreach (var component in currentConcentration.Keys)
            {
                returnConcentrationChange.Add(component, 0.0);
            }

            foreach (var partialReaction in PartialReactions)
            {
                var concentrationChange = partialReaction.UpdateConcentration(currentConcentration, temperature, timestep);
                foreach (var component in concentrationChange.Keys)
                {
                    returnConcentrationChange[component] = returnConcentrationChange[component] + concentrationChange[component];
                }                
            }

            return returnConcentrationChange;
        }

    }
}
