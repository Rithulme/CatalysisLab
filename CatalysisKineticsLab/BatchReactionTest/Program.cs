using ProblemTypes;
using Reaction.Entities;
using System;
using System.Collections.Generic;

namespace BatchReactionTest
{
    class Program
    {
        List<Component> componentList;

        static void Main(string[] args)
        {
        }

        public List<Component> MakeComponenets()
        {
            var componentList = new List<Component>();
            componentList.Add(new Component("A", "A"));
            componentList.Add(new Component("B", "B"));
            componentList.Add(new Component("C", "C"));

            return componentList;
        }

        public BatchProblemNoDiffusion CreateExampleOne()
        {
            // define partial reactions
            var ListReaction1LHS = new List<Tuple<Component, int>>();
            var ListReaction1RHS = new List<Tuple<Component, int>>();
            ListReaction1LHS.Add(new Tuple<Component, int>(componentList[0], 1));
            ListReaction1RHS.Add(new Tuple<Component, int>(componentList[1], 1));

            var Reaction1 = new ElementaryReaction(ListReaction1LHS, ListReaction1RHS);

            var returnproblem = new BatchProblemNoDiffusion(1000.0,10,);

        }
    }
}
