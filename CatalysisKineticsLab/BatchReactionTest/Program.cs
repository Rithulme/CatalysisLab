using ProblemTypes;
using Reaction.Entities;
using System;
using System.Collections.Generic;

namespace BatchReactionTest
{
    class Program
    {
        static List<Component> componentList;

        static void Main(string[] args)
        {
            componentList = MakeComponents();
            var example = CreateExampleOne();

            example.InitialConcentration = new Dictionary<Component, double>(new Component.EqualityComparer());
            example.InitialConcentration.Add(componentList[0], 0.2);
            example.InitialConcentration.Add(componentList[1], 0.0);
            example.ResultTimestep = 5;
            example.ReactionTemperature = 323;

            example.Solve();

            if (true)
            {

            }

        }

        public static List<Component> MakeComponents()
        {
            var componentList = new List<Component>();
            componentList.Add(new Component("A", "A"));
            componentList.Add(new Component("B", "B"));
            componentList.Add(new Component("C", "C"));

            return componentList;
        }

        public static BatchProblemNoDiffusion CreateExampleOne()
        {
            // define partial reactions
            var ListReaction1LHS = new List<Tuple<Component, int>>();
            var ListReaction1RHS = new List<Tuple<Component, int>>();
            ListReaction1LHS.Add(new Tuple<Component, int>(componentList[0], 1));
            ListReaction1RHS.Add(new Tuple<Component, int>(componentList[1], 1));

            var Reaction1 = new ElementaryReaction(ListReaction1LHS, ListReaction1RHS, 250000, 50000, 125000, 52000);

            //make globalreaction
            List <ElementaryReaction>  reactionList = new List<ElementaryReaction> { Reaction1 };
            var GlobalReaction = new GlobalReaction(reactionList);

            var returnproblem = new BatchProblemNoDiffusion(1000.0,10,GlobalReaction);

            return returnproblem;

        }
    }
}
